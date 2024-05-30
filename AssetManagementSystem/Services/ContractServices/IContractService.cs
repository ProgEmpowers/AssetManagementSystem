using AssetManagementSystem.Models.Dtos;
using AssetManagementSystem.Models.Domains;

namespace AssetManagementSystem.Services.ContractServices
{
    public interface IContractService
    {
        Task<Contract> AddContractAsync(ContractDto newContract);
        Task<List<Contract>> GetAllContractsAsync(
            string? filterOn = null,
            string? filterQuery = null,
            string? sortBy = null,
            bool isAscending = true,
            int pageNumber = 1,
            int pageSize = 6);
        Task<Contract?> GetContractByIdAsync(int id);
    }
}
