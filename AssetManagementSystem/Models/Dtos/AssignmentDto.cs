namespace AssetManagementSystem.Models.Dtos
{
    public class AssignmentDto
    {
        public string Name { get; set; }
        public string AssetType { get; set; }
        public byte[] Image { get; set; }
        public string QRurl { get; set; }
    }
}
