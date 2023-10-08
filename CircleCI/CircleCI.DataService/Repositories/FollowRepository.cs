using AutoMapper;
using CircleCI.DataService.Data;
using CircleCI.DataService.Repositories.Interfaces;
using CircleCI.Entities.DbSet;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CircleCI.DataService.Repositories;

public class FollowRepository : GenericRepository<Follow>, IFollowRepository
{
    public FollowRepository(AppDbContext context,
        ILogger logger,
        IMapper mapper) : base(context, logger, mapper)
    { }

    public async Task<bool> IsSubscriber(int userId,int followedId)
    {
        try
        {
            var follow = await _dbSet.FirstOrDefaultAsync(f => f.FollowedUserId == followedId);

            if (follow == null)
                return false;

            return follow.FollowerUserId == userId;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "{Repo} IsSubscriber function error", typeof(FollowRepository));
            throw;
        }
    }

    public async Task<Follow?> GetById(int userId, int followedId)
    {
        
        try
        {
            var follow = await _dbSet
                .Where(f => f.FollowerUserId == userId &&
                            f.FollowedUserId == followedId)
                .FirstOrDefaultAsync();
            
            return follow;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "{Repo} GetById function error", typeof(FollowRepository));
            throw;
        }
    }
}