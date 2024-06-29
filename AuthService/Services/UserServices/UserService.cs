using AssetManagementSystem.Models;
using AuthService.Data;
using AuthService.Models.Domains;
using AuthService.Models.Dtos;
using AutoMapper;
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
        


        public UserService(AuthDbContext dbContext, IMapper mapper, ILogger<UserService> logger )
        {

            this._dbContext = dbContext;
            this._mapper = mapper;
            this._logger = logger;
         

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

        public async Task<UserAsset> AssignAssetToUserAsync(UserAsset userAsset)
        {

           

            // Create the UserAsset relationship
           // var userAsset = new UserAsset
          //  {
           //     UserId = userId,
          //      AssetId = assetId
          //  };
          userAsset.AssetAssignedTime = DateTime.Now;

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


    }

}
