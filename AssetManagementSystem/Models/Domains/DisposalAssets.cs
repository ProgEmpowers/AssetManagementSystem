using AssetManagementSystem.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace AssetManagementSystem.Models.Domains
{
    public class DisposalAssets
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string? AssetType { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public string? QRcode { get; set; }
        public float? AssetValue { get; set; }
        public bool IsActive { get; set; }
        public AssetStatusEnum AssetStatus { get; set; }
        public string? Price { get; set; }
        public string? Update { get; set; }
    }
}
