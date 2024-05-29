using AssetManagementSystem.Models;
using AuthService.Data;
using AuthService.Models.Dtos;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly AuthDbContext _dbContext;
        private readonly IMapper mapper;
        public UserService(AuthDbContext dbContext, IMapper mapper)
        {

            _dbContext = dbContext;
            this.mapper = mapper;
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



    }

}
