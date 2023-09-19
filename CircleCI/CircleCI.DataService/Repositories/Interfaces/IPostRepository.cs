using CircleCI.Entities.DbSet;
using CircleCI.Entities.DTOs.Responses;

namespace CircleCI.DataService.Repositories.Interfaces;

public interface IPostRepository : IGenericRepository<Post>
{
    Task<int> PostsAmount();
    Task<bool> Update(Post post);
    Task<GetPostResponse?> GetByIdMapped(int postId, int userId);
    Task<Post?> GetById(int id);
    Task<IEnumerable<GetPostResponse?>> KeySetPage(int? page, int userId);
}