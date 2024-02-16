using AssetManagementSystem.Models.Dtos;
using AssetManagementSystem.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AssetManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorsController : ControllerBase
    {
        private readonly IVendorService _vendorService;

        public VendorsController(IVendorService vendorService)
        {
            _vendorService = vendorService;
        }

        [HttpPost]
        public async Task<IActionResult> AddVendor([FromBody] VendorDto vendorDto)
        {
            if (vendorDto == null)
                return BadRequest();

            var addedVendor = _vendorService.AddVendorAsync(vendorDto);

            return Ok(addedVendor);
        }

        [HttpGet]
        public async Task<IActionResult> GetallVendors()
        {
            var AllVendors = await _vendorService.GetAllVendorsAsync();
            return Ok(AllVendors);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetVendorById([FromRoute] int id)
        {
            var SelectedVendor = await _vendorService.GetVendorByIdAsync(id);
            if (SelectedVendor == null)
            {
                return NotFound();
            }
            return Ok(SelectedVendor);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateVendor([FromRoute] int id, [FromBody] VendorDto vendorDto)
        {
            var SelectedVendor = await _vendorService.UpdateVendorAsync(id, vendorDto);
            if (SelectedVendor == null)
            {
                return NotFound();
            }
            return Ok(SelectedVendor);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteVendor([FromRoute] int id)
        {
            var SelectedVendor = await _vendorService.DeleteVendorAsync(id);
            if (SelectedVendor == null)
            {
                return NotFound();
            }
            return Ok(SelectedVendor);
        }
    }
}
