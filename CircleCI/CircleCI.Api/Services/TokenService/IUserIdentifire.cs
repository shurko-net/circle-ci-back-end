namespace CircleCI.Api.Services.TokenService;

public interface IUserIdentifire
{
    int GetIdByToken(HttpRequest request);
}