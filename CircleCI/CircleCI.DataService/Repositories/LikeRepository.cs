using AutoMapper;
using CircleCI.DataService.Data;
using CircleCI.DataService.Repositories.Interfaces;
using CircleCI.Entities.DbSet;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CircleCI.DataService.Repositories;

public class LikeRepository : GenericRepository<Like>, ILikeRepository
{
    public LikeRepository(AppDbContext context,
        ILogger logger,
        IMapper mapper) : base(context, logger, mapper)
    { }

    public async Task<bool> IsLiked(int userId, int postId)
    {
        try
        {
            var like = await _dbSet.FirstOrDefaultAsync(l => l.PostId == postId);

            if (like == null)
                return false;

            return like.UserId == userId;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "{Repo} IsLiked function error", typeof(LikeRepository));
            throw;
        }
    }

    public async Task<Like?> GetById(int userId, int postId)
    {
        try
        {
            var follow = await _dbSet
                .Where(l => l.PostId == postId &&
                            l.UserId == userId)
                .FirstOrDefaultAsync();
            
            return follow;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "{Repo} GetById function error", typeof(LikeRepository));
            throw;
        }
    }
}