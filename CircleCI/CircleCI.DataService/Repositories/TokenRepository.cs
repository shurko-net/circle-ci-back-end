using AutoMapper;
using CircleCI.DataService.Data;
using CircleCI.DataService.Repositories.Interfaces;
using CircleCI.Entities.DbSet;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CircleCI.DataService.Repositories;

public class TokenRepository : GenericRepository<Token>, ITokenRepository
{
    public TokenRepository(AppDbContext context,
        ILogger logger,
        IMapper mapper) : base(context, logger, mapper)
    { }

    public async Task<bool> Update(Token token)
    {
        try
        {
            var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == token.Id);

            if (result == null)
                return false;

            result.RefreshToken = token.RefreshToken;

            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "{Repo} Update function error", typeof(TokenRepository));
            throw;
        }
    }

    public async Task<Token> GetUserToken(int userId)
    {
        try
        {
            var result = await _dbSet.FirstOrDefaultAsync(x => x.UserId == userId);
            
            if (result == null)
                return new Token()
                {
                    RefreshToken = string.Empty
                };

            return result;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "{Repo} GetUserToken function error", typeof(TokenRepository));
            throw;
        }
    }

    public async Task<Token> GetTokenByValue(string token)
    {
        try
        {
            var result = await _dbSet.FirstOrDefaultAsync(x => x.RefreshToken == token);

            if (result == null)
                return new Token()
                {
                    RefreshToken = string.Empty
                };

            return result;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "{Repo} GetTokenByValue function error", typeof(TokenRepository));
            throw;
        }
    }
}