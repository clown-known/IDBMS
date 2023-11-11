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
    public class DecorProgressReportsController : ODataController
    {
        private readonly DecorProgressReportService _service;

        public DecorProgressReportsController(DecorProgressReportService service)
        {
            _service = service;
        }

        [EnableQuery]
        [HttpGet]
        public IActionResult GetDecorProgressReports()
        {
            var response = new ResponseMessage()
            {
                Message = "Get successfully!",
                Data = _service.GetAll()
            };
            return Ok(response);
        }
        [EnableQuery]
        [HttpGet("payment-stage/{id}")]
        public IActionResult GetDecorProgressReportsByPaymentStageId(Guid id)
        {
            var response = new ResponseMessage()
            {
                Message = "Get successfully!",
                Data = _service.GetByPaymentStageId(id)
            };
            return Ok(response);
        }
        [HttpPost]
        public IActionResult CreateDecorProgressReport([FromBody] DecorProgressReportRequest request)
        {
            try
            {
                var result = _service.CreateDecorProgressReport(request);
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
        public IActionResult UpdateDecorProgressReport(Guid id, [FromBody] DecorProgressReportRequest request)
        {
            try
            {
                _service.UpdateDecorProgressReport(id, request);
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
        public IActionResult DeleteDecorProgressReport(Guid id)
        {
            try
            {
                _service.DeleteDecorProgressReport(id);
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