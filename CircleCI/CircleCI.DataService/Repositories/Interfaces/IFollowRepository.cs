using CircleCI.Entities.DbSet;

namespace CircleCI.DataService.Repositories.Interfaces;

public interface IFollowRepository : IGenericRepository<Follow>
{
    Task<bool> IsSubscriber(int userId,int followedId);
    Task<Follow?> GetById(int userId,int followedId);
}