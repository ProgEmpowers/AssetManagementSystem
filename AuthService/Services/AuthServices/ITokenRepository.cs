using AssetManagementSystem.Models;
using Microsoft.AspNetCore.Identity;

namespace AuthService.Services.AuthServices
{
    public interface ITokenRepository
    {
        string CreateJwtToken(User user, List<string> roles);
    }
}
