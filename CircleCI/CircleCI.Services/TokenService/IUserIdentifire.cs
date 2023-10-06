using Microsoft.AspNetCore.Http;

namespace CircleCI.Services.TokenService;

public interface IUserIdentifire
{
    int GetIdByCookie(HttpRequest request);
    int GetIdByHeader(HttpContext request);
}