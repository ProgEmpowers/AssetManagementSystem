using System.ComponentModel.DataAnnotations;

namespace AssetManagementSystem.Models
{
    public class Log
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
