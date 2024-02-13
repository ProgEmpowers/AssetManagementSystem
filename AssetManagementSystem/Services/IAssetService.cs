﻿using AssetManagementSystem.Models;
using AssetManagementSystem.Models.Dtos;

namespace AssetManagementSystem.Services
{
    public interface IAssetService
    {
        Task<Asset> AddAssetAsync(AssetDto newAsset);
        Task<List<Asset>> GetAllAssetsAsync();
        Task<Asset?> GetAssetByIdAsync(int id);
        Task<Asset?> UpdateAssetAsync(int id, AssetDto assetDto);
        Task<Asset?> DeleteAssetAsync(int id);
        Task<List<Asset>> GetAllDeletedAssetsAsync();
        Task<Asset?> GetDeletedAssetByIdAsync(int id);
        Task<Asset?> RecoverDeletedAssetAsync(int id);
    }
}