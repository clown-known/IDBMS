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
    public class TaskAssignmentsController : ODataController
    {
        private readonly TaskAssignmentService _service;
        private readonly PaginationService<TaskAssignment> _paginationService;

        public TaskAssignmentsController(TaskAssignmentService service, PaginationService<TaskAssignment> paginationService)
        {
            _service = service;
            _paginationService = paginationService;
        }
        [EnableQuery]
        [HttpGet]
        [Authorize(Policy = "Participation")]
        public IActionResult GetTaskAssignments(Guid projectId, string? name, int? pageSize, int? pageNo)
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
        [HttpGet("project/{id}")]
        [Authorize(Policy = "Participation")]
        public IActionResult GetTaskAssignmentsByProjectId(Guid projectId, Guid id, string? name, int? pageSize, int? pageNo)
        {
            try
            {
                var list = _service.GetByProjectId(id, name);

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
        [HttpGet("project-task/{id}")]
        [Authorize(Policy = "Participation")]
        public IActionResult GetTaskAssignmentsByTaskId(Guid projectId, Guid id, string? name, int? pageSize, int? pageNo)
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

        [EnableQuery]
        [HttpGet("user/{userId}")]
        [Authorize(Policy = "Participation")]
        public IActionResult GetTaskAssignmentsOfUserInProject(Guid projectId, Guid userId, int? pageSize, int? pageNo)
        {
            try
            {
                var list = _service.GetTaskAssignmentsOfUserInProject(projectId, userId);

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
        [Authorize(Policy = "ProjectManager")]
        public IActionResult CreateTaskAssignment(Guid projectId, [FromBody] TaskAssignmentRequest request)
        {
                try
                {
                    _service.CreateTaskAssignment(request);
                    var response = new ResponseMessage()
                    {
                        Message = "Create successfully!",
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

        [HttpPut("project-task/{id}")]
        [Authorize(Policy = "ProjectManager")]
        public IActionResult UpdateTaskAssignment(Guid projectId, Guid id, [FromBody] List<Guid> request)
        {
            try
            {
                _service.UpdateTaskAssignmentByTaskId(id, request);
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
        [Authorize(Policy = "ProjectManager")]
        public IActionResult DeleteTaskAssignment(Guid projectId, Guid id)
        {
            try
            {
                _service.DeleteTaskAssignment(id);
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
