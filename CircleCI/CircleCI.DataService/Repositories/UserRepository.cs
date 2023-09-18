using AutoMapper;
using CircleCI.DataService.Data;
using CircleCI.DataService.Repositories.Interfaces;
using CircleCI.Entities.DbSet;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CircleCI.DataService.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(AppDbContext context,
        ILogger logger,
        IMapper mapper) : base(context, logger, mapper)
    { }

    public async Task<bool> UpdateAsync(User user)
    {
        try
        {
            var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == user.Id);

            if (result == null)
                return false;

            result.Biography = user.Biography;
            result.Name = user.Name;
            result.Surname = user.Surname;
            result.TNumber = user.TNumber;
            result.FollowersAmount = user.FollowersAmount;

            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "{Repo} Update function error", typeof(UserRepository));
            throw;
        }
    }

    public async Task<User?> GetByIdAsync(int id)
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
            _logger.LogError(e, "{Repo} GetById function error", typeof(UserRepository));
            throw;
        }
    }

    public async Task<User?> GetByEmailAsync(string userEmail)
    {
        try
        {
            var result = await _dbSet.AsSplitQuery()
                .Include(x => x.Token)
                .FirstOrDefaultAsync(x => x.Email == userEmail);
            
            if (result == null)
                return new User();

            return result;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "{Repo} GetUserByEmail function error", typeof(UserRepository));
            throw;
        }
    }

    public async Task<IEnumerable<User?>> SearchByInitialAsync(int? page, string query)
    {
        try
        {
            const int pageSize = 5;
            var result = await _dbSet.Where(u => u.Name.Contains(query) 
                                                 || u.Surname.Contains(query))
                .Skip((page ?? 0) * pageSize)
                .Take(5)
                .AsSplitQuery()
                .ToListAsync();

            if (!result.Any())
            {
                return new List<User?>();
            }

            return result;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "{Repo} SearchByInitial function error", typeof(UserRepository));
            throw;
        }
    }
}