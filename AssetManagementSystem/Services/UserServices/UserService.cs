using AssetManagementSystem.Context;
using AssetManagementSystem.Models;
using AssetManagementSystem.Models.Dtos;

namespace AssetManagementSystem.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly AssetManagementDbContext _dbContext;

        public UserService(AssetManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<User> AddUserAsync(UserDto newUser)
        {
            var NewUser = new User()
            {
                
                Name = newUser.Name,
                Email = newUser.Email,
                Address = newUser.Address,
                MobileNo = newUser.MobileNo,
                UserRole = newUser.UserRole,
            };

            await _dbContext.User.AddAsync(NewUser);
            await _dbContext.SaveChangesAsync();
            return NewUser;
        }
    }
}
