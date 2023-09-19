using AutoMapper;
using CircleCI.DataService.Data;
using CircleCI.DataService.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace CircleCI.DataService.Repositories;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly AppDbContext _context;
    public ICategoryRepository Categories { get; }
    public ICommentRepository Comments { get; }
    public IFollowRepository Follows { get; }
    public ILikeRepository Likes { get; }
    public IPostRepository Posts { get; }
    public ITokenRepository Tokens { get; }
    public IUserRepository Users { get; }
    public ISavedRepository Saves { get; }
    public ICategoryListRepository CategoriesList { get; }

    public UnitOfWork(AppDbContext context, ILoggerFactory loggerFactory, IMapper mapper)
    {
        _context = context;
        var logger = loggerFactory.CreateLogger("logs");

        Categories = new CategoryRepository(_context, logger, mapper);
        Follows = new FollowRepository(_context, logger, mapper);
        Comments = new CommentRepository(_context, logger, mapper);
        Likes = new LikeRepository(_context, logger, mapper);
        Posts = new PostRepository(_context, logger, mapper);
        Tokens = new TokenRepository(_context, logger, mapper);
        Users = new UserRepository(_context, logger, mapper);
        Saves = new SavedRepository(_context, logger, mapper);
        CategoriesList = new CategoryListRepository(_context, logger, mapper);
    }
    
    public async Task<bool> CompleteAsync()
    {
        var result = await _context.SaveChangesAsync();
        
        return result > 0;
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}