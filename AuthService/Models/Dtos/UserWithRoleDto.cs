﻿namespace AuthService.Models.Dtos
{
    public class UserWithRoleDto
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string? Address { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Nic { get; set; }

        public string? PhoneNumber { get; set; }
        public string? DateofBirth { get; set; }
        public string? JobPost { get; set; }
        public bool IsActive { get; set; }
        public string ImageUrl { get; set; }
        public string? CustomUserId { get; set; }
        public string Role { get; set; }
    }
}
