namespace CircleCI.DataService.Repositories.Interfaces;

public interface IUnitOfWork
{
    ICategoryRepository Categories { get; }
    ICommentRepository Comments { get; }
    IFollowRepository Follows { get; }
    ILikeRepository Likes { get; }
    IPostRepository Posts { get; }
    ITokenRepository Tokens { get; }
    IUserRepository Users { get; }
    ISavedRepository Saves { get; }
    ICategoryListRepository CategoriesList { get; }

    Task<bool> CompleteAsync();
}