using System.Collections.Generic;
using AssetManagementSystem.Context;
using AssetManagementSystem.Models;
using AssetManagementSystem.Models.Dtos;
using AssetManagementSystem.Services;
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
        public async Task<IActionResult> GetallAssets()
        {
            var AllAssets = await _assetService.GetAllAssetsAsync();
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
    }
}