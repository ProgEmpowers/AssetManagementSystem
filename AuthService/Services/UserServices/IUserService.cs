using AssetManagementSystem.Models;
using AuthService.Models.Domains;
using AuthService.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Services.UserServices
{
    public interface IUserService 
    {

        Task<string> GenerateUserIdAsync();
        Task<IEnumerable<UserDto>> GetAllUsersAsync();

        Task<User?> DeleteUserAsync(string id);
        Task<User?> GetUserByIdAsync(string id);
        Task<User?> UpdateUserAsync(string id, UserUpdateDto userUpdateDto);

        Task<UserAsset> AssignAssetToUserAsync(UserAsset userAsset);

        Task<List<int>> GetAssetIdsByUserIdAsync(string userId);


    }
}
