using AuthService.Models.Domains;
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
        public string? DateofBirth { get; set; }
        public string? JobPost { get; set; }
        public bool IsActive { get; set; }
        public string? ImageUrl { get; set; }
        public string? CustomUserId { get; set; }

        // List of asset IDs
        //  public List<int> AssetIds { get; set; } = new List<int>();

        //   public int? AssetId { get; set; }

       // public ICollection<UserAsset> UserAssets { get; set; }

    }
}
