using CircleCI.Entities.DbSet;

namespace CircleCI.DataService.Repositories.Interfaces;

public interface ILikeRepository : IGenericRepository<Like>
{
    Task<bool> IsLiked(int userId,int postId);
    Task<Like?> GetById(int userId,int postId);
}