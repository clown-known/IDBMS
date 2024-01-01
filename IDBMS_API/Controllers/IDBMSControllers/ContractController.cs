using API.Supporters.JwtAuthSupport;
using BusinessObject.Models;
using DocumentFormat.OpenXml.Wordprocessing;
using IDBMS_API.DTOs.Request;
using IDBMS_API.DTOs.Response;
using IDBMS_API.Services;
using IDBMS_API.Services.PaginationService;
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
        [HttpGet("{projectId}/download")]
        [Authorize(Policy = "Admin, Participation, ProjectManager, Owner")]
        public async Task<IActionResult> Index(Guid projectId)
        {
            try
            {
                byte[] file = await contractService.DownloadContract(projectId);
                string fileName = "Contract-" + projectId.ToString() + ".docx";

                var response = new ResponseMessage()
                {
                    Message = "Download successfully!",
                    Data = File(file, "application/octet-stream", fileName),
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new ResponseMessage()
                {
                    Message = $"Error: {ex.Message}"
                };
                return BadRequest(response);
            }
        }
        [HttpPost("company")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> GenContractForCompany(ContractRequest request)
        {
            try
            {
                byte[] file = await contractService.GenNewConstractForCompany(request);
                //string fileName = "Contract-"+projectid.ToString()+".docx";
                string fileName = "Contract.docx";

                var response = new ResponseMessage()
                {
                    Message = "Generate successfully!",
                    Data = File(file, "application/octet-stream", fileName),
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new ResponseMessage()
                {
                    Message = $"Error: {ex.Message}"
                };
                return BadRequest(response);
            }
        }
        [HttpGet("{projectId}/company")]
        [Authorize(Policy = "Admin, Participation, ProjectManager, Owner")]
        public async Task<IActionResult> GetDataForCompany(Guid projectId)
        {
            try
            {
                var result = contractService.GetDataForCompanyContract(projectId);

                var response = new ResponseMessage()
                {
                    Message = "Get successfully!",
                    Data = result,
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new ResponseMessage()
                {
                    Message = $"Error: {ex.Message}"
                };
                return BadRequest(response);
            }
        }
        [HttpPost("individual")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> GenContractForCustomer(ContractForCustomerRequest request)
        {
            try
            {
                byte[] file = await contractService.GenNewConstractForCustomer(request);
                //string fileName = "Contract-"+projectid.ToString()+".docx";
                string fileName = "Contract.docx";

                var response = new ResponseMessage()
                {
                    Message = "Generate successfully!",
                    Data = File(file, "application/octet-stream", fileName),
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new ResponseMessage()
                {
                    Message = $"Error: {ex.Message}"
                };
                return BadRequest(response);
            }
        }
        [HttpGet("{projectId}/individual")]
        [Authorize(Policy = "Admin, Participation, ProjectManager, Owner")]
        public async Task<IActionResult> GetDataForCustomer(Guid projectId)
        {
            try
            {
                var result = contractService.GetDataForCustomerContract(projectId);

                var response = new ResponseMessage()
                {
                    Message = "Get successfully!",
                    Data = result,
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new ResponseMessage()
                {
                    Message = $"Error: {ex.Message}"
                };
                return BadRequest(response);
            }
        }
        [HttpPost("{projectId}/upload")]
        [Authorize(Policy = "Admin, Participation, ProjectManager, Owner")]
        public async Task<IActionResult> UploadContract(Guid projectId, [FromForm]IFormFile file)
        {
            try
            {
                var result = await contractService.UploadContract(projectId, file);

                if (result == false) throw new Exception("Upload contract return false, upload failed!");

                var response = new ResponseMessage()
                {
                    Message = "Upload successfully!",
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new ResponseMessage()
                {
                    Message = $"Error: {ex.Message}"
                };
                return BadRequest(response);
            }
        }
    }
}
