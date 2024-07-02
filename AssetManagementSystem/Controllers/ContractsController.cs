using AssetManagementSystem.Models.Dtos;
using AssetManagementSystem.Services.ContractServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AssetManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractsController : ControllerBase
    {
        private readonly IContractService _contractService;

        public ContractsController(IContractService contractService)
        {
            _contractService = contractService;
        }

        [HttpPost]
        public async Task<IActionResult> AddContract([FromBody] ContractDto contractDto)
        {
            if (contractDto == null)
            {
                return BadRequest();
            }


            var addedContract = await _contractService.AddContractAsync(contractDto);
            return Ok(addedContract);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllContracts(
            [FromQuery] string? filterOn,
            [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy,
            [FromQuery] bool? isAscending,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 6
            )
        {
            var allContracts = await _contractService.GetAllContractsAsync(filterOn, filterQuery, sortBy, isAscending ?? true, pageNumber, pageSize);
            return Ok(allContracts);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetContractById([FromRoute] int id)
        {
            var selectedContract = await _contractService.GetContractByIdAsync(id);

            if (selectedContract == null)
            {
                return NotFound();
            }

            return Ok(selectedContract);
        }
    }
}
