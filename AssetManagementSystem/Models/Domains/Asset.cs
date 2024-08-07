﻿using AssetManagementSystem.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetManagementSystem.Models.Domains
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
        public string? DateCreated { get; set; }
        public AssetStatusEnum AssetStatus { get; set; }
        public string? UserId { get; set; }
        public string? Vendor { get; set; }
    }
}
