using IDBMS_API.DTOs.Request;
using IDBMS_API.DTOs.Response;
using IDBMS_API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace IDBMS_API.Controllers.IDBMSControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemInTasksController : ODataController
    {
        private readonly ItemInTaskService _service;

        public ItemInTasksController(ItemInTaskService service)
        {
            _service = service;
        }

        [EnableQuery]
        [HttpGet]
        public IActionResult GetItemInTasks()
        {
            return Ok(_service.GetAll());
        }

        [EnableQuery]
        [HttpGet("{id}")]
        public IActionResult GetItemInTaskById(Guid id)
        {
            return Ok(_service.GetById(id));
        }

        [EnableQuery]
        [HttpGet("project/{id}")]
        public IActionResult GetItemInTaskByProjectId(Guid id)
        {
            return Ok(_service.GetByProjectId(id));
        }

        [EnableQuery]
        [HttpGet("room/{id}")]
        public IActionResult GetItemInTaskByRoomId(Guid id)
        {
            return Ok(_service.GetByRoomId(id));
        }

        [EnableQuery]
        [HttpGet("project-task/{id}")]
        public IActionResult GetItemInTaskByTaskId(Guid id)
        {
            return Ok(_service.GetByTaskId(id));
        }


        [HttpPost]
        public IActionResult CreateItemInTask([FromBody] ItemInTaskRequest request)
        {
            try
            {
                var result = _service.CreateItemInTask(request);
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
        public IActionResult UpdateItemInTask(Guid id, [FromBody] ItemInTaskRequest request)
        {
            try
            {
                _service.UpdateItemInTask(id, request);
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
