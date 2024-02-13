using System.Collections.Generic;
using AssetManagementSystem.Context;
using AssetManagementSystem.Models;
using AssetManagementSystem.Models.Dtos;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace AssetManagementSystem.Services
{
    public class AssetService : IAssetService
    {

        private readonly AssetManagementDbContext _dbContext;
        private readonly IMapper mapper;

        public AssetService(AssetManagementDbContext dbContext , IMapper mapper)
        {
            _dbContext = dbContext;
            this.mapper = mapper;
        }


        public async Task<Asset> AddAssetAsync(AssetDto assetDto)
        {
            var NewAsset = mapper.Map<Asset>(assetDto);

            await _dbContext.Asset.AddAsync(NewAsset);
            await _dbContext.SaveChangesAsync();
            return NewAsset;
        }

        public async Task<Asset?> DeleteAssetAsync(int id)
        {
            var SelectedAsset = await _dbContext.Asset.FirstOrDefaultAsync(x => x.Id == id);

            SelectedAsset.IsActive = false;

            await _dbContext.SaveChangesAsync();
            return SelectedAsset;
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
        public async Task<Asset?> UpdateAssetAsync(int id, AssetDto assetDto)
        {
            var SelectedAsset = await _dbContext.Asset.FirstOrDefaultAsync(x => x.Id == id);
            SelectedAsset = mapper.Map<Asset>(assetDto);

            await _dbContext.SaveChangesAsync();
            return SelectedAsset;
        }

        public async Task<List<Asset>> GetDeletedAssetsAsync()
        {
            var Assets = await _dbContext.Asset.ToListAsync();
            return Assets;
        }
    }
}