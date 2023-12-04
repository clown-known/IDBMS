using IDBMS_API.DTOs.Request;
using IDBMS_API.DTOs.Response;
using BusinessObject.Enums;
using IDBMS_API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace IDBMS_API.Controllers.IDBMSControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectTasksController : ODataController
    {
        private readonly ProjectTaskService _service;

        public ProjectTasksController(ProjectTaskService service)
        {
            _service = service;
        }

        [EnableQuery]
        [HttpGet]
        public IActionResult GetProjectTasks()
        {
            return Ok(_service.GetAll());
        }        
        
        [EnableQuery]
        [HttpGet("{id}")]
        public IActionResult GetProjectTaskById(Guid id)
        {
            return Ok(_service.GetById(id));
        }

        [EnableQuery]
        [HttpGet("project/{id}/interior-items")]
        public IActionResult GetSuggestionTasksByProjectId(Guid id)
        {
            return Ok(_service.GetSuggestionTasksByProjectId(id));
        }

        [EnableQuery]
        [HttpGet("room/{id}/interior-items")]
        public IActionResult GetSuggestionTasksByRoomId(Guid id)
        {
            return Ok(_service.GetSuggestionTasksByRoomId(id));
        }

        [EnableQuery]
        [HttpGet("project/{id}")]
        public IActionResult GetProjectTasksByProjectId(Guid id)
        {
            return Ok(_service.GetByProjectId(id));
        }

        [EnableQuery]
        [HttpGet("room/{id}")]
        public IActionResult GetProjectTasksByRoomId(Guid id)
        {
            return Ok(_service.GetByRoomId(id));
        }

        [EnableQuery]
        [HttpGet("payment-stage/{id}")]
        public IActionResult GetProjectTasksByPaymentStageId(Guid id)
        {
            return Ok(_service.GetByPaymentStageId(id));
        }
        [HttpPost]
        public IActionResult CreateProjectTask([FromBody] ProjectTaskRequest request)
        {
            try
            {
                var result = _service.CreateProjectTask(request);
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

        [HttpPost("interior-item")]
        public IActionResult CreateProjectTaskWithCustomItem([FromBody] CustomItemSuggestionRequest request)
        {
            try
            {
                var result = _service.CreateProjectTaskWithCustomItem(request);
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
        public IActionResult UpdateProjectTask(Guid id, [FromBody] ProjectTaskRequest request)
        {
            try
            {
                _service.UpdateProjectTask(id, request);
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

        [HttpPut("payment-stage/{id}")]
        public IActionResult AssignTasksToStage(Guid id,[FromBody] List<Guid> listTaskId)
        {
            try
            {
                _service.AssignTasksToStage(id, listTaskId);
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

        [HttpPut("StartedDate")]
        public IActionResult StartTasksOfStage(Guid paymentStageId)
        {
            try
            {
                _service.StartTasksOfStage(paymentStageId);
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

        [HttpPut("{id}/status")]
        public IActionResult UpdateProjectTaskStatus(Guid id, ProjectTaskStatus status)
        {
            try
            {
                _service.UpdateProjectTaskStatus(id, status);
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
