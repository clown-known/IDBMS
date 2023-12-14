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
    public class TaskDocumentsController : ODataController
    {
        private readonly TaskDocumentService _service;

        public TaskDocumentsController(TaskDocumentService service)
        {
            _service = service;
        }

        [EnableQuery]
        [HttpGet]
        public IActionResult GetTaskDocuments()
        {
            return Ok(_service.GetAll());
        }

        [EnableQuery]
        [HttpGet("task-report/{id}")]
        public IActionResult GetTaskDocumentsByTaskReportId(Guid id)
        {
            return Ok(_service.GetByTaskReportId(id));
        }

        [EnableQuery]
        [HttpGet("{id}")]
        public IActionResult GetTaskDocumentById(Guid id)
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
        public IActionResult CreateTaskDocument([FromBody] TaskDocumentRequest request)
        {
            try
            {
                try
                {
                    var result = _service.CreateTaskDocument(request);
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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTaskDocument(Guid id)
        {
            try
            {
                _service.DeleteTaskDocument(id);
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
