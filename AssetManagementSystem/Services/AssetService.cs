using System.Collections.Generic;
using AssetManagementSystem.Context;
using AssetManagementSystem.Models;
using AssetManagementSystem.Models.Dtos;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

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

        public async Task<List<Asset>> GetAllAssetsAsync()
        {
            var Assetz = await _dbContext.Asset.ToListAsync();
            return Assetz;
        }

        public async Task<Asset> GetAssetByIdAsync(int id)
        {
            var Assetz = await _dbContext.Asset.FirstOrDefaultAsync(x => x.Id == id);
            return Assetz;
        }
    }
}