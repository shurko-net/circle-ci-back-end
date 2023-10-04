using AutoMapper;
using CircleCI.DataService.Data;
using CircleCI.DataService.Repositories.Interfaces;
using CircleCI.Entities.DbSet;
using CircleCI.Entities.DTOs.Responses;
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
            var result = await _dbSet.Where(u => u.Name.StartsWith(query) 
                                                 || u.Surname.StartsWith(query))
                .OrderBy(u => u.Name)
                .ThenBy(u => u.Surname)
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

    public async Task<UserProfileResponse?> GetUserProfileAsync(int requestUserId, int ownerId)
    {
        try
        {
            var user = await _dbSet.FirstOrDefaultAsync(u => u.Id == requestUserId);

            if (user == null)
                return null;

            var result = _mapper.Map<UserProfileResponse>(user);
            result.CommentsAmount = await _context.Comments.CountAsync(u => u.UserId == ownerId);
            result.PostsAmount = await _context.Posts.CountAsync(u => u.UserId == ownerId);
            result.IsMyself = requestUserId == ownerId;
            result.IsFollowed = await _context.Follows.AnyAsync(u => u.FollowerUserId == ownerId
                                                                     && u.FollowedUserId == requestUserId);
            
            return result;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "{Repo} GetUserProfileAsync function error", typeof(UserRepository));
            throw;
        }
    }

    public async Task<IEnumerable<User>> GetPopularUserAsync()
    {
        try
        {
            var users = await _dbSet.OrderBy(u => u.Followers)
                .Take(5)
                .AsSplitQuery().ToListAsync();

            return users;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "{Repo} GetPopularUser function error", typeof(UserRepository));
            throw;
        }
    }
}