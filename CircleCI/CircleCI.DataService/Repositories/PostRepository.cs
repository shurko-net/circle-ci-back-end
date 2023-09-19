using AutoMapper;
using CircleCI.DataService.Data;
using CircleCI.DataService.Repositories.Interfaces;
using CircleCI.Entities.DbSet;
using CircleCI.Entities.DTOs.Responses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CircleCI.DataService.Repositories;

public class PostRepository : GenericRepository<Post>, IPostRepository 
{
    public PostRepository(AppDbContext context,
        ILogger logger,
        IMapper mapper) : base(context, logger, mapper)
    { }
    public async Task<GetPostResponse?> GetByIdMapped(int postId, int userId)
    {
        try
        {
            var temp = await _dbSet.Include(p => p.User)
                .Include(p => p.Category)
                .AsSplitQuery()
                .FirstOrDefaultAsync(p => p.Id == postId);

            if (temp == null)
                return null;
            
            var result = _mapper.Map<GetPostResponse>(temp);
            
            result.LikesAmount = await _context.Likes.CountAsync(c => c.PostId == result.Id);
            result.CommentsAmount = await _context.Comments.CountAsync(c => c.PostId == result.Id);
            result.IsPostOwner = result.UserId == userId;
            result.IsLiked = await _context.Likes.AnyAsync(l => l.PostId == result.Id &&
                                                                l.UserId == userId);
            result.IsMyself = result.IsPostOwner;
            result.IsSaved = await _context.Saves.AnyAsync(l => l.PostId == result.Id &&
                                                                l.UserId == userId);
            
            return result;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "{Repo} GetByIdMapped function error", typeof(PostRepository));
            throw;
        }
    }

    public async Task<Post?> GetById(int id)
    {
        try
        {
            var result = await _dbSet.FindAsync(id);

            if (result == null)
                return null;
            
            return result;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "{Repo} GetById function error", typeof(PostRepository));
            throw;
        }
    }

    public async Task<int> PostsAmount()
    {
        try
        {
            return await _dbSet.CountAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "{Repo} PostsAmount function error", typeof(PostRepository));
            throw;
        }
    }

    public async Task<bool> Update(Post post)
    {
        try
        {
            var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == post.Id);

            if (result == null)
                return false;

            result.Content = post.Content;
            result.CreatedAt = DateTime.UtcNow;
            result.Title = post.Title;
            result.LikesAmount = post.LikesAmount;
            
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "{Repo} Update function error", typeof(PostRepository));
            throw;
        }
    }

    public async Task<IEnumerable<GetPostResponse?>> KeysetPage(int? page, int userId)
    {
        try
        {
            const int pageSize = 5;
            var temp = await _dbSet.OrderByDescending(b => b.Id)
                .Skip((page ?? 0) * pageSize)
                .Include(b => b.Category)
                .Include(b => b.User)
                .Take(pageSize)
                .AsSplitQuery()
                .ToListAsync();
            
            var mappedPosts = _mapper.Map<IEnumerable<GetPostResponse>>(temp);
            
            foreach (var post in mappedPosts)
            {
                post.LikesAmount = await _context.Likes.CountAsync(c => c.PostId == post.Id);
                post.CommentsAmount = await _context.Comments.CountAsync(c => c.PostId == post.Id);
                post.IsPostOwner = post.UserId == userId;
                post.IsLiked = await _context.Likes.AnyAsync(l => l.PostId == post.Id &&
                                                                  l.UserId == userId);
                post.IsMyself = post.IsPostOwner;
                post.IsSaved = await _context.Saves.AnyAsync(l => l.PostId == post.Id &&
                                                                  l.UserId == userId);
            }
            
            return mappedPosts;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "{Repo} KeysetPage function error", typeof(PostRepository));
            throw;
        }
    }
}