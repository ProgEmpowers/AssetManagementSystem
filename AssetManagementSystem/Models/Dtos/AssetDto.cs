namespace AssetManagementSystem.Models.Dtos
{
    public class AssetDto
    {
        public string Name { get; set; }
        public string AssetType { get; set; }
        public byte[] Image { get; set; }
        public string QRurl { get; set; }
        public float AssetValue { get; set; }
        public string Status { get; set; }
    }
}
