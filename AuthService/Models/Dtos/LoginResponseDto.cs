﻿namespace AuthService.Models.Dtos
{
    public class LoginResponseDto
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public List<string> Roles { get; set; }
        public string RefreshToken { get; set; } = string.Empty;

    }
}
