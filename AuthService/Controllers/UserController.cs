using AssetManagementSystem.Models.Enums;
using AuthService.Models.Domains;
using AuthService.Models.Dtos;
using AuthService.Services.AssetService;
using AuthService.Services.UserServices;
using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAssetService _assetService;
        public UserController(IUserService userService, IAssetService assetService)
        {
            _userService = userService;
            _assetService = assetService;
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

        [HttpGet("GetUserByEmail/{email}")]
        public async Task<IActionResult> GetUserByEmail([FromRoute] string email)
        {
            var SelectedUser = await _userService.GetUserByEmailAsync(email);
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

        [HttpPost("AssignAssetToUser/{request}")]
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

        [HttpGet("GetAssetIdsByEmail/{email}")]
        public async Task<ActionResult<IEnumerable<int>>> GetAssetIdsByEmail(string email)
        {
            var SelectedUser = await _userService.GetUserByEmailAsync(email);
            if (SelectedUser == null)
            {
                return NotFound();
            }
            return Ok(SelectedUser);
        }

        [HttpPost("AssignAssetAsync")]
        public async Task<IActionResult> AssignAssetAsync(AssignAssetToUserRequest request)
        {
            var selectedAsset = await _assetService.GetAssetByIdAsync(request.AssetId);
            if (selectedAsset == null) { return NotFound(); }

            var user = await _userService.GetUserByIdAsync(request.UserId);
            if (user == null) { return NotFound("User not found!"); }

            var prevAsset = selectedAsset;
            selectedAsset.AssetStatus = AssetStatusEnum.Acquired;
            selectedAsset.UserId = user.Email;
            
            var updatedAsset = await _assetService.UpdateAssetAsync(request.AssetId, selectedAsset);
            if (updatedAsset == null)
            {
                return BadRequest("Unable to assign, please try again!");
            }

            var response = await _userService.AssignAssetToUserAsync(request);
            if (response == null) {
                await _assetService.UpdateAssetAsync(request.AssetId, prevAsset);
                return BadRequest("Unable to assign, please try again!");
            }

            return Ok();
        }


        [HttpDelete("ReleaseAsset")]
        public async Task<IActionResult> ReleaseAsset(AssignAssetToUserRequest request)
        {
            var selectedAsset = await _assetService.GetAssetByIdAsync(request.AssetId);
            if (selectedAsset == null) { return NotFound(); }

            var prevAsset = selectedAsset;
            selectedAsset.AssetStatus = AssetStatusEnum.Free;
            selectedAsset.UserId = "";
            var assetResponse = await _assetService.UpdateAssetAsync(request.AssetId, selectedAsset);
            if (assetResponse == null) 
            { 
                return BadRequest();
            }

            var selectedUser = await _userService.GetUserByEmailAsync(request.UserId);
            if (selectedUser == null) {
                await _assetService.UpdateAssetAsync(request.AssetId, prevAsset);
                return NotFound();
            }
            request.UserId = selectedUser.Id;

            var response = await _userService.DeleteUserAssetAsync(request);
            if(response != true)
            {
                await _assetService.UpdateAssetAsync(request.AssetId, prevAsset);
                return BadRequest();
            }
            return Ok();
        }

        [HttpGet("count/{roleName}")] 
        public async Task<int> GetRoleUserCount(string roleName)
        {
            try
            {
                var userCount = await _userService.GetUserCountInRoleAsync(roleName);
                return userCount;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }


        [HttpGet("deleted")]
        public async Task<IActionResult> GetDeletedUsers()
        {
            var DeletedUsers = await _userService.GetDeletedUsersAsync();
            return Ok(DeletedUsers);
        }


        [HttpGet("WithRole")]
        public async Task<IActionResult> GetAllUsersWithRole()
        {
            var usersWithRole = await _userService.GetAllUsersWithRoleAsync();
            return Ok(usersWithRole);
        }

        [HttpPut("recoverDeletedUser/{id}")]
        public async Task<IActionResult> RecoverDeletedUser([FromRoute] string id)
        {
            var SelectedUser = await _userService.RecoverDeletedUserAsync(id);
            if (SelectedUser == null)
            {
                return NotFound();
            }
            return Ok(SelectedUser);
        }

    }
}
