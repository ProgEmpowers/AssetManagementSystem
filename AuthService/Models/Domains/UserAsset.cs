using AssetManagementSystem.Models;

namespace AuthService.Models.Domains
{
    public class UserAsset
    {
        public string UserId { get; set; }
        public int AssetId { get; set; }

       public DateTime? AssetAssignedTime { get; set; }
    }
}
