using AssetManagementSystem.Models.Dtos;
using AssetManagementSystem.Models;

namespace AssetManagementSystem.Services
{
    public interface IVendorService
    {
        Task<Vendor>AddVendorAsync(VendorDto newVendor);
    }
}
