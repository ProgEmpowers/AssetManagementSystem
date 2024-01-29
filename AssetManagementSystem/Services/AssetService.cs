using System.Collections.Generic;
using AssetManagementSystem.Models;

namespace AssetManagementSystem.Services
{
    public class AssetService : IAssetService
    {
        private static List<Asset> assets = new List<Asset>
        {
            new Asset { Id = 1, Name = "Laptop" },
            new Asset { Id = 2, Name = "Monitor" },
            new Asset { Id = 2, Name = "Table" }
            // Add more sample assets if needed
        };

        public IEnumerable<Asset> GetAllAssets()
        {
            return assets;
        }

        public Asset GetAssetById(int id)
        {
            return assets.Find(a => a.Id == id);
        }

        public void AddAsset(Asset newAsset)
        {
            newAsset.Id = assets.Count + 1;
            assets.Add(newAsset);
        }

        public void UpdateAsset(int id, Asset updatedAsset)
        {
            var existingAsset = assets.Find(a => a.Id == id);

            if (existingAsset != null)
            {
                existingAsset.Name = updatedAsset.Name;
                // Update other properties as needed
            }
        }

        public void DeleteAsset(int id)
        {
            var existingAsset = assets.Find(a => a.Id == id);

            if (existingAsset != null)
            {
                assets.Remove(existingAsset);
            }
        }
    }
}