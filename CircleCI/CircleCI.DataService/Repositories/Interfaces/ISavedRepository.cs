using CircleCI.Entities.DbSet;

namespace CircleCI.DataService.Repositories.Interfaces;

public interface ISavedRepository : IGenericRepository<Saved>
{
    Task<bool> IsSaved(int userId,int postId);
    Task<Saved?> GetById(int userId,int postId);
}