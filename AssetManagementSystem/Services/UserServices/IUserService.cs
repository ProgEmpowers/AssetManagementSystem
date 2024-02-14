using AssetManagementSystem.Models.Dtos;
using AssetManagementSystem.Models;

namespace AssetManagementSystem.Services.UserServices
{
    public interface IUserService
    {
        Task<User> AddUserAsync(UserDto newUser);
    }
}
