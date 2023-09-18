using AutoMapper;
using CircleCI.DataService.Data;
using CircleCI.DataService.Repositories.Interfaces;
using CircleCI.Entities.DbSet;
using CircleCI.Entities.DTOs.Requests;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CircleCI.DataService.Repositories;

public class CommentRepository : GenericRepository<Comment>, ICommentRepository
{
    public CommentRepository(AppDbContext context,
        ILogger logger,
        IMapper mapper) : base(context, logger, mapper)
    { }
    
    public async Task<IEnumerable<Comment>> AllPostComments(int postId)
    {
        try
        {
            return await _dbSet.Where(x => x.PostId == postId)
                .Include(p => p.User)
                .AsSplitQuery()
                .ToListAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "{Repo} AllPostComments function error", typeof(CommentRepository));
            throw;
        }
    }
    
    public async Task<bool> Update(Comment comment)
    {
        try
        {
            var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == comment.Id);

            if (result == null)
                return false;

            result.Content = comment.Content;
            result.CreatedAt = DateTime.UtcNow;

            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "{Repo} Update function error", typeof(CommentRepository));
            throw;
        }
    }
}