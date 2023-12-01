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
        [HttpGet]
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
        public async Task<IActionResult> GetDataForCompany(Guid projectid)
        {
            var response = contractService.GetDataForCompanyContract(projectid);
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
    }
}
