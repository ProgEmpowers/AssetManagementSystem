namespace AssetManagementSystem.Models.Dtos
{
    public class ContractDto
    {
        public DateTime AssignedDate { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string[] Vendors { get; set; }
    }
}
