using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AssetManagementSystem.Models
{
    public class User : IdentityUser
    {
       
        public string? Address { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Nic { get; set; }
        public DateTime? DateofBirth { get; set; }
        public string? JobPost { get; set; }
        public bool IsActive { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }

    }
}
