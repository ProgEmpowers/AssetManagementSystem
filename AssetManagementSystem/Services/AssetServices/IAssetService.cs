﻿using AssetManagementSystem.Models.Domains;
using AssetManagementSystem.Models.Dtos;

namespace AssetManagementSystem.Services.AssetServices
{
    public interface IAssetService
    {
        Task<Asset> AddAssetAsync(AssetDto newAsset);
        Task<List<Asset>> GetAllAssetsAsync(
            string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true,
            int pageNumber = 1, int pageSize = 10);
        Task<Asset?> GetAssetByIdAsync(int id);
        Task<Asset?> UpdateAssetAsync(int id, AssetDto assetDto);
        Task<Asset?> DeleteAssetAsync(int id);
        Task<List<Asset>> GetAllDeletedAssetsAsync(
            string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true,
            int pageNumber = 1, int pageSize = 10);
        Task<Asset?> GetDeletedAssetByIdAsync(int id);
        Task<Asset?> RecoverDeletedAssetAsync(int id);
    }
}