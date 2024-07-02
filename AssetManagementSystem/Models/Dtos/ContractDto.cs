namespace AssetManagementSystem.Models.Dtos
{
    public class ContractDto
    {
        public DateTime AssignedDate { get; set; }
        public string? Optionals { get; set; }
        public List<OrderedAssetTypeDto> OrderedAssetTypes { get; set; }
        public List<int> IdOfVendors { get; set; }
        public List<string>? NameOfVendors { get; set; }
    }

    public class OrderedAssetTypeDto
    {
        public string OrderedAsset { get; set; }
        public int Quantity { get; set; }
    }
}

