using AutoMapper;
using CircleCI.Api.Services.ImageStorageService;
using CircleCI.Api.Services.TokenService;
using CircleCI.DataService.Repositories.Interfaces;
using CircleCI.Entities.DbSet;
using CircleCI.Entities.DTOs.Requests;
using CircleCI.Entities.DTOs.Responses;
using Microsoft.AspNetCore.Mvc;

namespace CircleCI.Api.Controllers;

public class PostController : BaseController
{
    private readonly ICloudStorage _cloudStorage;
    public PostController(IUnitOfWork unitOfWork,
        IUserIdentifire userIdentifire,
        IMapper mapper,
        ICloudStorage cloudStorage) : base(unitOfWork, mapper, userIdentifire)
    {
        _cloudStorage = cloudStorage;
    }

    [HttpGet("get-post/{postId}")]
    public async Task<IActionResult> GetPost(int postId)
    {
        var userId = _userIdentifire.GetIdByToken(Request);
        var post = await _unitOfWork.Posts.GetByIdMapped(postId, userId);

        if (post == null)
            return NotFound("Post not found");

        return Ok(post);
    }

    [HttpGet("get-posts/{page?}")]
    public async Task<IActionResult> GetPosts(int page = 0)
    {
        var userId = _userIdentifire.GetIdByToken(Request);
        var posts = await _unitOfWork.Posts.KeysetPage(page, userId);
        int postAmount = await _unitOfWork.Posts.PostsAmount();
        Response.Headers.Add("x-total-count", postAmount.ToString());
        
        if (!posts.Any())
        {
            return NotFound("Posts not found");
        }
        
        return Ok(posts);
    }
    [HttpPost("create-post")]
    public async Task<IActionResult> CreatePost([FromBody] CreatePostRequest request)
    {
        if (ModelState.IsValid)
        {
            var userId = _userIdentifire.GetIdByToken(Request);
            
            if (userId == 0)
            {
                return Unauthorized();
            }
            var post = new Post()
            {
                UserId = userId,
                CreatedAt = DateTime.UtcNow,
                Title = request.Title,
                Content = request.Content,
                ImageUrl = await _cloudStorage.UploadFileAsync(request.ImageUrl, _cloudStorage.GetFileName()),
                Category = request.Categories.Select(categoryName => new Category()
                {
                    Name = categoryName
                }).ToList()
            };

            await _unitOfWork.Posts.Add(post);
            await _unitOfWork.CompleteAsync();

            return Ok(_mapper.Map<GetPostResponse>(post));
        }

        return BadRequest();
    }
    [HttpPut("like/{postId}")]
    public async Task<IActionResult> LikeOnPost(int postId)
    {
        var userId = _userIdentifire.GetIdByToken(Request);
        var like = await _unitOfWork.Likes.GetById(userId, postId);
        var post = await _unitOfWork.Posts.GetById(postId);

        GetPostResponse? mappedPost = new();
            
        if (post == null)
            return NotFound("Post not found");
            
        if (like == null)
        {
            post.LikesAmount++;
            await _unitOfWork.Likes.Add(new Like
            {
                PostId = post.Id,
                UserId = userId
            });
            await _unitOfWork.Posts.Update(post);
            await _unitOfWork.CompleteAsync();

            mappedPost = await _unitOfWork.Posts.GetByIdMapped(post.Id, userId);
                
            return Ok(mappedPost);
        }

        post.LikesAmount--;
        await _unitOfWork.Likes.Delete(like.Id);
        await _unitOfWork.Posts.Update(post);
        await _unitOfWork.CompleteAsync();

        mappedPost = await _unitOfWork.Posts.GetByIdMapped(post.Id, userId);

        return Ok(mappedPost);
    }
    
    [HttpPut("save/{postId}")]
    public async Task<IActionResult> SavePost(int postId)
    {
        var userId = _userIdentifire.GetIdByToken(Request);
        var save = await _unitOfWork.Saves.GetById(userId, postId);
        var post = await _unitOfWork.Posts.GetById(postId);

        GetPostResponse? mappedPost = new();
            
        if (post == null)
            return NotFound("Post not found");
            
        if (save == null)
        {
            await _unitOfWork.Saves.Add(new Saved
            {
                PostId = post.Id,
                UserId = userId
            });
            await _unitOfWork.CompleteAsync();

            mappedPost = await _unitOfWork.Posts.GetByIdMapped(post.Id, userId);
                
            return Ok(mappedPost);
        }
        
        await _unitOfWork.Saves.Delete(save.Id);
        await _unitOfWork.CompleteAsync();

        mappedPost = await _unitOfWork.Posts.GetByIdMapped(post.Id, userId);

        return Ok(mappedPost);
    }
}