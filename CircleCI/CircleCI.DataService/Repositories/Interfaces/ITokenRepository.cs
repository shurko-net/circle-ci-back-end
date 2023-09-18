using CircleCI.Entities.DbSet;

namespace CircleCI.DataService.Repositories.Interfaces;

public interface ITokenRepository : IGenericRepository<Token>
{
    Task<bool> Update(Token token);
    Task<Token> GetUserToken(int userId);
    Task<Token> GetTokenByValue(string token);
}