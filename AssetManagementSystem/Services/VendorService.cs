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
    }
}
