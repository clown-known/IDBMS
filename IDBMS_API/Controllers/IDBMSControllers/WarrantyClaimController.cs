using API.Supporters.JwtAuthSupport;
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
        [Authorize(Policy = "Admin, Participation, ProjectManager, Viewer")]
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
        [Authorize(Policy = "Admin, Participation, ProjectManager, Viewer")]
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
        [Authorize(Policy = "Admin, Participation, ProjectManager, Viewer")]
        public IActionResult GetWarrantyClaimsByProjectId(Guid projectId, bool? isCompanyCover, string? name, int? pageSize, int? pageNo)
        {
            try
            {
                var list = _service.GetByProjectId(projectId, isCompanyCover, name);

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
        [HttpGet("{id}")]
        [Authorize(Policy = "Admin, Participation, ProjectManager, Viewer")]
        public IActionResult GetWarrantyClaimById(Guid id)
        {
            try
            {
                var response = new ResponseMessage()
                {
                    Message = "Get successfully!",
                    Data = _service.GetById(id),
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
        [Authorize(Policy = "Admin, Participation")]
        public async Task<IActionResult> CreateWarrantyClaim([FromForm][FromBody] WarrantyClaimRequest request)
        {
                try
                {
                    var result = await _service.CreateWarrantyClaim(request);
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
        [Authorize(Policy = "Admin, Participation, ProjectManager")]
        public IActionResult UpdateWarrantyClaim(Guid id, [FromForm][FromBody] WarrantyClaimRequest request)
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
        [Authorize(Policy = "Admin, Participation, ProjectManager")]
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
