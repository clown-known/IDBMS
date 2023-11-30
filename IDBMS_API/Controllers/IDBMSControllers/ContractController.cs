/*using IDBMS_API.Services;
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
    }
}
*/