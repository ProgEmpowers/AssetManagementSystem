﻿namespace AuthService.Models.Dtos
{
    public class AuthResponseDto
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string? Token { get; set; }
    }
}
