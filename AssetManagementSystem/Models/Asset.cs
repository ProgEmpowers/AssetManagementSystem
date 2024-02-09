using System.ComponentModel.DataAnnotations;

namespace AssetManagementSystem.Models
{
    public class Asset
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string AssetType { get; set; }
        public byte[] Image { get; set; }
        public string QRurl { get; set; }
        public float AssetValue { get; set; }
        public string Status { get; set;}

        public int LogId { get; set; }

        //navigation properties
        public Log Log { get; set; }
    }
}
