using AssetManagementSystem.Models.Dtos;
using AssetManagementSystem.Models;

namespace AssetManagementSystem.Services
{
    public interface IVendorService
    {
        Task<Vendor>AddVendorAsync(VendorDto newVendor);
        Task<List<Vendor>> GetAllVendorsAsync();
        Task<Vendor?> GetVendorByIdAsync(int id);
        Task<Vendor?> UpdateVendorAsync(int id, VendorDto vendorDto);
    }
}
