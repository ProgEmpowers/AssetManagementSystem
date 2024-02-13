using AssetManagementSystem.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetManagementSystem.Models
{
    public class Asset
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string AssetType { get; set; }
        public string Description { get; set; }
        public string? ImageUrl { get; set; }
        public string? QRcode { get; set; }
        public bool IsActive { get; set; }
        public float? AssetValue { get; set; }
        public AssetStatusEnum AssetStatus { get; set;}
    }
}
