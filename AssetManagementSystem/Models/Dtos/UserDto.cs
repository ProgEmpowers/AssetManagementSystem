using AssetManagementSystem.Models.Enums;

namespace AssetManagementSystem.Models.Dtos
{
    public class UserDto
    {
        public int Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int MobileNo { get; set; }
        public UserRoleEnum UserRole { get; set; }
    }
}
