using AssetManagementSystem.Models.Dtos;
namespace AuthService.Services.AssetService
{
    public interface IAssetService
    {
        Task<AssetDto> GetAssetByIdAsync(int assetId);

        Task<AssetDto> UpdateAssetAsync(int id, AssetDto assetDto);

    }
}
