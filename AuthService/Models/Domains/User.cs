using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AssetManagementSystem.Models
{
    public class User : IdentityUser
    {
       
        public string? Address { get; set; }
        

    }
}
