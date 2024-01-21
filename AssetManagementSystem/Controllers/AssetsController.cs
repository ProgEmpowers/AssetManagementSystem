using System.Collections.Generic;
using AssetManagementSystem.Models;
using AssetManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;

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

        // GET: api/Assets
        [HttpGet]
        public IEnumerable<Asset> Get()
        {
            return _assetService.GetAllAssets();
        }

        // GET: api/Assets/5
        [HttpGet("{id}")]
        public ActionResult<Asset> Get(int id)
        {
            var asset = _assetService.GetAssetById(id);

            if (asset == null)
            {
                return NotFound(); // 404 Not Found
            }

            return asset;
        }

        // POST: api/Assets
        [HttpPost]
        public ActionResult Post([FromBody] Asset newAsset)
        {
            _assetService.AddAsset(newAsset);

            // Return the newly created asset with a 201 Created status
            return CreatedAtAction(nameof(Get), new { id = newAsset.Id }, newAsset);
        }

        // PUT: api/Assets/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Asset updatedAsset)
        {
            _assetService.UpdateAsset(id, updatedAsset);

            return NoContent(); // 204 No Content
        }

        // DELETE: api/Assets/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _assetService.DeleteAsset(id);

            return NoContent(); // 204 No Content
        }
    }
}