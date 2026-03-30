using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Wasla.Service.Abstract.Repositories;
using Wasla.Service.Abstract.Services;
using Wasla.Service.DTOs.Contracts;
using Wasla.Service.Immplementation;

namespace Wasla.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractController : ControllerBase
    {
        private readonly IContractService _contract;
        public ContractController(IContractService contract)
        {
            _contract = contract;
        }

        [HttpGet("details")]
        public async Task<IActionResult> GetContract(int contractId)
        {
            var res = await _contract.CreatContract(contractId);
            return Ok(res);
        }
        [HttpGet("upload_project")]
        public async Task<IActionResult> UploadProject(int pId)
        {
            var res = await _contract.UploadProject(pId);
            return Ok(res);
        }
        [HttpGet("Approve_Contract")]
        public async Task<IActionResult> ApproveContract(int pId)
        {
            var res = await _contract.ApproveContract(pId);
            return Ok(res);
        }
    }
}
