namespace AuthService.Models.Dtos
{
    public class AssignAssetToUserRequest
    {
        public string UserId { get; set; }
        public int AssetId { get; set; }
        public DateTime? AssetAssignedTime { get; set; }
    }
}
