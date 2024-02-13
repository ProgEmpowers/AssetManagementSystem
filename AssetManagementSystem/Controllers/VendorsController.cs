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
    }
}
