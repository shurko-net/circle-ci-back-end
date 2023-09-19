using CircleCI.Entities.DbSet;

namespace CircleCI.DataService.Repositories.Interfaces;

public interface ICategoryListRepository : IGenericRepository<CategoryList>
{
    Task<IEnumerable<CategoryList>> GetCategoriesByIdAsync(IEnumerable<int> list);
    Task<IEnumerable<CategoryList>> SearchTagsAsync(string query);
}