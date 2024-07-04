using AssetManagementSystem.Models;
using AuthService.Data;
using AuthService.Models.Domains;
using AuthService.Models.Dtos;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using System.Text.Json;

namespace AuthService.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly AuthDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;



        public UserService(AuthDbContext dbContext, IMapper mapper, ILogger<UserService> logger, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {

            this._dbContext = dbContext;
            this._mapper = mapper;
            this._logger = logger;
            this._userManager = userManager;
            this._roleManager = roleManager;

        }


        public async Task<string> GenerateUserIdAsync()
        {
            var lastUser = await _dbContext.Users
                .OrderByDescending(u => u.CustomUserId)
                .FirstOrDefaultAsync();

            if (lastUser == null || string.IsNullOrWhiteSpace(lastUser.CustomUserId))
            {
                return "E0001";
            }

            var lastUserId = lastUser.CustomUserId;
            var numericPart = int.Parse(lastUserId.Substring(1));
            var newNumericPart = numericPart + 1;
            var newUserId = $"E{newNumericPart.ToString("D4")}";

            return newUserId;
        }

     


        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var Users = await _dbContext.User.Where(user => user.IsActive == true).ToListAsync();
            
            return Users.Select(user => new UserDto
            {
                Id = user.Id,
                Email=user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateofBirth = user.DateofBirth,
                Address = user.Address,
                Nic=user.Nic,
                PhoneNumber = user.PhoneNumber,
                JobPost=user.JobPost,
                CustomUserId = user.CustomUserId,
                
           
            });
        }



        public async Task<User?> DeleteUserAsync(string id)
        {
            var SelectedUser = await _dbContext.User.Where(user => user.IsActive == true).FirstOrDefaultAsync(x => x.Id == id);
            if (SelectedUser == null)
            {
                return null;
            }
            SelectedUser.IsActive = false;
            await _dbContext.SaveChangesAsync();
            return SelectedUser;
        }

        public async Task<User?> GetUserByIdAsync(string id)
        {
            var SelectedUser = await _dbContext.User.Where(user => user.IsActive == true).FirstOrDefaultAsync(x => x.Id == id);
            _logger.LogInformation($"Finished Get Employee by Id : {JsonSerializer.Serialize(SelectedUser)}");
            return SelectedUser;
        }



        public async Task<User?> UpdateUserAsync(string id, UserUpdateDto userUpdateDto)
        {
            var selectedUser = await _dbContext.User.Where(user => user.IsActive == true).FirstOrDefaultAsync(x => x.Id == id);
            if (selectedUser == null)
            {
                return null;
            }

            _mapper.Map(userUpdateDto, selectedUser);
            await _dbContext.SaveChangesAsync();
            _logger.LogInformation($"Finished Update a User : {JsonSerializer.Serialize(selectedUser)}");
            return selectedUser;
        }

        public async Task<UserAsset> AssignAssetToUserAsync(AssignAssetToUserRequest request)
        {

           

  
          request.AssetAssignedTime = DateTime.Now;


            var userAsset = new UserAsset
            {
                UserId = request.UserId,
                AssetId = request.AssetId,
                AssetAssignedTime = request.AssetAssignedTime
                
            };


            _dbContext.UserAssets.Add(userAsset);
            await _dbContext.SaveChangesAsync();
            return userAsset;

        }

        public async Task<List<int>> GetAssetIdsByUserIdAsync(string userId)
        {
            return await _dbContext.UserAssets
                .Where(ua => ua.UserId == userId)
                .Select(ua => ua.AssetId)
                .ToListAsync();
        }

        public async Task<bool> DeleteUserAssetAsync(AssignAssetToUserRequest request)
        {
            var userAsset = await _dbContext.UserAssets
                .FirstOrDefaultAsync(ua => ua.AssetId == request.AssetId && ua.UserId == request.UserId);
            if (userAsset != null) 
            { 
                _dbContext.UserAssets.Remove(userAsset);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            
            return false;
        }


        public async Task<int> GetUserCountInRoleAsync(string roleName)
        {
            // Check if the role exists
            var roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                throw new Exception("Role does not exist.");
            }

            // Get users in the specified role
            var usersInRole = await _userManager.GetUsersInRoleAsync(roleName);

            var activeUsersInRole = usersInRole.Where(u => u.IsActive).ToList();

            // Return the count of users in the role
            return activeUsersInRole.Count;
        }


        public async Task<IEnumerable<UserWithRoleDto>> GetDeletedUsersAsync()
        {
            var users = await _userManager.Users.Where(user => user.IsActive == false).OrderBy(user => user.CustomUserId).ToListAsync();
            var userWithRole = new List<UserWithRoleDto>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var role = roles.FirstOrDefault(); // Assuming each user has only one role
                userWithRole.Add(new UserWithRoleDto
                {
                    Id = user.Id,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Address = user.Address,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Nic = user.Nic,
                    DateofBirth = user.DateofBirth,
                    JobPost = user.JobPost,
                    IsActive = user.IsActive,
                    ImageUrl = user.ImageUrl,
                    CustomUserId = user.CustomUserId,
                    Role = role
                });
            }

            return userWithRole;
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            var SelectedUser = await _dbContext.User.FirstOrDefaultAsync(x => x.Email == email);
            _logger.LogInformation($"Finished Get Employee by Email : {JsonSerializer.Serialize(SelectedUser)}");
            return SelectedUser;
        }

        public async Task<List<UserWithRoleDto>> GetAllUsersWithRoleAsync()
        {
            var users = await _userManager.Users.Where(user => user.IsActive == true).OrderBy(user => user.CustomUserId).Where(u => u.Email != "admin@corzent.com").ToListAsync();
            var userWithRole = new List<UserWithRoleDto>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var role = roles.FirstOrDefault(); // Assuming each user has only one role
                userWithRole.Add(new UserWithRoleDto
                {
                    Id = user.Id,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Address = user.Address,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Nic = user.Nic,
                    DateofBirth = user.DateofBirth,
                    JobPost = user.JobPost,
                    IsActive = user.IsActive,
                    ImageUrl = user.ImageUrl,
                    CustomUserId = user.CustomUserId,
                    Role = role
                });
            }

            return userWithRole;
        }

        public async Task<User?> RecoverDeletedUserAsync(string id)
        {
            var SelectedUser = await _dbContext.User.Where(u => u.IsActive == false).FirstOrDefaultAsync(x => x.Id == id);
            if (SelectedUser == null)
            {
                return null;
            }
            SelectedUser.IsActive = true;
            await _dbContext.SaveChangesAsync();
            _logger.LogInformation($"Finished Recover Deleted User : {JsonSerializer.Serialize(SelectedUser)}");
            return SelectedUser;
        }

    }

}
