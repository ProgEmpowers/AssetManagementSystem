using AssetManagementSystem.Models;
using AuthService.Models.Dtos;
using AuthService.Services.AuthServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<User> userManager, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var identityUser = await userManager.FindByEmailAsync(request.Email);
            if (identityUser != null)
            {
                var checkPasswordResult = await userManager.CheckPasswordAsync(identityUser, request.Password);
                if (checkPasswordResult)
                {
                    var roles = await userManager.GetRolesAsync(identityUser);

                    var jwtToken = tokenRepository.CreateJwtToken(identityUser, roles.ToList());

                    var response = new LoginResponseDto()
                    {
                        Email = request.Email,
                        Roles = roles.ToList(),
                        Token = jwtToken
                    };
                    return Ok(response);
                }
                ModelState.AddModelError("", "Password Incorrect");
                return ValidationProblem(ModelState);
            }
            ModelState.AddModelError("", "Email Incorrect");
            return ValidationProblem(ModelState);
        }

        [HttpPost]
        [Route("register")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
        {
            var user = new User
            {
                UserName = request.Email?.Trim(),
                Email = request.Email?.Trim(),
                Address = request.Address,
                PhoneNumber = request.PhoneNumber?.Trim(),
                Nic = request.Nic?.Trim(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                JobPost = request.JobPost,
                DateofBirth = request.DateofBirth,
                IsActive = true



            };

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var identityUser = await userManager.FindByEmailAsync(request.Email);
            if (identityUser != null)
            {
                ModelState.AddModelError("", "Email Is Already registered");
                return ValidationProblem(ModelState);
            }

            var identityResult = await userManager.CreateAsync(user, request.Password);
            if (identityResult.Succeeded)
            {
                identityResult = await userManager.AddToRoleAsync(user, request.Role);
                if (identityResult.Succeeded)
                {
                    return Ok(ModelState);
                }
                else
                {
                    if (identityResult.Errors.Any())
                    {
                        foreach (var error in identityResult.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
            }
            else
            {
                if (identityResult.Errors.Any())
                {
                    foreach (var error in identityResult.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return ValidationProblem(ModelState);
        }
    }
}
