using AssetManagementSystem.Context;
using AssetManagementSystem.Models.Dtos;
using AssetManagementSystem.Models.Domains;
using AutoMapper;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Net;
using AssetManagementSystem.Services.EmailServices;
using AssetManagementSystem.Services.VendorServices;

namespace AssetManagementSystem.Services.ContractServices
{
    public class ContractService : IContractService
    {
        private readonly AssetManagementDbContext _dbContext;
        private readonly IMapper mapper;
        private readonly ILogger<ContractService> logger;
        private readonly IEmailService emailService;
        private readonly IVendorService vendorService;

        public ContractService(AssetManagementDbContext dbContext, IMapper mapper, ILogger<ContractService> logger, IEmailService emailService, IVendorService vendorService)
        {
            _dbContext = dbContext;
            this.mapper = mapper;
            this.logger = logger;
            this.emailService = emailService;
            this.vendorService = vendorService;
        }

        public async Task<Contract?> AddContractAsync(ContractDto contractDto)
        {
            if(contractDto == null)
            {
                return null;
            }

            var selectedVendor = await vendorService.GetVendorByIdAsync(contractDto.IdOfVendor);

            contractDto.VendorName = selectedVendor.Name;

            string emailsOfSelectedVendors = selectedVendor.Email;

#pragma warning disable CS8604 // Possible null reference argument.
            await emailService.SendEmailAsync(emailsOfSelectedVendors, subject: contractDto.Subject, contractDto.Message);
#pragma warning restore CS8604 // Possible null reference argument.

            var NewContract = mapper.Map<Contract>(contractDto);
            await _dbContext.Contract.AddAsync(NewContract);
            await _dbContext.SaveChangesAsync();
            return NewContract;
        }

        public async Task<List<Contract>> GetAllContractsAsync(
            string? filterOn = null,
            string? filterQuery = null,
            string? sortBy = null,
            bool isAscending = true,
            int pageNumber = 1,
            int pageSize = 6)
        {
            pageSize = await _dbContext.Contract.CountAsync();

            var Contracts = _dbContext.Contract.AsQueryable();
            if(string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("Subject", StringComparison.OrdinalIgnoreCase))
                {
                    Contracts = Contracts.Where(x => x.Subject.Contains(filterQuery));
                }
            }
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("Id", StringComparison.OrdinalIgnoreCase))
                {
                    Contracts = isAscending ? Contracts.OrderBy(x => x.Id) : Contracts.OrderByDescending(x => x.Id);
                }
            }
            var skipResult = (pageNumber - 1) * pageSize;
            logger.LogInformation($"Finished Get All Contracts : {JsonSerializer.Serialize(Contracts)}");
            return await Contracts.Skip(skipResult).Take(pageSize).ToListAsync();
        }
        public async Task<Contract> GetContractByIdAsync(int id)
        {
            var SelectedContract = await _dbContext.Contract.FirstOrDefaultAsync(x => x.Id == id);
            logger.LogInformation($"Finished Get Contract By Id : {JsonSerializer.Serialize(SelectedContract)}");
            return SelectedContract;
        }

        
    }
}
