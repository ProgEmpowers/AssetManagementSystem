using System.ComponentModel.DataAnnotations;

namespace AssetManagementSystem.Models.Domains
{
    public class Log
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int AssetId { get; set; }

        //navigation properties
        public Asset Asset { get; set; }
    }
}
