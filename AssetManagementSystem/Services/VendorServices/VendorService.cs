using AssetManagementSystem.Context;
using AssetManagementSystem.Models.Dtos;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using AssetManagementSystem.Models.Domains;
using Serilog.Data;
using System.Text.Json;
using Microsoft.IdentityModel.Tokens;

namespace AssetManagementSystem.Services.VendorServices
{
    public class VendorService : IVendorService
    {
        private readonly AssetManagementDbContext _dbContext;
        private readonly IMapper mapper;
        private readonly ILogger<VendorService> logger;

        public VendorService(AssetManagementDbContext dbContext, IMapper mapper, ILogger<VendorService> logger)
        {
            _dbContext = dbContext;
            this.mapper = mapper;
            this.logger = logger;
        }


        public async Task<Vendor?> AddVendorAsync(VendorDto vendorDto)
        {
            var existVendor = await _dbContext.Vendor.Where(Vendor => Vendor.IsActive == true).FirstOrDefaultAsync(x => x.MobileNo == vendorDto.MobileNo && x.Email == vendorDto.Email);

            if (existVendor != null)
            {
                return null;
            }

            var NewVendor = mapper.Map<Vendor>(vendorDto);
            NewVendor.IsActive = true;
            await _dbContext.Vendor.AddAsync(NewVendor);
            await _dbContext.SaveChangesAsync();
            return NewVendor;
        }

        public async Task<List<Vendor>> GetAllVendorsAsync(
            string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true,
            int pageNumber = 1, int pageSize = 10)
        {
            var Vendors = _dbContext.Vendor.Where(vendor => vendor.IsActive == true).AsQueryable();
            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    Vendors = Vendors.Where(x => x.Name.Contains(filterQuery));
                }
            }
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("Id", StringComparison.OrdinalIgnoreCase))
                {
                    Vendors = isAscending ? Vendors.OrderBy(x => x.Id) : Vendors.OrderByDescending(x => x.Id);
                }
            }
            var skipResult = (pageNumber - 1) * pageSize;
            logger.LogInformation($"Finished Get All Vendors : {JsonSerializer.Serialize(Vendors)}");
            return await Vendors.Skip(skipResult).Take(pageSize).ToListAsync();
        }

        public async Task<Vendor?> GetVendorByIdAsync(int id)
        {
            var SelectedVendor = await _dbContext.Vendor.Where(Vendor => Vendor.IsActive == true).FirstOrDefaultAsync(x => x.Id == id);
            logger.LogInformation($"Finished Get Vendor By Id : {JsonSerializer.Serialize(SelectedVendor)}");
            return SelectedVendor;
        }

        public async Task<List<string>> GetVendorsNamesAsync()
        {
            var vendorNames = await _dbContext.Vendor.Where(vendor => vendor.IsActive == true).Select(vendor => vendor.Name).ToListAsync();
            logger.LogInformation($"Finished Get Vendor Names : {JsonSerializer.Serialize(vendorNames)}");
            return vendorNames;
        }
        

        public async Task<Vendor?> UpdateVendorAsync(int id, VendorDto vendorDto)
        {
            
            var SelectedVendor = await _dbContext.Vendor.Where(vendor => vendor.IsActive == true).FirstOrDefaultAsync(x => x.Id == id);

            if (SelectedVendor == null)
            {
                return null;
            } 
            else
            {
                if (vendorDto.Name != "")
                {
                    SelectedVendor.Name = vendorDto.Name;
                }
                if (vendorDto.MobileNo != null)
                {
                    var vendorHasMobileNo = await _dbContext.Vendor.FirstOrDefaultAsync(vendor => vendor.MobileNo == vendorDto.MobileNo);

                    if (vendorDto.Email != "")
                    {
                        var vendorHasEmail = await _dbContext.Vendor.FirstOrDefaultAsync(vendor => vendor.Email == vendorDto.Email);

                        if(vendorHasMobileNo == vendorHasEmail)
                        {
                            return null;
                        }
                    } else
                    {
                        var vendorHasEmail = await _dbContext.Vendor.FirstOrDefaultAsync(vendor => vendor.Email == SelectedVendor.Email);

                        if(vendorHasMobileNo == vendorHasEmail)
                        {
                            return null;
                        }
                    }
                    SelectedVendor.MobileNo = (int)vendorDto.MobileNo;
                }
                if (vendorDto.Address != "")
                {
                    SelectedVendor.Address = vendorDto.Address;
                }
                if (vendorDto.Email != "")
                {
                    var vendorHasEmail = await _dbContext.Vendor.FirstOrDefaultAsync(vendor => vendor.Email == vendorDto.Email);

                    if (vendorDto.MobileNo != null)
                    {
                        var vendorHasMobileNo = await _dbContext.Vendor.FirstOrDefaultAsync(vendor => vendor.MobileNo == vendorDto.MobileNo);

                        if (vendorHasMobileNo == vendorHasEmail)
                        {
                            return null;
                        }
                    }
                    else
                    {
                        var vendorHasMobileNo = await _dbContext.Vendor.FirstOrDefaultAsync(vendor => vendor.MobileNo == SelectedVendor.MobileNo);

                        if(vendorHasMobileNo == vendorHasEmail)
                        {
                            return null;
                        }
                    }

                    SelectedVendor.Email = vendorDto.Email;
                }
                if (vendorDto.SupplyAssetType != "")
                {
                    SelectedVendor.SupplyAssetType = vendorDto.SupplyAssetType;
                }
            }
            
            
            await _dbContext.SaveChangesAsync();
            return SelectedVendor;
        }

        public async Task<Vendor?> DeleteVendorAsync(int id)
        {
            var SelectedVendor = await _dbContext.Vendor.Where(vendor => vendor.IsActive == true).FirstOrDefaultAsync(x => x.Id == id);
            if (SelectedVendor == null)
            {
                return null;
            }
            SelectedVendor.IsActive = false;
            await _dbContext.SaveChangesAsync();
            return SelectedVendor;
        }
    }
}
