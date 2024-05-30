using AssetManagementSystem.Models;
using AuthService.Models.Dtos;

namespace AuthService.Services.UserServices
{
    public interface IUserService 
    {
        Task<IEnumerable<UserDto>> GetAllUsersAsync();

        Task<User?> DeleteUserAsync(string id);
    }
}
