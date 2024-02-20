using System.ComponentModel.DataAnnotations;

namespace AssetManagementSystem.Models.Domains
{
    public class SellingContract
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
    }
}
