using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using CircleCI.Api.Configuration;
using CircleCI.Api.Services.TokenService;
using CircleCI.DataService.Repositories.Interfaces;
using CircleCI.Entities.DbSet;
using CircleCI.Entities.DTOs.Requests;
using CircleCI.Entities.DTOs.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CircleCI.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthManagementController : BaseController
{
    private readonly JwtConfig _jwtConfig;
    public AuthManagementController(IUnitOfWork unitOfWork,
        IOptions<JwtConfig> jwtConfig,
        IUserIdentifire userIdentifire,
        IMapper mapper) : base(unitOfWork, mapper, userIdentifire)
    {
        _jwtConfig = jwtConfig.Value;
    }

    private string CreateToken(User user, bool isAccess)
    {
        List<Claim> claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(
            isAccess ? _jwtConfig.AccessTokenSecret : _jwtConfig.RefreshTokenSecret));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: isAccess ? DateTime.UtcNow.AddMinutes(10) : DateTime.UtcNow.AddDays(10), // TODO - AddMinutes must equal 1
            signingCredentials: credentials
            );

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }
    
    private bool IsValidToken(string token, bool isAccess)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(
            isAccess ? _jwtConfig.AccessTokenSecret : _jwtConfig.RefreshTokenSecret));

        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,
            IssuerSigningKey = key
        };

        try
        {
            _ = tokenHandler.ValidateToken(token, tokenValidationParameters, out _);
            return true;
        }
        catch (SecurityTokenValidationException)
        {
            return false;
        }
    }
    
    private void SetCookie(string accessToken)
    {
        HttpContext.Response.Cookies.Append("X-Access-Token", accessToken,
            new CookieOptions
            {
                Expires = DateTime.UtcNow.AddDays(30),
                Path = "/api/auth/refresh"
            });
    }
    
    [AllowAnonymous]
    [HttpPost("registration")]
    public async Task<IActionResult> Registration([FromBody] UserRegistrationRequest request)
    {
        if (ModelState.IsValid)
        {
            var user = await _unitOfWork.Users.GetByEmailAsync(request.Email);
            
            if (!string.IsNullOrEmpty(user.Email))
            {
                return BadRequest("This email is already exist");
            }

            request.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);

            user = _mapper.Map<User>(request);
            user.Token.User = user;
            user.Token.RefreshToken = CreateToken(user, false);
            
            await _unitOfWork.Users.Add(user);
            await _unitOfWork.CompleteAsync();

            var accessToken = CreateToken(user, true);
            
            SetCookie(accessToken);

            return Ok(new
            {
                User = _mapper.Map<UserAuthResponse>(user),
                accessToken,
            });
        }

        return BadRequest("Error occured");
    }
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody]UserLoginRequest request)
    {
        if (ModelState.IsValid)
        {
            var user = await _unitOfWork.Users.GetByEmailAsync(request.Email);

            if (user == null ||
                string.IsNullOrEmpty(user.Email))
            {
                return BadRequest("Email aren`t exist");
            }

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                return BadRequest("Wrong password");
            }
        
            var accessToken = CreateToken(user, true);
        
            if (string.IsNullOrEmpty(user.Token.RefreshToken) 
                || !IsValidToken(user.Token.RefreshToken, false))
            {
                user.Token.RefreshToken = CreateToken(user, false);
                await _unitOfWork.CompleteAsync();
            }
        
            SetCookie(accessToken);

            return Ok(new
            {
                User = _mapper.Map<UserAuthResponse>(user),
                accessToken,
            });
        }

        return BadRequest();
    }
    
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        var token = await _unitOfWork.Tokens.GetUserToken(_userIdentifire.GetIdByHeader(HttpContext));
        
        Response.Cookies.Delete("X-Access-Token", new CookieOptions
            {
                Expires = DateTime.UtcNow.AddYears(-1),
                Path = "/api/auth/refresh"
            });
        token.RefreshToken = string.Empty;
        await _unitOfWork.Tokens.Update(token);
        await _unitOfWork.CompleteAsync();

        return Unauthorized();
    }
    
    [AllowAnonymous]
    [HttpGet("refresh")]
    public async Task<IActionResult> Refresh()
    {
        var userId = _userIdentifire.GetIdByCookie(Request);
        var accessToken = Request.Cookies["X-Access-Token"];
        
        if (string.IsNullOrEmpty(accessToken))
        {
            return Unauthorized();
        }
        
        var token = await _unitOfWork.Tokens.GetUserToken(userId);

        if (string.IsNullOrEmpty(token.RefreshToken))
        {
            return Unauthorized();
        }
        
        var user = await _unitOfWork.Users.GetByIdAsync(userId);

        if (!IsValidToken(token.RefreshToken, false))
        {
            token.RefreshToken = string.Empty;
            await _unitOfWork.Tokens.Update(token);
            await _unitOfWork.CompleteAsync();
            
            return Unauthorized();
        }

        if (!IsValidToken(accessToken, true))
        {
            accessToken = CreateToken(user, true);
            
            SetCookie(accessToken);
            
            return Ok(new
            {
                User = _mapper.Map<UserAuthResponse>(user),
                accessToken
            });
        }
        
        return Ok(new
        {
            User = _mapper.Map<UserAuthResponse>(user),
            accessToken,
        });
    }
}