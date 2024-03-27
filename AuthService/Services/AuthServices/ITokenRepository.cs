using Microsoft.AspNetCore.Identity;

namespace AuthService.Services.AuthServices
{
    public interface ITokenRepository
    {
        string CreateJwtToken(IdentityUser user, List<string> roles);
    }
}
