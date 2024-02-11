namespace AssetManagementSystem.Models.Dtos
{
    public class AssignmentDto
    {
        public int Id { get; set; }
        public DateTime MyProperty { get; set; }

        public int UserId { get; set; }
        public int AssetId { get; set; }
    }
}
