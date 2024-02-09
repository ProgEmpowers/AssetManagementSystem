using System.ComponentModel.DataAnnotations;

namespace AssetManagementSystem.Models
{
    public class Assignment
    {
        [Key]
        public int Id { get; set; }
        public DateTime MyProperty { get; set; }

        public int UserId { get; set; }
        public int AssetId { get; set; }

        //navigation properties
        public User User { get; set; }
        public Asset Asset { get; set; }

    }
}
