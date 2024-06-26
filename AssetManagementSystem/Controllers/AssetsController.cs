﻿using System.Collections.Generic;
using System.Globalization;
using AssetManagementSystem.Context;
using AssetManagementSystem.CustomActionFilters;
using AssetManagementSystem.Models;
using AssetManagementSystem.Models.Domains;
using AssetManagementSystem.Models.Dtos;
using AssetManagementSystem.Models.Enums;
using AssetManagementSystem.Services.AssetServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AssetManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetsController : ControllerBase
    {
        private readonly IAssetService _assetService;

        public AssetsController(IAssetService assetService)
        {
            _assetService = assetService;
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> AddAsset([FromBody] AssetDto assetDto)
        {
            if (assetDto == null)
                return BadRequest();

            var addedAsset = await _assetService.AddAssetAsync(assetDto);
            return Ok(addedAsset);
        }

        [HttpGet]
        public async Task<IActionResult> GetallAssets(
            [FromQuery] string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 20)
        {
            var AllAssets = await _assetService.GetAllAssetsAsync(filterOn, filterQuery, sortBy, isAscending ?? true, pageNumber, pageSize);
            return Ok(AllAssets);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetAssetById([FromRoute] int id)
        {
            var SelectedAsset = await _assetService.GetAssetByIdAsync(id);
            if (SelectedAsset == null)
            {
                return NotFound();
            }
            return Ok(SelectedAsset);
        }

        [HttpGet("GetAssetByUserAsync/{email}")]
        public async Task<IActionResult> GetAssetByUserAsync([FromRoute] string email)
        {
            var usersAssets = await _assetService.GetAssetByUserAsync(email);
            if (usersAssets == null)
            {
                return NotFound();
            }
            return Ok(usersAssets);
        }

        [HttpGet("GetTotalNoOfAssetsAsync")]
        public async Task<IActionResult> GetTotalNoOfAssetsAsync()
        {
            var count = await _assetService.GetTotalNoOfAssetsAsync();
            return Ok(count);
        }

        [HttpGet("GetAssetsByStatusAsync/{status:int}")]
        public async Task<IActionResult> GetAssetsByStatusAsync([FromRoute] AssetStatusEnum status)
        {
            var assetsList = await _assetService.GetAssetsByStatusAsync(status);
            if (assetsList == null)
            {
                return NotFound();
            }
            return Ok(assetsList);
        }

        [HttpGet("GetNoOfAssetsByStatusAsync/{status:int}")]
        public async Task<IActionResult> GetNoOfAssetsByStatusAsync([FromRoute] AssetStatusEnum status)
        {
            var assetCount = await _assetService.GetNoOfAssetsByStatusAsync(status);
            
            return Ok(assetCount);
        }

        [HttpPut]
        [Route("{id:int}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateAsset([FromRoute] int id, [FromBody] AssetDto assetDto)
        {
            var SelectedAsset = await _assetService.UpdateAssetAsync(id, assetDto);
            if (SelectedAsset == null)
            {
                return NotFound();
            }
            return Ok(SelectedAsset);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteAsset([FromRoute] int id)
        {
            var SelectedAsset = await _assetService.DeleteAssetAsync(id);
            if (SelectedAsset == null)
            {
                return NotFound();
            }
            return Ok(SelectedAsset);
        }

        [HttpGet("getAllDeletedAssets")]
        public async Task<IActionResult> GetDeletedAssets(
            [FromQuery] string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var AllAssets = await _assetService.GetAllDeletedAssetsAsync(filterOn, filterQuery, sortBy, isAscending ?? true, pageNumber, pageSize);
            return Ok(AllAssets);
        }

        [HttpGet("getDeletedAssetById/{id:int}")]
        public async Task<IActionResult> GetDeletedAssetById([FromRoute] int id)
        {
            var SelectedAsset = await _assetService.GetDeletedAssetByIdAsync(id);
            if (SelectedAsset == null)
            {
                return NotFound();
            }
            return Ok(SelectedAsset);
        }

        [HttpPut("recoverDeletedAsset/{id:int}")]
        public async Task<IActionResult> RecoverDeletedAsset([FromRoute] int id)
        {
            var SelectedAsset = await _assetService.RecoverDeletedAssetAsync(id);
            if (SelectedAsset == null)
            {
                return NotFound();
            }
            return Ok(SelectedAsset);
        }

        [HttpPost("AddDisposalAsset")]
        [ValidateModel]
        public async Task<IActionResult> AddDisposalAsset([FromBody] DisposalAssetDto disposalassetDto)
        {
            if (disposalassetDto == null)
                return BadRequest();

            var addedDisposalAsset = await _assetService.AddDisposalAssetAsync(disposalassetDto);
            return Ok(addedDisposalAsset);
        }

        [HttpGet("GetAllDisposalAssets")]
        public async Task<IActionResult> GetAllDisposalAssets(
           [FromQuery] string? filterOn, [FromQuery] string? filterQuery,
           [FromQuery] string? sortBy, [FromQuery] bool? isAscending
           )
        {
            var AllDisposalAssets = await _assetService.GetAllDisposalAssetsAsync(filterOn, filterQuery, sortBy, isAscending ?? true);
            return Ok(AllDisposalAssets);
        }

        [HttpPut(" UpdateDisposalAsset/{id:int}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateDisposalAsset([FromRoute] int id, [FromBody] DisposalAssetDto disposalassetDto)
        {
            var SelecteddisposalAsset = await _assetService.UpdateDisposalAssetAsync(id, disposalassetDto);
            if (SelecteddisposalAsset == null)
            {
                return NotFound();
            }
            return Ok(SelecteddisposalAsset);
        }


        [HttpGet("types")]
        public async Task<ActionResult<IEnumerable<string>>> GetAssetTypes()
        {
            var assetTypes = await _assetService.GetAssetTypesAsync();
            return Ok(assetTypes);
        }

        [HttpGet("type/{type}")]
        public async Task<ActionResult<IEnumerable<Asset>>> GetAssetsByType(string type)
        {
            var assets = await _assetService.GetAssetsByTypeAsync(type);
            return Ok(assets);
        }

        
    }
}