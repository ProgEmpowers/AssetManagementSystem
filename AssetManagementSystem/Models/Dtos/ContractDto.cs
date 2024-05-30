namespace AssetManagementSystem.Models.Dtos
{
    public class ContractDto
    {
        public DateTime AssignedDate { get; set; }
        public string? Subject { get; set; }
        public string? Message { get; set; }
        public string? SupplyAssetType { get; set; }
        public int IdOfVendor { get; set; }
        public string? VendorName { get; set; }
    }
}
