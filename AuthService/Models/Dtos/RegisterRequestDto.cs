﻿namespace AuthService.Models.Dtos
{
    public class RegisterRequestDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
    }
}
