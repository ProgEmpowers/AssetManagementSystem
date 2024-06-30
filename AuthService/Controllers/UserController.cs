using AuthService.Models.Domains;
using AuthService.Models.Dtos;
using AuthService.Services.UserServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetUserById([FromRoute] string id)
        {
            var SelectedUser = await _userService.GetUserByIdAsync(id);
            if (SelectedUser == null)
            {
                return NotFound();
            }
            return Ok(SelectedUser);
        }


        [HttpPut]
        [Route("{id}")]

        public async Task<IActionResult> UpdateUser([FromRoute] string id, [FromBody] UserUpdateDto request)
        {
            var SelectedUser = await _userService.UpdateUserAsync(id, request);
            if (SelectedUser == null)
            {
                return NotFound();
            }
            return Ok(SelectedUser);
        }

        [HttpPost]
        public async Task<IActionResult> AssignAssetToUser(AssignAssetToUserRequest request)
        {
            try
            {
                await _userService.AssignAssetToUserAsync(request);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAssetIdsByUserId/{userId}")]
        public async Task<ActionResult<IEnumerable<int>>> GetAssetIdsByUserId(string userId)
        {
            var SelectedUser = await _userService.GetAssetIdsByUserIdAsync(userId);
            if (SelectedUser == null)
            {
                return NotFound();
            }
            return Ok(SelectedUser);
        }

    }
}
