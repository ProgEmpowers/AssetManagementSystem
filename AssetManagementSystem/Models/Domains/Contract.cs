﻿using System.ComponentModel.DataAnnotations;

namespace AssetManagementSystem.Models.Domains
{
    public class Contract
    {
        [Key]
        public int Id { get; set; }
        public DateTime AssignedDate { get; set; }
        public string? Subject { get; set; }
        public string? Message { get; set; }
        public string? SupplyAssetType { get; set; }
        public int IdOfVendor { get; set; }
        public string? VendorName { get; set; }
    }
}
