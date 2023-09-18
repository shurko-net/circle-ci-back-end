using AutoMapper;
using CircleCI.DataService.Data;
using CircleCI.DataService.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CircleCI.DataService.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    public readonly ILogger _logger;
    public readonly AppDbContext _context;
    public readonly IMapper _mapper;
    internal DbSet<T> _dbSet;

    public GenericRepository(
        AppDbContext context,
        ILogger logger,
        IMapper mapper)
    {
        _logger = logger;
        _context = context;
        _dbSet = context.Set<T>();
        _mapper = mapper;
    }
    public async Task<bool> Add(T entity)
    {
        await _dbSet.AddAsync(entity);
        return true;
    }

    public async Task<bool> Delete(int entityId)
    {
        var result = await _dbSet.FindAsync(entityId);
        
        if (result == null)
            return false;
        
        _dbSet.Remove(result);
        return true;
    }
}