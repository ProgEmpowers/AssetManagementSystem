using AssetManagementSystem.Models.Dtos;
using AssetManagementSystem.Services.VendorServices;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Admin,VendorManeger")]
        public async Task<IActionResult> AddVendor([FromBody] VendorDto vendorDto)
        {
            if (vendorDto == null)
            {
                return BadRequest();
            }

            var addedVendor = await _vendorService.AddVendorAsync(vendorDto);


            return Ok(addedVendor);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,VendorManeger")]
        public async Task<IActionResult> GetallVendors(
            [FromQuery] string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var AllVendors = await _vendorService.GetAllVendorsAsync(filterOn, filterQuery, sortBy, isAscending ?? true, pageNumber, pageSize);
            return Ok(AllVendors);
        }

        [HttpGet]
        [Route("{id:int}")]
        [Authorize(Roles = "Admin,VendorManeger")]
        public async Task<IActionResult> GetVendorById([FromRoute] int id)
        {
            var SelectedVendor = await _vendorService.GetVendorByIdAsync(id);
            if (SelectedVendor == null)
            {
                return NotFound();
            }
            return Ok(SelectedVendor);
        }

        [HttpGet]
        [Route("ids&names")]
        [Authorize(Roles = "Admin,VendorManeger")]
        public async Task<IActionResult> GetVendorList()
        {
            var vendorList = await _vendorService.GetVendorListAsync();
            return Ok(vendorList);
        }

        [HttpPut]
        [Route("{id:int}")]
        [Authorize(Roles = "Admin,VendorManeger")]
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
        [Authorize(Roles = "Admin,VendorManeger")]
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
