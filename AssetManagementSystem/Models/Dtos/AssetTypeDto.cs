using System.ComponentModel.DataAnnotations;

namespace AssetManagementSystem.Models.Dtos
{
    public class AssetTypeDto
    {
        [Required]
        [MinLength(2, ErrorMessage = "min")]
        public string Name { get; set; }
    }
}
