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
        public IActionResult GetTaskAssignments(string? name, int? pageSize, int? pageNo)
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
        //lead arc, cons man
        [EnableQuery]
        [HttpGet("project/{id}")]
        public IActionResult GetTaskAssignmentsByProjectId(Guid id, string? name, int? pageSize, int? pageNo)
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
        //lead arc, cons man
        [EnableQuery]
        [HttpGet("user/{id}")]
        public IActionResult GetTaskAssignmentsByUserId(Guid id, string? name, int? pageSize, int? pageNo)
        {
            try
            {
                var list = _service.GetByUserId(id, name);

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
        public IActionResult CreateTaskAssignment([FromBody] TaskAssignmentRequest request)
        {
                try
                {
                    var result = _service.CreateTaskAssignment(request);
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

        [HttpDelete("{id}")]
        public IActionResult DeleteTaskAssignment(Guid id)
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
