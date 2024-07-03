using AssetManagementSystem.Models.Domains;
using AssetManagementSystem.Models.Dtos;

using AssetManagementSystem.Models.Enums;


namespace AssetManagementSystem.Services.AssetServices
{
    public interface IAssetService
    {
        Task<Asset> AddAssetAsync(AssetDto newAsset);
        Task<DisposalAssets> AddDisposalAssetsAsync(DisposalAssetsDto newDisposalsAssets);
        Task<List<DisposalAssets>> GetAllDisposalAssetsAsync(
            string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true);
        Task<List<Asset>> GetAllAssetsAsync(
            string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true,
            int pageNumber = 1, int pageSize = 10);
        Task<Asset?> GetAssetByIdAsync(int id);
        Task<Asset?> UpdateAssetAsync(int id, AssetDto assetDto);
        Task<DisposalAssets?> UpdateDisposalAssetsAsync(int id, DisposalAssetsDto disposalassetsDto);
        Task<Asset?> DeleteAssetAsync(int id);
        Task<List<Asset>> GetAllDeletedAssetsAsync(
            string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true,
            int pageNumber = 1, int pageSize = 10);
        Task<Asset?> GetDeletedAssetByIdAsync(int id);
        Task<Asset?> RecoverDeletedAssetAsync(int id);


        Task<IEnumerable<string>> GetAssetTypesAsync();
        Task<IEnumerable<Asset>> GetAssetsByTypeAsync(string type);

   

        Task<List<Asset>> GetAssetsByStatusAsync(AssetStatusEnum status);
        Task<List<Asset>> GetAssetByUserAsync(string email);
        Task<int> GetTotalNoOfAssetsAsync();
        Task<int> GetNoOfAssetsByStatusAsync(AssetStatusEnum status);

    }
}