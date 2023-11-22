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
    public class TaskReportsController : ODataController
    {
        private readonly TaskReportService _service;

        public TaskReportsController(TaskReportService service)
        {
            _service = service;
        }

        [EnableQuery]
        [HttpGet]
        public IActionResult GetTaskReports()
        {
            var response = new ResponseMessage()
            {
                Message = "Get successfully!",
                Data = _service.GetAll()
            };
            return Ok(_service.GetAll());
        }
        [EnableQuery]
        [HttpGet("project-task/{id}")]
        public IActionResult GetTaskReportsByProjectTaskId(Guid id)
        {
            var response = new ResponseMessage()
            {
                Message = "Get successfully!",
                Data = _service.GetByTaskId(id)
            };
            return Ok(_service.GetByTaskId(id));
        }
        //for Manager to view their list
        [EnableQuery]
        [HttpGet("user/{id}")]
        public IActionResult GetTaskReportsByUserId(Guid id)
        {
            var response = new ResponseMessage()
            {
                Message = "Get successfully!",
                Data = _service.GetByUserId(id)
            };
            return Ok(_service.GetByUserId(id));
        }
        [HttpPost]
        public IActionResult CreateTaskReport([FromBody] TaskReportRequest request)
        {
            try
            {
                var result = _service.CreateTaskReport(request);
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
