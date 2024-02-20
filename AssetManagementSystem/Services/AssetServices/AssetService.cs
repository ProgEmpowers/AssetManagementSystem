using System.Collections.Generic;
using System.Text.Json;
using AssetManagementSystem.Context;
using AssetManagementSystem.Models.Domains;
using AssetManagementSystem.Models.Dtos;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace AssetManagementSystem.Services.AssetServices
{
    public class AssetService : IAssetService
    {

        private readonly AssetManagementDbContext _dbContext;
        private readonly IMapper mapper;
        private readonly ILogger<AssetService> logger;

        public AssetService(AssetManagementDbContext dbContext, IMapper mapper, ILogger<AssetService> logger)
        {
            _dbContext = dbContext;
            this.mapper = mapper;
            this.logger = logger;
        }


        public async Task<Asset> AddAssetAsync(AssetDto assetDto)
        {
            var NewAsset = mapper.Map<Asset>(assetDto);
            NewAsset.IsActive = true;
            await _dbContext.Asset.AddAsync(NewAsset);
            await _dbContext.SaveChangesAsync();
            logger.LogInformation($"Finished Add Asset : {JsonSerializer.Serialize(NewAsset)}");
            return NewAsset;
        }

        public async Task<Asset?> DeleteAssetAsync(int id)
        {
            var SelectedAsset = await _dbContext.Asset.Where(asset => asset.IsActive == true).FirstOrDefaultAsync(x => x.Id == id);
            if (SelectedAsset == null)
            {
                return null;
            }
            SelectedAsset.IsActive = false;
            await _dbContext.SaveChangesAsync();
            logger.LogInformation($"Finished Delete Asset : {JsonSerializer.Serialize(SelectedAsset)}");
            return SelectedAsset;
        }

        public async Task<List<Asset>> GetAllAssetsAsync()
        {
            var Assets = await _dbContext.Asset.Where(asset => asset.IsActive == true).ToListAsync();
            logger.LogInformation($"Finished Get All Assets : {JsonSerializer.Serialize(Assets)}");
            return Assets;
        }

        public async Task<Asset?> GetAssetByIdAsync(int id)
        {
            var SelectedAsset = await _dbContext.Asset.Where(asset => asset.IsActive == true).FirstOrDefaultAsync(x => x.Id == id);
            logger.LogInformation($"Finished Get Asset by Id : {JsonSerializer.Serialize(SelectedAsset)}");
            return SelectedAsset;
        }
        public async Task<Asset?> UpdateAssetAsync(int id, AssetDto assetDto)
        {
            var SelectedAsset = await _dbContext.Asset.Where(asset => asset.IsActive == true).FirstOrDefaultAsync(x => x.Id == id);
            if (SelectedAsset == null)
            {
                return null;
            }
            SelectedAsset = mapper.Map(assetDto, SelectedAsset);
            await _dbContext.SaveChangesAsync();
            logger.LogInformation($"Finished Update a Asset : {JsonSerializer.Serialize(SelectedAsset)}");
            return SelectedAsset;
        }

        public async Task<List<Asset>> GetAllDeletedAssetsAsync()
        {
            var Assets = await _dbContext.Asset.Where(asset => asset.IsActive == false).ToListAsync();
            logger.LogInformation($"Finished Get All Deleted Assets : {JsonSerializer.Serialize(Assets)}");
            return Assets;
        }

        public async Task<Asset?> GetDeletedAssetByIdAsync(int id)
        {
            var Asset = await _dbContext.Asset.Where(asset => asset.IsActive == false).FirstOrDefaultAsync(x => x.Id == id);
            logger.LogInformation($"Finished Get Deleted Asset by id : {JsonSerializer.Serialize(Asset)}");
            return Asset;
        }

        public async Task<Asset?> RecoverDeletedAssetAsync(int id)
        {
            var SelectedAsset = await _dbContext.Asset.Where(asset => asset.IsActive == false).FirstOrDefaultAsync(x => x.Id == id);
            if (SelectedAsset == null)
            {
                return null;
            }
            SelectedAsset.IsActive = true;
            await _dbContext.SaveChangesAsync();
            logger.LogInformation($"Finished Recover Deleted Asset : {JsonSerializer.Serialize(SelectedAsset)}");
            return SelectedAsset;
        }
    }
}