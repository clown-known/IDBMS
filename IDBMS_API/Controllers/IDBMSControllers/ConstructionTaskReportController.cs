/*using BusinessObject.DTOs.Request;
using BusinessObject.DTOs.Response;
using IDBMS_API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace IDBMS_API.Controllers.IDBMSControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConstructionTaskReportsController : ODataController
    {
        private readonly ConstructionTaskReportService _service;

        public ConstructionTaskReportsController(ConstructionTaskReportService service)
        {
            _service = service;
        }

        [EnableQuery]
        [HttpGet]
        public IActionResult GetConstructionTaskReports()
        {
            var response = new ResponseMessage()
            {
                Message = "Get successfully!",
                Data = _service.GetAll()
            };
            return Ok(response);
        }
        [EnableQuery]
        [HttpGet("construction-task/{id}")]
        public IActionResult GetConstructionTaskReportsByConstructionTaskId(Guid id)
        {
            var response = new ResponseMessage()
            {
                Message = "Get successfully!",
                Data = _service.GetByConstructionTaskId(id)
            };
            return Ok(response);
        }
        [HttpPost]
        public IActionResult CreateConstructionTaskReport([FromBody] ConstructionTaskReportRequest request)
        {
            try
            {
                var result = _service.CreateConstructionTaskReport(request);
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
        public IActionResult UpdateConstructionTaskReport(Guid id, [FromBody] ConstructionTaskReportRequest request)
        {
            try
            {
                _service.UpdateConstructionTaskReport(id, request);
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
        public IActionResult DeleteConstructionTaskReport(Guid id)
        {
            try
            {
                _service.DeleteConstructionTaskReport(id);
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
*/