using AutoMapper;
using CircleCI.DataService.Data;
using CircleCI.DataService.Repositories.Interfaces;
using CircleCI.Entities.DbSet;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CircleCI.DataService.Repositories;

public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
{
    public CategoryRepository(AppDbContext context,
        ILogger logger,
        IMapper mapper) : base(context, logger, mapper)
    { }

    public async Task<IEnumerable<Category>> AllPostTags(int postId)
    {
        try
        {
            return await _dbSet.Where(x => x.PostId == postId)
                .AsNoTracking()
                .AsSplitQuery()
                .ToListAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "{Repo} AllPostTags function error", typeof(CategoryRepository));
            throw;
        }
    }

    public async Task<bool> AddRange(IEnumerable<Category> categories)
    {
        await _dbSet.AddRangeAsync(categories);
        return true;
    }

    // TODO - Добавить функционал поиска
    public async Task<IEnumerable<Category>> TagSearch(int postId)
    {
        try
        {
            return new List<Category>();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "{Repo} TagSearch function error", typeof(CategoryRepository));
            throw;
        }
    }
}