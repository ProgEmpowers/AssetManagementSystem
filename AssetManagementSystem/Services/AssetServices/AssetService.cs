using System.Collections.Generic;
using System.Text.Json;
using AssetManagementSystem.Context;
using AssetManagementSystem.Models.Domains;
using AssetManagementSystem.Models.Dtos;
using AssetManagementSystem.Models.Enums;
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
            NewAsset.AssetStatus = AssetStatusEnum.Free;
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

        public async Task<List<Asset>> GetAllAssetsAsync(
            string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true,
            int pageNumber = 1, int pageSize = 10)
        {
            var Assets = _dbContext.Asset.Where(asset => asset.IsActive == true).AsQueryable();
            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    Assets = Assets.Where(x => x.Name.Contains(filterQuery));
                }
            }
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("Id", StringComparison.OrdinalIgnoreCase))
                {
                    Assets = isAscending ? Assets.OrderBy(x => x.Id) : Assets.OrderByDescending(x => x.Id);
                }
            }
            var skipResult = (pageNumber - 1) * pageSize;
            logger.LogInformation($"Finished Get All Assets : {JsonSerializer.Serialize(Assets)}");
            return await Assets.Skip(skipResult).Take(pageSize).ToListAsync();
        }

        public async Task<Asset?> GetAssetByIdAsync(int id)
        {
            var SelectedAsset = await _dbContext.Asset.Where(asset => asset.IsActive == true).FirstOrDefaultAsync(x => x.Id == id);
            logger.LogInformation($"Finished Get Asset by Id : {JsonSerializer.Serialize(SelectedAsset)}");
            return SelectedAsset;
        }


        public async Task<List<Asset>> GetAssetByUserAsync(string email)
        {
            var userAssets = _dbContext.Asset.Where(asset => asset.UserId == email).AsQueryable();
            logger.LogInformation($"Finished Get Asset by User : {JsonSerializer.Serialize(userAssets)}");
            return await userAssets.ToListAsync();
        }

        public async Task<List<Asset>> GetAssetsByStatusAsync(AssetStatusEnum status)
        {
            var Assets = _dbContext.Asset.Where(asset => asset.AssetStatus == status).AsQueryable();
            logger.LogInformation($"Finished Get Asset by Status : {JsonSerializer.Serialize(Assets)}");
            return await Assets.ToListAsync();
        }

        public async Task<int> GetTotalNoOfAssetsAsync()
        {
            var AssetCount = _dbContext.Asset.CountAsync();
            logger.LogInformation($"Finished Get Total Asset Count : {JsonSerializer.Serialize(AssetCount)}");
            return await AssetCount;
        }

        public async Task<int> GetNoOfAssetsByStatusAsync(AssetStatusEnum status)
        {
            var AssetCount = _dbContext.Asset.CountAsync(asset => asset.AssetStatus == status);
            logger.LogInformation($"Finished Get Asset Count By Status : {JsonSerializer.Serialize(AssetCount)}");
            return await AssetCount;   
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

        public async Task<List<Asset>> GetAllDeletedAssetsAsync(
            string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true,
            int pageNumber = 1, int pageSize = 10)
        {
            var Assets = _dbContext.Asset.Where(asset => asset.IsActive == false).AsQueryable();
            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    Assets = Assets.Where(x => x.Name.Contains(filterQuery));
                }
            }
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("Id", StringComparison.OrdinalIgnoreCase))
                {
                    Assets = isAscending ? Assets.OrderBy(x => x.Id) : Assets.OrderByDescending(x => x.Id);
                }
            }
            var skipResult = (pageNumber - 1) * pageSize;
            logger.LogInformation($"Finished Get All Deleted Assets : {JsonSerializer.Serialize(Assets)}");
            return await Assets.Skip(skipResult).Take(pageSize).ToListAsync(); ;
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

        public async Task<Asset> AddDisposalAssetAsync(DisposalAssetDto disposalassetDto)
        {
            var NewDisposalAsset = mapper.Map<Asset>(disposalassetDto);
            NewDisposalAsset.IsActive = true;
            NewDisposalAsset.AssetStatus = AssetStatusEnum.Disposal;
            NewDisposalAsset.UserId = "";
            await _dbContext.Asset.AddAsync(NewDisposalAsset);
            await _dbContext.SaveChangesAsync();
            logger.LogInformation($"Finished Add Disposal Asset : {JsonSerializer.Serialize(NewDisposalAsset)}");
            return NewDisposalAsset;
        }

        public async Task<List<Asset>> GetAllDisposalAssetsAsync(
            string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true)
        {
            var DisposalAssets = _dbContext.Asset.Where(asset => asset.IsActive == true).AsQueryable();
            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    DisposalAssets = DisposalAssets.Where(x => x.Name.Contains(filterQuery));
                }
            }
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("Id", StringComparison.OrdinalIgnoreCase))
                {
                    DisposalAssets = isAscending ? DisposalAssets.OrderBy(x => x.Id) : DisposalAssets.OrderByDescending(x => x.Id);
                }
            }
            logger.LogInformation($"Finished Get All Disposal Assets : {JsonSerializer.Serialize(DisposalAssets)}");
            return await DisposalAssets.ToListAsync();
        }

        public async Task<Asset?> UpdateDisposalAssetAsync(int id, DisposalAssetDto disposalassetDto)
        {
            var SelecteddisposalAsset = await _dbContext.Asset.Where(asset => asset.IsActive == true).FirstOrDefaultAsync(x => x.Id == id);
            if (SelecteddisposalAsset == null)
            {
                return null;
            }
            SelecteddisposalAsset = mapper.Map(disposalassetDto, SelecteddisposalAsset);
            await _dbContext.SaveChangesAsync();
            logger.LogInformation($"Finished Update a DisposalAsset : {JsonSerializer.Serialize(SelecteddisposalAsset)}");
            return SelecteddisposalAsset;
        }
    }
}