using AuthService.Services.UserServices;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers
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


        [HttpGet]
        public async Task<IActionResult> GetallUsers()
        {
            var AllUsers = await _userService.GetAllUsersAsync();
            return Ok(AllUsers);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var SelectedUser = await _userService.DeleteUserAsync(id);
            if (SelectedUser == null)
            {
                return NotFound();
            }
            return Ok(SelectedUser);
        }
    }
}
