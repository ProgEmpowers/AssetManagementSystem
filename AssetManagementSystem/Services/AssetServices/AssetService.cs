using System.Collections.Generic;
using System.Text.Json;
using AssetManagementSystem.Context;
using AssetManagementSystem.Models.Domains;
using AssetManagementSystem.Models.Dtos;
using AssetManagementSystem.Models.Enums;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

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
            NewAsset.DateCreated = DateAndTime.Now.ToString();
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
            var Assets = _dbContext.Asset.Where(asset => asset.AssetStatus == status && asset.IsActive==true).AsQueryable();
            logger.LogInformation($"Finished Get Asset by Status : {JsonSerializer.Serialize(Assets)}");
            return await Assets.ToListAsync();
        }

        public async Task<int> GetTotalNoOfAssetsAsync()
        {
            var AssetCount = _dbContext.Asset.Where(asset => asset.IsActive == true).CountAsync();
            logger.LogInformation($"Finished Get Total Asset Count : {JsonSerializer.Serialize(AssetCount)}");
            return await AssetCount;
        }

        public async Task<int> GetNoOfAssetsByStatusAsync(AssetStatusEnum status)
        {
            var AssetCount = _dbContext.Asset.CountAsync(asset => asset.AssetStatus == status && asset.IsActive == true);
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

        public async Task<DisposalAssets> AddDisposalAssetsAsync(DisposalAssetsDto disposalassetDto)
        {
            var NewDisposalAssets = mapper.Map<DisposalAssets>(disposalassetDto);
            NewDisposalAssets.IsActive = true;
            await _dbContext.DisposalAssets.AddAsync(NewDisposalAssets);
            await _dbContext.SaveChangesAsync();
            logger.LogInformation($"Finished Add Disposal Asset : {JsonSerializer.Serialize(NewDisposalAssets)}");
            return NewDisposalAssets;
        }

        public async Task<List<DisposalAssets>> GetAllDisposalAssetsAsync(
            string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true)
        {
            var DisposalAssets = _dbContext.DisposalAssets.Where(disposalasset => disposalasset.IsActive == true).AsQueryable();
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

        public async Task<DisposalAssets?> UpdateDisposalAssetsAsync(int id, DisposalAssetsDto disposalassetDto)
        {
            var SelecteddisposalAssets = await _dbContext.DisposalAssets.Where(disposalasset => disposalasset.IsActive == true).FirstOrDefaultAsync(x => x.Id == id);
            if (SelecteddisposalAssets == null)
            {
                return null;
            }
            SelecteddisposalAssets = mapper.Map(disposalassetDto, SelecteddisposalAssets);
            await _dbContext.SaveChangesAsync();
            logger.LogInformation($"Finished Update a DisposalAsset : {JsonSerializer.Serialize(SelecteddisposalAssets)}");
            return SelecteddisposalAssets;
        }

        public async Task<IEnumerable<Asset>> GetAssetsByTypeAsync(string type)
        {
            return await _dbContext.Asset.Where(a => a.AssetType == type).Where(asset => asset.IsActive == true).Where(a => a.AssetStatus == AssetStatusEnum.Free).ToListAsync();
        }


        public async Task<AssetType?> AddAssetTypeAsync(AssetTypeDto type)
        {
            var newAssetType = mapper.Map<AssetType>(type);
            var name = newAssetType.Name;
            var check = await _dbContext.AssetType.FirstOrDefaultAsync(a => a.Name == name);
            if (check != null)
            {
                return null;
            }
            await _dbContext.AssetType.AddAsync(newAssetType);
            await _dbContext.SaveChangesAsync();
            logger.LogInformation($"Finished Add Asset Type: {JsonSerializer.Serialize(newAssetType)}");
            return newAssetType;
        }

        public async Task<List<string>> GetAssetTypesAsync()
        {
            var types = _dbContext.AssetType.Select(type => type.Name).AsQueryable();
            return await types.ToListAsync();
        }

        public async Task<AssetType> DeleteAssetTypeAsync(AssetTypeDto name)
        {
            var type = mapper.Map<AssetType>(name);
            var selectedType = await _dbContext.AssetType.FirstOrDefaultAsync(a => a.Name == name.Name);
            _dbContext.AssetType.Remove(selectedType);
            await _dbContext.SaveChangesAsync();
            return type;
        }


    }
}