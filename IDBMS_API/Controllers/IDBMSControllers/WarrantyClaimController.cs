using BusinessObject.Models;
using DocumentFormat.OpenXml.Drawing.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using IDBMS_API.DTOs.Request;
using IDBMS_API.DTOs.Response;
using IDBMS_API.Services;
using IDBMS_API.Services.PaginationService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using System.Threading.Tasks;

namespace IDBMS_API.Controllers.IDBMSControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarrantyClaimsController : ODataController
    {
        private readonly WarrantyClaimService _service;
        private readonly PaginationService<WarrantyClaim> _paginationService;

        public WarrantyClaimsController(WarrantyClaimService service, PaginationService<WarrantyClaim> paginationService)
        {
            _service = service;
            _paginationService = paginationService;
        }

        [EnableQuery]
        [HttpGet]
        public IActionResult GetWarrantyClaims(bool? isCompanyCover, string? name, int? pageSize, int? pageNo)
        {
            try
            {
                var list = _service.GetAll(isCompanyCover, name);

                var response = new ResponseMessage()
                {
                    Message = "Get successfully!",
                    Data = _paginationService.PaginateList(list, pageSize, pageNo)
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

        [EnableQuery]
        [HttpGet("user/{id}")]
        public IActionResult GetWarrantyClaimsByUserId(Guid id, bool? isCompanyCover, string? name, int? pageSize, int? pageNo)
        {
            try
            {
                var list = _service.GetByUserId(id, isCompanyCover, name);

                var response = new ResponseMessage()
                {
                    Message = "Get successfully!",
                    Data = _paginationService.PaginateList(list, pageSize, pageNo)
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
        [EnableQuery]
        [HttpGet("project/{id}")]
        public IActionResult GetWarrantyClaimsByProjectId(Guid id, bool? isCompanyCover, string? name, int? pageSize, int? pageNo)
        {
            try
            {
                var list = _service.GetByUserId(id, isCompanyCover, name);

                var response = new ResponseMessage()
                {
                    Message = "Get successfully!",
                    Data = _paginationService.PaginateList(list, pageSize, pageNo)
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
        [HttpPost]
        public IActionResult CreateWarrantyClaim([FromBody][FromForm] WarrantyClaimRequest request)
        {
                try
                {
                    var result = _service.CreateWarrantyClaim(request);
                    var response = new ResponseMessage()
                    {
                        Message = "Create successfully!",
                        Data = result
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

        [HttpPut("{id}")]
        public IActionResult UpdateWarrantyClaim(Guid id, [FromBody][FromForm] WarrantyClaimRequest request)
        {
            try
            {
                _service.UpdateWarrantyClaim(id, request);
                var response = new ResponseMessage()
                {
                    Message = "Update successfully!",
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

        [HttpDelete("{id}")]
        public IActionResult DeleteWarrantyClaim(Guid id, Guid projectId)
        {
            try
            {
                _service.DeleteWarrantyClaim(id, projectId);
                var response = new ResponseMessage()
                {
                    Message = "Delete successfully!",
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
