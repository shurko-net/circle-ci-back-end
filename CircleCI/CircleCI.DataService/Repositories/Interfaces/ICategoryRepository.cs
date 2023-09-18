using CircleCI.Entities.DbSet;

namespace CircleCI.DataService.Repositories.Interfaces;

public interface ICategoryRepository : IGenericRepository<Category>
{
    Task<IEnumerable<Category>> AllPostTags(int postId);
    Task<bool> AddRange(IEnumerable<Category> categories);
    Task<IEnumerable<Category>> TagSearch(int postId);
}