namespace CircleCI.Api.Services.TokenService;

public interface IUserIdentifire
{
    int GetIdByCookie(HttpRequest request);
    int GetIdByHeader(HttpContext request);
}