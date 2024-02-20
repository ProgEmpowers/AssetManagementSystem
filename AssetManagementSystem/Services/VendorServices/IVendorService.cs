using AssetManagementSystem.Models.Dtos;
using AssetManagementSystem.Models.Domains;

namespace AssetManagementSystem.Services.VendorServices
{
    public interface IVendorService
    {
        Task<Vendor> AddVendorAsync(VendorDto newVendor);
        Task<List<Vendor>> GetAllVendorsAsync();
        Task<Vendor?> GetVendorByIdAsync(int id);
        Task<Vendor?> UpdateVendorAsync(int id, VendorDto vendorDto);
        Task<Vendor?> DeleteVendorAsync(int id);
    }
}
