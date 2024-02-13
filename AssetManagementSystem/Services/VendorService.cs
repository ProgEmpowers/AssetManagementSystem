using AssetManagementSystem.Context;
using AssetManagementSystem.Models.Dtos;
using AssetManagementSystem.Models;
using System.Threading.Tasks;

namespace AssetManagementSystem.Services
{
    public class VendorService : IVendorService
    {
        private readonly AssetManagementDbContext _dbContext;

        public VendorService(AssetManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<Vendor> AddVendorAsync(VendorDto newVendor)
        {

            var NewVendor = new Vendor()
            {
                Name = newVendor.Name,
                Address = newVendor.Address,
                MobileNo = newVendor.MobileNo,
                Email = newVendor.Email,
                SupplyAssetType = newVendor.SupplyAssetType,
            };

            await _dbContext.Vendor.AddAsync(NewVendor);
            await _dbContext.SaveChangesAsync();
            return NewVendor;
        }
    }
}
