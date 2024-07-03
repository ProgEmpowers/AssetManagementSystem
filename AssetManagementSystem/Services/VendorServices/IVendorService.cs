using AssetManagementSystem.Models.Dtos;
using AssetManagementSystem.Models.Domains;

namespace AssetManagementSystem.Services.VendorServices
{
    public interface IVendorService
    {
        Task<Vendor> AddVendorAsync(VendorDto newVendor);
        Task<List<Vendor>> GetAllVendorsAsync(
            string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true,
            int pageNumber = 1, int pageSize = 10);
        Task<Vendor?> GetVendorByIdAsync(int id);
        Task<List<Vendor>> GetVendorListAsync();
        Task<Vendor?> UpdateVendorAsync(int id, VendorDto vendorDto);
        Task<Vendor?> DeleteVendorAsync(int id);
        Task<List<Vendor>> GetVendorsByIdAsync(List<int> idOfVendors);
    }
}
