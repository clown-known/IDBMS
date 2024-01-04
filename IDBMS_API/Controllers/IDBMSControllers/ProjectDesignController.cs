using API.Supporters.JwtAuthSupport;
using BusinessObject.Enums;
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
    public class ProjectDesignsController : ODataController
    {
        private readonly ProjectDesignService _service;
        private readonly PaginationService<ProjectDesign> _paginationService;

        public ProjectDesignsController(ProjectDesignService service, PaginationService<ProjectDesign> paginationService)
        {
            _service = service;
            _paginationService = paginationService;
        }

        [EnableQuery]
        [HttpGet]
        [Authorize(Policy = "")]
        public IActionResult GetProjectDesigns(ProjectType? type, string? name, bool? isHidden, int? pageSize, int? pageNo)
        {
            try
            {
                var list = _service.GetAll(type, name, isHidden);

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
        [Authorize(Policy = "")]
        public IActionResult GetProjectDesignById(int id)
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
        [Authorize(Policy = "")]
        public IActionResult CreateProjectDesign([FromBody] ProjectDesignRequest request)
        {
            try
            {
                var result = _service.CreateProjectDesign(request);
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
        [Authorize(Policy = "")]
        public IActionResult UpdateProjectDesign(int id, [FromBody] ProjectDesignRequest request)
        {
            try
            {
                _service.UpdateProjectDesign(id, request);
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

        [HttpPut("{id}/isHidden")]
        [Authorize(Policy = "")]
        public IActionResult UpdateProjectDesignStatus(int id, bool isHidden)
        {
            try
            {
                _service.UpdateProjectDesign(id, isHidden);
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
    }

}
