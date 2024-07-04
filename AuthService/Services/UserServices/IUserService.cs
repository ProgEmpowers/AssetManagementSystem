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
        Task<User?> GetUserByEmailAsync(string email);
        Task<User?> UpdateUserAsync(string id, UserUpdateDto userUpdateDto);

        Task<UserAsset> AssignAssetToUserAsync(AssignAssetToUserRequest request);

        Task<List<int>> GetAssetIdsByUserIdAsync(string userId);

        Task<bool> DeleteUserAssetAsync(AssignAssetToUserRequest request);
        Task<int> GetUserCountInRoleAsync(string roleName);
        Task<IEnumerable<UserWithRoleDto>> GetDeletedUsersAsync();
        Task<User?> GetUserByEmailAsync(string email);

        Task<List<UserWithRoleDto>> GetAllUsersWithRoleAsync();
        Task<User?> RecoverDeletedUserAsync(string id);
    }
}
