using AssetManagementSystem.Models;
using AuthService.Models.Dtos;
using AuthService.Models.Helpter;
using AuthService.Services.AuthServices;
using AuthService.Services.EmailServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using MimeKit;
using RestSharp;
using System.Net;
using System.Net.Mail;

namespace AuthService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly ITokenRepository tokenRepository;
        private readonly IEmailService emailService;

        public AuthController(UserManager<User> userManager, ITokenRepository tokenRepository, IEmailService emailService)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
            this.emailService = emailService;
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

            if(request.Password != request.ConfirmPassword)
            {
                ModelState.AddModelError("", "different passwords");
                return ValidationProblem(ModelState);
            }

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
                    //return Ok(ModelState);
                    SendRegistrationEmail(user.Email, request.Password, request.Role);
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
            resetPasswordDto.Token = WebUtility.UrlDecode(resetPasswordDto.Token);
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
    }
}
