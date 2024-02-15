using AssetManagementSystem.Context;
using AssetManagementSystem.Models.Dtos;
using AssetManagementSystem.Models;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AssetManagementSystem.Services
{
    public class VendorService : IVendorService
    {
        private readonly AssetManagementDbContext _dbContext;
        private readonly IMapper mapper;

        public VendorService(AssetManagementDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            this.mapper = mapper;
        }


        public async Task<Vendor> AddVendorAsync(VendorDto vendorDto)
        {
            var NewVendor = mapper.Map<Vendor>(vendorDto);
            NewVendor.IsActive = true;
            await _dbContext.Vendor.AddAsync(NewVendor);
            await _dbContext.SaveChangesAsync();
            return NewVendor;
        }

        public async Task<List<Vendor>> GetAllVendorsAsync()
        {
            var Vendors = await _dbContext.Vendor.Where(vendor => vendor.IsActive == true).ToListAsync();
            return Vendors;
        }

        public async Task<Vendor?> GetVendorByIdAsync(int id)
        {
            var SelectedVendor = await _dbContext.Vendor.Where(Vendor => Vendor.IsActive == true).FirstOrDefaultAsync(x => x.Id == id);
            return SelectedVendor;
        }

        public async Task<Vendor?> UpdateVendorAsync(int id, VendorDto vendorDto)
        {
            var SelectedVendor = await _dbContext.Vendor.Where(vendor => vendor.IsActive == true).FirstOrDefaultAsync(x => x.Id == id);
            if (SelectedVendor == null)
            {
                return null;
            }
            SelectedVendor = mapper.Map(vendorDto, SelectedVendor);
            await _dbContext.SaveChangesAsync();
            return SelectedVendor;
        }
    }
}
