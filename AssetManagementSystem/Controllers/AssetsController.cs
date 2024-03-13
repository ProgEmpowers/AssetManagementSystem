using System.Collections.Generic;
using System.Globalization;
using AssetManagementSystem.Context;
using AssetManagementSystem.Models;
using AssetManagementSystem.Models.Dtos;
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
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var AllAssets = await _assetService.GetAllAssetsAsync(filterOn, filterQuery, sortBy, isAscending ?? true, pageNumber, pageSize);
            return Ok(AllAssets);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetAssetById([FromRoute]int id)
        {
            var SelectedAsset = await _assetService.GetAssetByIdAsync(id);
            if (SelectedAsset == null)
            {
                return NotFound();
            }
            return Ok(SelectedAsset);
        }

        [HttpPut]
        [Route("{id:int}")]
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
    }
}