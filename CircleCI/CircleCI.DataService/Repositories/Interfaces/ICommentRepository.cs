using CircleCI.Entities.DbSet;
using CircleCI.Entities.DTOs.Requests;

namespace CircleCI.DataService.Repositories.Interfaces;

public interface ICommentRepository : IGenericRepository<Comment>
{
    Task<IEnumerable<Comment>> AllPostComments(int postId);
    Task<bool> Update(Comment comment);
}