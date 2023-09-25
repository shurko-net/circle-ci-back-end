using AutoMapper;
using CircleCI.Api.Services.TokenService;
using CircleCI.DataService.Repositories.Interfaces;
using CircleCI.Entities.DbSet;
using CircleCI.Entities.DTOs.Responses;
using Microsoft.AspNetCore.Mvc;

namespace CircleCI.Api.Controllers;

[ApiController]
[Route("api")]
public class UserController : BaseController
{
    public UserController(IUnitOfWork unitOfWork,
        IMapper mapper,
        IUserIdentifire userIdentifire) 
        : base(unitOfWork, mapper, userIdentifire)
    {
    }

    [HttpGet("get-user/{userId?}")]
    public async Task<IActionResult> GetUser(int userId = 0)
    {
        if (userId == 0)
        {
            var owner = await _unitOfWork.Users.GetByIdAsync(_userIdentifire.GetIdByHeader(HttpContext));

            return Ok(_mapper.Map<UserResponse>(owner));
        }
        
        var user = await _unitOfWork.Users.GetByIdAsync(userId);

        if (user == null)
            return NotFound("User not found");
        
        return Ok(_mapper.Map<UserResponse>(user));
    }
    
    [HttpGet("is-sub/{followedId}")]
    public async Task<IActionResult> IsSubscribe(int followedId)
    {
        var userId = _userIdentifire.GetIdByHeader(HttpContext);

        return Ok(new
        {
            IsOwner = userId == followedId,
            IsFollowed = await _unitOfWork.Follows.IsSubscriber(userId, followedId)
        });
    }

    [HttpPut("follow/{followableId}")]
    public async Task<IActionResult> SubscribeUser(int followableId)
    {
        var userId = _userIdentifire.GetIdByHeader(HttpContext);
        var follow = await _unitOfWork.Follows.GetById(userId, followableId);
        var user = await _unitOfWork.Users.GetByIdAsync(followableId);
        UserResponse mappedUser;

        if (user == null)
            return NotFound("User doesnt exist");
        
        if (follow == null)
        {
            user.FollowersAmount++;
            await _unitOfWork.Follows.Add(new Follow()
            {
                FollowedUserId = user.Id,
                FollowerUserId = userId
            });
            await _unitOfWork.Users.UpdateAsync(user);
            await _unitOfWork.CompleteAsync();

            mappedUser = _mapper.Map<UserResponse>(user);
            
            return Ok(new
            {
                mappedUser,
                IsFollowed = true
            });
        }

        user.FollowersAmount--;
        await _unitOfWork.Follows.Delete(follow.Id);
        await _unitOfWork.Users.UpdateAsync(user);
        await _unitOfWork.CompleteAsync();
        
        mappedUser = _mapper.Map<UserResponse>(user);
        
        return Ok(new
        {
            mappedUser,
            IsFollowed = false
        });
    }

    [HttpGet("search-user/{page?}/{query?}")]
    public async Task<IActionResult> SearchUser(int page = 0,string query = "")
    {
        if (string.IsNullOrEmpty(query))
        {
            return NotFound("Empty query string");
        }

        var list = await _unitOfWork.Users.SearchByInitialAsync(page, query);

        if (!list.Any())
        {
            return NotFound("Users not found");
        }
        
        return Ok(_mapper.Map<IEnumerable<UserResponse>>(list));
    }
}