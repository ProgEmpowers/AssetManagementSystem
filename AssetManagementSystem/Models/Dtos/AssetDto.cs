using AssetManagementSystem.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace AssetManagementSystem.Models.Dtos
{
    public class AssetDto
    {
        [Required]
        [MaxLength(5, ErrorMessage = "max")]
        [MinLength(3, ErrorMessage = "min")]
        public string Name { get; set; }
        public string AssetType { get; set; }
        public string Description { get; set; }
        public string? ImageUrl { get; set; }
        public string? QRcode { get; set; }
        public float? AssetValue { get; set; }
        public AssetStatusEnum AssetStatus { get; set; }
        public int? UserId { get; set; }
    }
}
