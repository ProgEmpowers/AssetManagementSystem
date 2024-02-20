using System.ComponentModel.DataAnnotations;

namespace AssetManagementSystem.Models.Domains
{
    public class Vendor
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int MobileNo { get; set; }
        public string Email { get; set; }
        public string SupplyAssetType { get; set; }
        public bool IsActive { get; set; }
    }
}
