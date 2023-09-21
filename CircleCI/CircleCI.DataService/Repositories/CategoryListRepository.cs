using AutoMapper;
using CircleCI.DataService.Data;
using CircleCI.DataService.Repositories.Interfaces;
using CircleCI.Entities.DbSet;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CircleCI.DataService.Repositories;

public class CategoryListRepository : GenericRepository<CategoryList>, ICategoryListRepository
{
    public CategoryListRepository(AppDbContext context, ILogger logger, IMapper mapper) : base(context, logger, mapper)
    { }

    public async Task<IEnumerable<CategoryList>> GetCategoriesByIdAsync(IEnumerable<int> list)
    {
        try
        {
            return await _dbSet.Where(c => list.Contains(c.Id))
                .AsNoTracking()
                .ToListAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "{Repo} GetCategoriesByIdAsync function error", typeof(CategoryListRepository));
            throw;
        }
    }

    public async Task<IEnumerable<CategoryList>> SearchTagsAsync(string query)
    {
        try
        {
            return await _dbSet.Where(c => c.Name.Contains(query))
                .OrderByDescending(c => c.Name.StartsWith(query))
                .ThenBy(c => c)
                .AsNoTracking()
                .ToListAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "{Repo} GetCategoriesByIdAsync function error", typeof(CategoryListRepository));
            throw;
        }
    }
}