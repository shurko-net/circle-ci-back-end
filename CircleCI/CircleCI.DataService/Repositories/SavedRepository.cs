using AutoMapper;
using CircleCI.DataService.Data;
using CircleCI.DataService.Repositories.Interfaces;
using CircleCI.Entities.DbSet;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CircleCI.DataService.Repositories;

public class SavedRepository : GenericRepository<Saved>, ISavedRepository
{
    public SavedRepository(AppDbContext context, ILogger logger, IMapper mapper) : base(context, logger, mapper)
    { }

    public async Task<bool> IsSaved(int userId, int postId)
    {
        try
        {
            var save = await _dbSet.FirstOrDefaultAsync(l => l.PostId == postId);

            if (save == null)
                return false;

            return save.UserId == userId;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "{Repo} IsSaved function error", typeof(SavedRepository));
            throw;
        }
    }

    public async Task<Saved?> GetById(int userId, int postId)
    {
        try
        {
            var save = await _dbSet
                .Where(l => l.PostId == postId &&
                            l.UserId == userId)
                .FirstOrDefaultAsync();
            
            return save;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "{Repo} GetById function error", typeof(SavedRepository));
            throw;
        }
    }
}