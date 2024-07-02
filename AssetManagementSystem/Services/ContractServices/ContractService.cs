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
using System.Text;

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
            if (contractDto == null)
            {
                return null;
            }

            var selectedVendor = await vendorService.GetVendorsByIdAsync(contractDto.IdOfVendors);

            List<string> emailsOfSelectedVendors = selectedVendor.Select(v => v.Email).ToList();

            string Subject = "Requesting a quotation for the following assets.";
            contractDto.AssignedDate = DateTime.Now;



            var NewContract = mapper.Map<Contract>(contractDto);

            foreach (var id in NewContract.IdOfVendors)
            {
                var vendor = await vendorService.GetVendorByIdAsync(id);
                NewContract.NameOfVendors?.Add(vendor.Name);
            }

            await _dbContext.Contract.AddAsync(NewContract);
            await _dbContext.SaveChangesAsync();


            foreach (var vendor in selectedVendor)
            {
                await emailService.SendEmailAsync(vendor.Email, subject: Subject, GetHtmlcontent(vendor, NewContract));
            }

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

            var Contracts = _dbContext.Contract.Include(x => x.OrderedAssetTypes).AsQueryable();

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

        private static string GetHtmlcontent(Vendor vendor, Contract contract)
        {
            var emailTemplate = new StringBuilder();

            emailTemplate.Append("<!DOCTYPE html>");
            emailTemplate.Append("<html>");
            emailTemplate.Append("<head>");
            emailTemplate.Append("<title>My Web Page</title>");
            emailTemplate.Append("<style>");
            emailTemplate.Append("table { width: 80%; margin: auto; border-collapse: collapse; }");
            emailTemplate.Append("th, td { border: 1px solid #ddd; padding: 8px; }");
            emailTemplate.Append("th { background-color: #f2f2f2; text-align: center; }");
            emailTemplate.Append("td { text-align: center; }");
            emailTemplate.Append("tr:nth-child(even) { background-color: #f9f9f9; }");
            emailTemplate.Append("</style>");
            emailTemplate.Append("</head>");
            emailTemplate.Append("<body>");
            emailTemplate.Append("<div style='width:100%;background-color:lightblue;text-align:center;'>");
            emailTemplate.Append("<h1>Welcome to Corzent !</h1>");
            emailTemplate.Append("<img src='https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTx3M9PYi9zNk7ZPV8nAna-lrbGQEW5XBsJXg&s' alt='Corzent Logo' style='max-width:100%;height:auto;' />");
            emailTemplate.Append($"<h2>Dear {vendor.Name} Company,</h2>");
            emailTemplate.Append("<p>We are requesting a quotation for the following assets:</p>");
            emailTemplate.Append("<div>");
            emailTemplate.Append("<table>");
            emailTemplate.Append("<tr><th>Asset Name</th><th>Quantity</th></tr>");

            foreach (var asset in contract.OrderedAssetTypes)
            {
                emailTemplate.Append("<tr>");
                emailTemplate.Append($"<td>{asset.OrderedAsset}</td>");
                emailTemplate.Append($"<td>{asset.Quantity}</td>");
                emailTemplate.Append("</tr>");
            }

            emailTemplate.Append("</table>");
            emailTemplate.Append("</div>");
            emailTemplate.Append("<p>After receiving your quotation, we will inform you whether this order will be confirmed or not.</p>");
            emailTemplate.Append("<h2>Thank You!</h2>");
            emailTemplate.Append("<div><h4>Contact us: corzent@gmail.com</h4></div>");
            emailTemplate.Append("</div>");
            emailTemplate.Append("</body>");
            emailTemplate.Append("</html>");

            return emailTemplate.ToString();
        }

        public async Task<Contract> GetContractByIdAsync(int id)
        {
            var SelectedContract = await _dbContext.Contract.Include(x => x.OrderedAssetTypes).FirstOrDefaultAsync(x => x.Id == id);
            logger.LogInformation($"Finished Get Contract By Id : {JsonSerializer.Serialize(SelectedContract)}");
            return SelectedContract;
        }

        string IContractService.GetHtmlcontent(Vendor vendor, Contract contract)
        {
            throw new NotImplementedException();
        }


    }
}
