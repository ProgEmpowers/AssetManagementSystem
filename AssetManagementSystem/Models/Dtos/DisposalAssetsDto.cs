using AssetManagementSystem.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace AssetManagementSystem.Models.Dtos
{
    public class DisposalAssetsDto
    {
        [Required]
        [MaxLength(5, ErrorMessage = "max")]
        [MinLength(3, ErrorMessage = "min")]
        public string Name { get; set; }
        public string? AssetType { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public string? QRcode { get; set; }
        public float? AssetValue { get; set; }
        public bool IsActive { get; set; }
        public AssetStatusEnum AssetStatus { get; set; }
        public string?  Price { get; set; }
        public string? Update { get; set; }
    }
}
