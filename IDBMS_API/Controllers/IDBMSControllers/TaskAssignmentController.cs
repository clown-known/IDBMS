using BusinessObject.DTOs.Request;
using BusinessObject.DTOs.Response;
using IDBMS_API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace IDBMS_API.Controllers.IDBMSControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskAssignmentsController : ODataController
    {
        private readonly TaskAssignmentService _service;

        public TaskAssignmentsController(TaskAssignmentService service)
        {
            _service = service;
        }

        [EnableQuery]
        [HttpGet]
        public IActionResult GetTaskAssignments()
        {
            return Ok(_service.GetAll());
        }
        //lead arc, cons man
        [EnableQuery]
        [HttpGet("project/{id}")]
        public IActionResult GetTaskAssignmentsByProjectId(Guid id)
        {
            return Ok(_service.GetByProjectId(id));
        }
        //lead arc, cons man
        [EnableQuery]
        [HttpGet("user/{id}")]
        public IActionResult GetTaskAssignmentsByUserId(Guid id)
        {
            return Ok(_service.GetByUserId(id));
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

        [HttpPut("{id}")]
        public IActionResult UpdateTaskAssignment(Guid id, [FromBody] TaskAssignmentRequest request)
        {
            try
            {
                _service.UpdateTaskAssignment(id, request);
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
