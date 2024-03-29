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
                .ThenInclude(p => p.Followers)
                .Include(p => p.Category)
                .ThenInclude(p => p.CategoryList)
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
            result.IsSaved = await _context.Saves.AnyAsync(l => l.PostId == result.Id &&
                                                                l.UserId == userId);
            result.IsFollow = temp.User.Followers.Any(u => u.FollowerUserId == userId
                                       && u.FollowedUserId == result.UserId);
            
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
            result.Title = post.Title;
            result.ImageUrl = post.ImageUrl;
            result.LikesAmount = post.LikesAmount;
            result.ViewsAmount = post.ViewsAmount;
            
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "{Repo} Update function error", typeof(PostRepository));
            throw;
        }
    }

    public async Task<IEnumerable<GetPostResponse?>> KeySetPage(
        int? page, int userId, bool isProfile = false, bool isSaved = false)
    {
        try
        {
            const int pageSize = 5;
            
            var temp = await _dbSet.OrderByDescending(b => b.Id)
                .Skip((page ?? 0) * pageSize)
                .Include(b => b.User)
                .Include(b => b.Category)
                .ThenInclude(b => b.CategoryList)
                .Take(pageSize)
                .ToListAsync();

            if(isProfile)
                temp = temp.Where(p => p.UserId == userId).ToList();
            
            var mappedPosts = _mapper.Map<IEnumerable<GetPostResponse>>(temp).ToList();
            
            foreach (var pair in temp.Zip(mappedPosts, (pt, mpt) => new { Pt = pt, Mpt = mpt}))
            {
                pair.Mpt.LikesAmount = await _context.Likes.CountAsync(c => c.PostId == pair.Mpt.Id);
                pair.Mpt.CommentsAmount = await _context.Comments.CountAsync(c => c.PostId == pair.Mpt.Id);
                pair.Mpt.IsPostOwner = pair.Mpt.UserId == userId;
                pair.Mpt.IsLiked = await _context.Likes.AnyAsync(l => l.PostId == pair.Mpt.Id &&
                                                                      l.UserId == userId);
                pair.Mpt.IsSaved = await _context.Saves.AnyAsync(l => l.PostId == pair.Mpt.Id &&
                                                                      l.UserId == userId);
                pair.Mpt.IsFollow = await _context.Follows.AnyAsync(f => f.FollowerUserId == pair.Mpt.UserId
                                                                         && f.FollowedUserId == userId);
            }

            if (isSaved)
            {
                return mappedPosts.Where(p => p.IsSaved).ToList();
            }
            
            return mappedPosts;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "{Repo} KeySetPage function error", typeof(PostRepository));
            throw;
        }
    }
}