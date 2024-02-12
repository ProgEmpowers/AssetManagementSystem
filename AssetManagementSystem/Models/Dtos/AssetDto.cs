using AssetManagementSystem.Models.Enums;

namespace AssetManagementSystem.Models.Dtos
{
    public class AssetDto
    {
        public string Name { get; set; }
        public string AssetType { get; set; }
        public string Description { get; set; }
        public string? ImageUrl { get; set; }
        public string? QRcode { get; set; }
        public float? AssetValue { get; set; }
        public AssetStatusEnum AssetStatus { get; set; }
    }
}
