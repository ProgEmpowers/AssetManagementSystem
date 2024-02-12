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
            var Assets = await _dbContext.Asset.ToListAsync();
            return Assets;
        }

        public async Task<Asset?> GetAssetByIdAsync(int id)
        {
            var SelectedAsset = await _dbContext.Asset.FirstOrDefaultAsync(x => x.Id == id);
            return SelectedAsset;
        }

        public async Task<Asset?> UpdateAssetAsync(int id , AssetDto assetDto)
        {
            var SelectedAsset = await _dbContext.Asset.FirstOrDefaultAsync(x => x.Id == id);

            SelectedAsset.Name = assetDto.Name;
            SelectedAsset.AssetType = assetDto.AssetType;
            SelectedAsset.Description = assetDto.Description;
            SelectedAsset.ImageUrl = assetDto.ImageUrl;
            SelectedAsset.QRcode = assetDto.QRcode;
            SelectedAsset.AssetValue = assetDto.AssetValue;
            SelectedAsset.AssetStatus = assetDto.AssetStatus;

            await _dbContext.SaveChangesAsync();
            return SelectedAsset;
        }
    }
}