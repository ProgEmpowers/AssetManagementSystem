using AssetManagementSystem.Models;

namespace AssetManagementSystem.Services
{
    public interface IAssetService
    {
        IEnumerable<Asset> GetAllAssets();
        Asset GetAssetById(int id);
        void AddAsset(Asset newAsset);
        void UpdateAsset(int id, Asset updatedAsset);
        void DeleteAsset(int id);
    }
}