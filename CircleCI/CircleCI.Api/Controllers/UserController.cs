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
        var ownerId = _userIdentifire.GetIdByHeader(HttpContext);
        
        if (userId == 0)
        {
            var owner = await _unitOfWork.Users.GetUserProfileAsync(ownerId, ownerId);

            return Ok(owner);
        }
        
        var user = await _unitOfWork.Users.GetUserProfileAsync(ownerId, userId);

        if (user == null)
            return NotFound("User not found");

        return Ok(user);
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
        var ownerId = _userIdentifire.GetIdByHeader(HttpContext);
        
        if (followableId == ownerId)
            return BadRequest("Сan`t subscribe to yourself");
        
        var follow = await _unitOfWork.Follows.GetById(followableId, ownerId);
        var user = await _unitOfWork.Users.GetByIdAsync(followableId);
        UserProfileResponse? response;
        
        if (user == null)
            return NotFound("User doesnt exist");
        
        if (follow == null)
        {
            user.FollowersAmount++;
            await _unitOfWork.Follows.Add(new Follow()
            {
                FollowerUserId = user.Id,
                FollowedUserId = ownerId
            });
            await _unitOfWork.Users.UpdateAsync(user);
            await _unitOfWork.CompleteAsync();

            response = await _unitOfWork.Users.GetUserProfileAsync(ownerId, followableId);

            return Ok(response);
        }

        user.FollowersAmount--;
        await _unitOfWork.Follows.Delete(follow.Id);
        await _unitOfWork.Users.UpdateAsync(user);
        await _unitOfWork.CompleteAsync();
        
        response = await _unitOfWork.Users.GetUserProfileAsync(ownerId, followableId);

        return Ok(response);
    }

    [HttpGet("get-popular-people")]
    public async Task<IActionResult> GetPopularPeople()
    {
        var users = await _unitOfWork.Users
            .GetPopularUserAsync(
                _userIdentifire.GetIdByHeader(HttpContext));

        if (!users.Any())
            return NotFound("Popular users doesnt found");
        
        return Ok(users);
    }
    
    [HttpGet("search-user/{page?}/{query?}")]
    public async Task<IActionResult> SearchUser(int page = 0, string query = "")
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

    [HttpGet("get-saved-posts/{page?}")]
    public async Task<IActionResult> GetSavedPost(int page = 0)
    {
        var userId = _userIdentifire.GetIdByHeader(HttpContext);
        var posts = await _unitOfWork.Posts.KeySetPage(page, userId, true, true);

        if (!posts.Any())
        {
            return NotFound("Saved posts not found");
        }

        return Ok(posts);
    }
}