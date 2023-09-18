using CircleCI.Entities.DbSet;

namespace CircleCI.DataService.Repositories.Interfaces;

public interface IUserRepository : IGenericRepository<User>
{
    Task<bool> UpdateAsync(User user);
    Task<User?> GetByIdAsync(int id);
    Task<User?> GetByEmailAsync(string userEmail);
    Task<IEnumerable<User?>> SearchByInitialAsync(int? page, string query);
}