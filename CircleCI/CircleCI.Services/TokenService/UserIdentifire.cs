using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace CircleCI.Services.TokenService;

public class UserIdentifire : IUserIdentifire
{
    public int GetIdByCookie(HttpRequest request)
    {
        try
        {
            string? accessToken = request.Cookies["X-Access-Token"];

            if (string.IsNullOrEmpty(accessToken))
                return 0;

            var jwt = new JwtSecurityTokenHandler().ReadJwtToken(accessToken);

            var claim = jwt.Claims.FirstOrDefault(i => i.Type == ClaimTypes.NameIdentifier);

            if (claim == null)
            {
                return 0;
            }

            int id = Convert.ToInt32(claim.Value);
            return id;
        }
        catch
        {
            return 0;
        }
    }

    public int GetIdByHeader(HttpContext request)
    {
        var userId = request.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId))
            return 0;

        return Int32.TryParse(userId, out var id) ? id : 0;
    }
}