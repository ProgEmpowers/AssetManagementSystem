using AssetManagementSystem.Models;
using AuthService.Models.Dtos;
using AuthService.Models.Helpter;
using AuthService.Services.AuthServices;
using AuthService.Services.EmailServices;
using AuthService.Services.UserServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MimeKit;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace AuthService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly ITokenRepository tokenRepository;
        private readonly IEmailService emailService;
        private readonly IConfiguration configuration;
        private readonly IUserService _userService;

        public AuthController(UserManager<User> userManager, ITokenRepository tokenRepository, IEmailService emailService, IConfiguration configuration, IUserService userService)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
            this.emailService = emailService;
            this.configuration = configuration;
            this._userService = userService;

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

            if (identityUser.IsActive == false)
            {
                ModelState.AddModelError("", "This is a deleted user");
                return ValidationProblem(ModelState);
            }

            if (identityUser != null)
            {
                var checkPasswordResult = await userManager.CheckPasswordAsync(identityUser, request.Password);
                if (checkPasswordResult)
                {
                    var roles = await userManager.GetRolesAsync(identityUser);

                    var jwtToken = tokenRepository.CreateJwtToken(identityUser, roles.ToList());

                    //refresh token
                    var refreshToken = GenerateRefreshToken();

                    //_= int.TryParse(configuration.GetSection("JWTSetting").GetSection("RefreshTokenValidityIn").Value!,out int RefreshTokenValidityIn);
                    if (!int.TryParse(configuration["Jwt:RefreshTokenValidityIn"], out int refreshTokenValidityIn))
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError, "Invalid configuration for RefreshTokenValidityIn");
                    }

                    identityUser.RefreshToken = refreshToken;
                    identityUser.RefreshTokenExpiryTime = DateTime.UtcNow.AddMinutes(refreshTokenValidityIn);
                    await userManager.UpdateAsync(identityUser);
                    //

                    var response = new LoginResponseDto()
                    {
                        Email = request.Email,
                        Roles = roles.ToList(),
                        Token = jwtToken,
                        RefreshToken = refreshToken,//
                    };
                    return Ok(response);
                }
                ModelState.AddModelError("", "Password Incorrect");
                return ValidationProblem(ModelState);
            }
            ModelState.AddModelError("", "Email Incorrect");
            return ValidationProblem(ModelState);
        }

        //
        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }




        //
        [HttpPost]
        [Route("refresh-token")]
        public async Task<ActionResult<AuthResponseDto>> RefreshToken(TokenDto tokenDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //var principal = GetPrincipalFromExpiredToken(tokenDto.Token);
            if (string.IsNullOrEmpty(tokenDto?.Token) || string.IsNullOrEmpty(tokenDto?.RefreshToken) || string.IsNullOrEmpty(tokenDto?.Email))
            {
                return BadRequest("Invalid client request");
            }

            var principal = GetPrincipalFromExpiredToken(tokenDto.Token);
            if (principal == null)
            {
                return BadRequest("Invalid token");
            }
            var user = await userManager.FindByEmailAsync(tokenDto.Email);
            var roles = await userManager.GetRolesAsync(user);


            if (principal is null || user is null || user.RefreshToken != tokenDto.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
                return BadRequest(new AuthResponseDto
                {
                    IsSuccess = false,
                    Message = "Invalid client request"
                });
            var newJwtToken = tokenRepository.CreateJwtToken(user, roles.ToList());
            var newRefreshToken = GenerateRefreshToken();
            //_ = int.TryParse(configuration.GetSection("JWTSetting").GetSection("RefreshTokenValidityIn").Value!, out int RefreshTokenValidityIn);
            if (!int.TryParse(configuration["Jwt:RefreshTokenValidityIn"], out int refreshTokenValidityIn))
            {
                // Handle error
                return StatusCode(StatusCodes.Status500InternalServerError, "Invalid configuration for RefreshTokenValidityIn");
            }

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddMinutes(refreshTokenValidityIn);

            await userManager.UpdateAsync(user);

            var response = new LoginResponseDto()
            {
                Email = tokenDto.Email,
                Roles = roles.ToList(),
                Token = newJwtToken,
                RefreshToken = newRefreshToken,//
            };
            return Ok(response);
        }

        private ClaimsPrincipal? GetPrincipalFromExpiredToken(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentNullException(nameof(token), "Token cannot be null or empty");
            }

            var tokenParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                //IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("JwtSetting").GetSection("securityKey").Value!)),
                //var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),
                ValidateLifetime = false
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenParameters, out SecurityToken securityToken);

            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }






        [HttpPost]
        [Route("register")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
        {
            var customUserId = await _userService.GenerateUserIdAsync();
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
                CustomUserId = customUserId,
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

            var pass = PasswordGenerator.GeneratePassword();

            var identityResult = await userManager.CreateAsync(user, pass);
            if (identityResult.Succeeded)
            {
                identityResult = await userManager.AddToRoleAsync(user, request.Role);
                if (identityResult.Succeeded)
                {
                    SendRegistrationEmail(user.Email, pass, request.Role);
                    //_hubContext.Clients.User(user.Id.ToString()).ReceiveNotification("You have been registered.");
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



        public static class PasswordGenerator
        {
            public static string GeneratePassword()
            {
                const int length = 6;
                const int uniqueChars = 1;

                const string lowerChars = "abcdefghijklmnopqrstuvwxyz";
                const string upperChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                const string digitChars = "0123456789";

                Random random = new Random();

                StringBuilder passwordBuilder = new StringBuilder();

                // Ensure at least one lowercase, one uppercase, and one digit character
                passwordBuilder.Append(lowerChars[random.Next(lowerChars.Length)]);
                passwordBuilder.Append(upperChars[random.Next(upperChars.Length)]);
                passwordBuilder.Append(digitChars[random.Next(digitChars.Length)]);

                // Fill the rest of the password length with a mix of characters
                string allChars = lowerChars + upperChars + digitChars;
                for (int i = 3; i < length; i++)
                {
                    passwordBuilder.Append(allChars[random.Next(allChars.Length)]);
                }

                // Shuffle the password to ensure randomness
                string password = new string(passwordBuilder.ToString().OrderBy(_ => random.Next()).ToArray());

                // Ensure there is exactly one unique character
                if (password.Distinct().Count() < uniqueChars)
                {
                    password = GeneratePassword();
                }

                return password;
            }
        }






        private async Task SendRegistrationEmail(string email, string password, string role)
        {
            try
            {
                Mailrequest mailrequest = new Mailrequest();
                mailrequest.ToEmail = email;
                mailrequest.Subject = "Welcome to Corzent Asset Management System";
                mailrequest.Body = GetHtmlcontent(email, password, role);
                await emailService.SendEmailAsync(mailrequest);
                
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        private string GetHtmlcontent(string email, string password, string role)
        {
            return $@"
    <div style='width:100%;background-color:lightblue;text-align:center;'>
        <h1>Welcome to Corzent !</h1>
        <img src='https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTx3M9PYi9zNk7ZPV8nAna-lrbGQEW5XBsJXg&s' alt='Corzent Logo' style='max-width:100%;height:auto;' />
        <h2>Dear {email},</h2>
        <p>You have been registered to the system with the following details:</p>
        <p><strong>Password:</strong> {password}</p>
        <p><strong>Role:</strong> {role}</p>
        <p>Please change your password after your first login.</p>
        <h2>Thank You!</h2>
        <div><h4>Contact us : corzent@gmail.com</h4></div>
    </div>";
        }






        [AllowAnonymous]
        [HttpPost("forgot-password")]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordDto forgotPasswordDto)
        {
            var user =await userManager.FindByEmailAsync(forgotPasswordDto.Email);
            if (user == null)
            {
                return Ok(new AuthResponseDto
                {
                    IsSuccess = false,
                    Message = "user does not exist with this email",
                });
            }
            var token = await userManager.GeneratePasswordResetTokenAsync(user);
            var resetLink = $"http://localhost:4200/reset-password?email={user.Email}&token={WebUtility.UrlEncode(token)}";

            var client = new RestClient("https://send.api.mailtrap.io/api/send");
            var request = new RestRequest
            {
                Method = Method.Post,
                RequestFormat = DataFormat.Json
            };

            request.AddHeader("Authorization", "Bearer 1859b9b50120ad358d1a9512e3d5c88d");
            request.AddJsonBody(new
            {
                from = new { email = "mailtrap@demomailtrap.com" },
                to = new[] { new { email = user.Email } },
                template_uuid = "7bc90853-71ba-4aee-b476-560b145336be",
                template_variables = new { user_email = user.Email, pass_reset_link = resetLink }
            });

            var response = client.Execute(request);
            if(response.IsSuccessful)
            {
                return Ok(new AuthResponseDto
                {
                    IsSuccess = true,
                    Message = "Email sent with password reset link. please check your email."
                });
            }
            else
            {
                return BadRequest(new AuthResponseDto
                {
                    IsSuccess = false,
                    Message = "failed to send email."
                });
            }
        }





        [AllowAnonymous]
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            if(resetPasswordDto.NewPassword != resetPasswordDto.ConfirmPassword)
            {
                return BadRequest(new AuthResponseDto
                {
                    IsSuccess = false,
                    Message = "Two different emails",
                });
            }
            var user = await userManager.FindByEmailAsync(resetPasswordDto.Email);
            //resetPasswordDto.Token = WebUtility.UrlDecode(resetPasswordDto.Token);
            if(user == null)
            {
                return BadRequest(new AuthResponseDto
                {
                    IsSuccess = false,
                    Message = "User does not exist with this Email",
                });
            }

            var result = await userManager.ResetPasswordAsync(user, resetPasswordDto.Token, resetPasswordDto.NewPassword);

            if(result.Succeeded)
            {
                return Ok(new AuthResponseDto
                {
                    IsSuccess = true,
                    Message = "Password reset Successfully",
                });
            }

            return BadRequest(new AuthResponseDto
            {
                IsSuccess = false,
                Message = result.Errors.FirstOrDefault()!.Description,
            });
        }


        [HttpPost("change-password")]
        public async Task<ActionResult> ChangePassword(ChangePasswordDto changePasswordDto)
        {
            var user = await userManager.FindByEmailAsync(changePasswordDto.Email);
            if(user == null)
            {
                return BadRequest(new AuthResponseDto
                {
                    IsSuccess = false,
                    Message = "User does not exists with this email"
                });
            }
            var result = await userManager.ChangePasswordAsync(user, changePasswordDto.CurrentPassword, changePasswordDto.NewPassword);

            if(result.Succeeded)
            {
                return Ok(new AuthResponseDto
                {
                    IsSuccess = true,
                    Message = "Password changed successfully"
                });
            }
            return BadRequest(new AuthResponseDto
            {
                IsSuccess = false,
                Message = "error happened 1"
            });
        }
    }
}
