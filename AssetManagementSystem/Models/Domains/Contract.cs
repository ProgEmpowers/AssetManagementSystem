using System.ComponentModel.DataAnnotations;

namespace AssetManagementSystem.Models.Domains
{
    public class Contract
    {
        [Key]
        public int Id { get; set; }
        public DateTime AssignedDate { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
