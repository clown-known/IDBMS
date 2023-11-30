using BusinessObject.Enums;
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
    public class BookingRequestsController : ODataController
    {
        private readonly BookingRequestService _service;

        public BookingRequestsController(BookingRequestService service)
        {
            _service = service;
        }

        [EnableQuery]
        [HttpGet]
        public IActionResult GetBookingRequests()
        {
            return Ok(_service.GetAll());
        }

        [HttpPost]
        public IActionResult CreateBookingRequest([FromBody] BookingRequestRequest request)
        {
            try
            {
                var res = _service.CreateBookingRequest(request);
                if (res == null)
                {
                    return BadRequest("Failed to create object");
                }
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBookingRequest(Guid id, [FromBody] BookingRequestRequest request)
        {
            try
            {
                _service.UpdateBookingRequest(id, request);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpPut("{id}/status")]
        public IActionResult UpdateBookingRequestStatus(Guid id, BookingRequestStatus status)
        {
            try
            {
                _service.UpdateBookingRequestStatus(id, status);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBookingRequest(Guid id)
        {
            try
            {
                _service.DeleteBookingRequest(id);
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
