using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CircleCI.Api.Services.TokenService;

public class UserIdentifire : IUserIdentifire
{
    public int GetIdByToken(HttpRequest request)
    {
        try
        {
            string accessToken = request.Cookies["X-Access-Token"];

            if (string.IsNullOrEmpty(accessToken))
                return 0;

            var jwt = new JwtSecurityTokenHandler().ReadJwtToken(accessToken);

            var claim = jwt.Claims.FirstOrDefault(i => i.Type == ClaimTypes.NameIdentifier);

            if (claim == null)
            {
                return 0;
            }

            int id = Convert.ToInt32(claim.Value);
            return id;
        }
        catch
        {
            return 0;
        }
    }
}