using AssetManagementSystem.Models.Dtos;
using AssetManagementSystem.Services;
using AssetManagementSystem.Services.UserServices;
using Microsoft.AspNetCore.Mvc;

namespace AssetManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
       

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] UserDto userDto)
        {
            if (userDto == null)
                return BadRequest();

            var addedUser = await _userService.AddUserAsync(userDto);

            return Ok(addedUser);
        }

        
    }
}
