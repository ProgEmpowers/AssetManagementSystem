using System.Collections.Generic;
using AssetManagementSystem.Context;
using AssetManagementSystem.Models;
using AssetManagementSystem.Models.Dtos;

namespace AssetManagementSystem.Services
{
    public class AssetService : IAssetService
    {

        private readonly AssetManagementDbContext _dbContext;

        public AssetService(AssetManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<Asset> AddAssetAsync(AssetDto newAsset)
        {

            var NewAsset = new Asset()
            {
                Name = newAsset.Name,
                AssetType = newAsset.AssetType,
                Description = newAsset.Description,
                ImageUrl = newAsset.ImageUrl,
                QRcode = newAsset.QRcode,
                AssetValue = newAsset.AssetValue,
                AssetStatus = newAsset.AssetStatus,
            };

            await _dbContext.Asset.AddAsync(NewAsset);
            await _dbContext.SaveChangesAsync();
            return NewAsset;
        }
    }
}