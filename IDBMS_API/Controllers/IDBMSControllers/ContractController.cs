using IDBMS_API.DTOs.Request;
using IDBMS_API.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace IDBMS_API.Controllers.IDBMSControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractController : Controller
    {
        ContractService contractService;
        public ContractController(ContractService contractService)
        {
            this.contractService = contractService;
        }
        [HttpGet("downLoadContract")]
        public async Task<IActionResult> Index(Guid projectid)
        {
            byte[] file = await contractService.DownloadContract(projectid);
            string fileName = "Contract-"+projectid.ToString()+".docx";
            return File(file, "application/octet-stream", fileName);
        }
        [HttpPost("generateForCompany")]
        public async Task<IActionResult> GenContractForCompany(ContractRequest request)
        {
            byte[] file = await contractService.GenNewConstractForCompany(request);
            //string fileName = "Contract-"+projectid.ToString()+".docx";
            string fileName = "Contract.docx";
            return File(file, "application/octet-stream", fileName);
        }
        [HttpPost("getDataForCompany")]
        public async Task<IActionResult> GetDataForCompany(Guid projectId)
        {
            var response = contractService.GetDataForCompanyContract(projectId);
            return Ok(response);
        }
        [HttpPost("generateForCustomer")]
        public async Task<IActionResult> GenContractForCustomer(ContractForCustomerRequest request)
        {
            byte[] file = await contractService.GenNewConstractForCustomer(request);
            //string fileName = "Contract-"+projectid.ToString()+".docx";
            string fileName = "Contract.docx";
            return File(file, "application/octet-stream", fileName);
        }
        [HttpPost("getDataForCustomer")]
        public async Task<IActionResult> GetDataForCustomer(Guid projectId)
        {
            var response = contractService.GetDataForCustomerContract(projectId);
            return Ok(response);
        }
        [HttpPost("pploadContract")]
        public async Task<IActionResult> UploadContract(Guid projectId, [FromForm]IFormFile file)
        {
            if ( await contractService.UploadContract(projectId, file) == false) return BadRequest();
            return Ok();
        }
    }
}
