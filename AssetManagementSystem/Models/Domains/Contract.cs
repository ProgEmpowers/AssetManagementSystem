using System.ComponentModel.DataAnnotations;

namespace AssetManagementSystem.Models.Domains
{
    public class Contract
    {
        [Key]
        public int Id { get; set; }
        public DateTime AssignedDate { get; set; }
        public string? Optionals { get; set; }
        public List<OrderedAssetType> OrderedAssetTypes { get; set; }
        public List<int> IdOfVendors { get; set; }
        public List<string> NameOfVendors { get; set; }
    }

    public class OrderedAssetType
    {
        [Key]
        public int Id { get; set; }
        public string OrderedAsset { get; set; }
        public int Quantity { get; set; }
    }
}
