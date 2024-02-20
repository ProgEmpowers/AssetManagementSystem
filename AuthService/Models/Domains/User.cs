using AssetManagementSystem.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace AssetManagementSystem.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public int Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int MobileNo { get; set; }
        public UserRoleEnum UserRole { get; set; }

    }
}
