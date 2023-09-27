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
        var userId = _userIdentifire.GetIdByHeader(HttpContext);
        var post = await _unitOfWork.Posts.GetByIdMapped(postId, userId);

        if (post == null)
            return NotFound("Post not found");

        return Ok(post);
    }

    [HttpGet("get-posts/{page?}")]
    public async Task<IActionResult> GetPosts(int page = 0)
    {
        var userId = _userIdentifire.GetIdByHeader(HttpContext);
        var posts = await _unitOfWork.Posts.KeySetPage(page, userId, false);
        int postAmount = await _unitOfWork.Posts.PostsAmount();
        Response.Headers.Add("x-total-count", postAmount.ToString());
        
        if (!posts.Any())
        {
            return NotFound("Posts not found");
        }
        
        return Ok(posts);
    }

    [HttpGet("get-user-posts/{page?}")]
    public async Task<IActionResult> GetUserPosts(int page = 0)
    {
        var userId = _userIdentifire.GetIdByHeader(HttpContext);
        var posts = await _unitOfWork.Posts.KeySetPage(page, userId, true);
        var postAmount = await _unitOfWork.Posts.PostsAmount();
        Response.Headers.Add("x-total-count", postAmount.ToString());
        
        if (!posts.Any())
        {
            return NotFound("Posts not found");
        }
        
        return Ok(posts);
    }
    
    [HttpPost("create-post")]
    public async Task<IActionResult> CreatePost([FromForm] CreatePostRequest request)
    {
        if (ModelState.IsValid)
        {
            var userId = _userIdentifire.GetIdByHeader(HttpContext);
            
            if (userId == 0)
            {
                return Unauthorized();
            }
            
            var post = _mapper.Map<Post>(request);
            List<int> categories = new();
            try
            {
                categories = new List<int>(request.Categories
                    .Split(",")
                    .Select(int.Parse)
                    .ToList());
            }
            catch(FormatException)
            {
                return BadRequest("Categories has invalid values");
            }

            if (request.File == null)
            {
                post.ImageUrl = string.Empty;
            }
            else
            {
                post.ImageUrl = await _cloudStorage.UploadFileAsync(request.File, _cloudStorage.GetFileName());
            }

            var category = await _unitOfWork.CategoriesList.GetCategoriesByIdAsync(categories);
            post.UserId = userId;
            post.Category = category.Select(c => new Category()
            {
                CategoryListId = c.Id,
            }).ToList();
            await _unitOfWork.Posts.Add(post);
            await _unitOfWork.CompleteAsync();
            
            post.Category = category.Select(c => new Category()
            {
                CategoryList = c
            }).ToList();
            var response = _mapper.Map<GetPostResponse>(post);
            
            return Ok(response);
        }

        return BadRequest();
    }
    
    [HttpPut("like/{postId}")]
    public async Task<IActionResult> LikeOnPost(int postId)
    {
        var userId = _userIdentifire.GetIdByHeader(HttpContext);
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
        var userId = _userIdentifire.GetIdByHeader(HttpContext);
        var save = await _unitOfWork.Saves.GetById(userId, postId);
        var post = await _unitOfWork.Posts.GetById(postId);

        GetPostResponse? mappedPost;
            
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

    [HttpPut("update-views/{postId}")]
    public async Task<IActionResult> UpdateViews(int postId)
    {
        var post = await _unitOfWork.Posts.GetById(postId);

        if (post == null)
        {
            return NotFound("Post not found");
        }

        post.ViewsAmount++;
        await _unitOfWork.Posts.Update(post);
        await _unitOfWork.CompleteAsync();

        return Ok(new
        {
            post.Id,
            post.ViewsAmount
        });
    }
}