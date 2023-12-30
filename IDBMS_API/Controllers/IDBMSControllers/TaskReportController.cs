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
    public class TaskReportsController : ODataController
    {
        private readonly TaskReportService _service;
        private readonly PaginationService<TaskReport> _paginationService;

        public TaskReportsController(TaskReportService service, PaginationService<TaskReport> paginationService)
        {
            _service = service;
            _paginationService = paginationService;
        }

        [EnableQuery]
        [HttpGet]
        [Authorize(Policy = "Admin, Participation, Architect, ConstructionManager, Viewer")]
        public IActionResult GetTaskReports(string? name, int? pageSize, int? pageNo)
        {
            try
            {
                var list = _service.GetAll(name);

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
        [Authorize(Policy = "Admin, Participation, Architect, ConstructionManager, Viewer")]
        public IActionResult GetTaskReportById(Guid id)
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

        [EnableQuery]
        [HttpGet("project-task/{id}")]
        [Authorize(Policy = "Admin, Participation, Architect, ConstructionManager, Viewer")]
        public IActionResult GetTaskReportsByProjectTaskId(Guid id, string? name, int? pageSize, int? pageNo)
        {
            try
            {
                var list = _service.GetByTaskId(id, name);

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
        [Authorize(Policy = "Admin, Participation, Architect, ConstructionManager")]
        public async Task<IActionResult> CreateTaskReport(Guid projectId,[FromBody] TaskReportRequest request)
        {
            try
            {
                var result = await _service.CreateTaskReport(projectId,request);
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
        [Authorize(Policy = "Admin, Participation, Architect, ConstructionManager")]
        public IActionResult UpdateTaskReport(Guid id, [FromBody] TaskReportRequest request)
        {
            try
            {
                _service.UpdateTaskReport(id, request);
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
        [Authorize(Policy = "Admin, Participation, Architect, ConstructionManager")]
        public IActionResult DeleteTaskReport(Guid id)
        {
            try
            {
                _service.DeleteTaskReport(id);
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
