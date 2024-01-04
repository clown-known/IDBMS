using API.Supporters.JwtAuthSupport;
using BusinessObject.Enums;
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
    public class BookingRequestsController : ODataController
    {
        private readonly BookingRequestService _service;
        private readonly PaginationService<BookingRequest> _paginationService;

        public BookingRequestsController(BookingRequestService service, PaginationService<BookingRequest> paginationService)
        {
            _service = service;
            _paginationService = paginationService;
        }

        [EnableQuery]
        [HttpGet]
        [Authorize(Policy = "User")]
        public IActionResult GetBookingRequests(BookingRequestStatus? status, string? contactName, int? pageSize, int? pageNo)
        {
            try
            {
                var list = _service.GetAll(status, contactName);

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
        [Authorize(Policy = "User")]
        public IActionResult UpdateBookingRequest(Guid id, [FromBody] BookingRequestRequest request)
        {
            try
            {
                _service.UpdateBookingRequest(id, request);
                var response = new ResponseMessage()
                {
                    Message = "Update successfully!",
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpPut("{id}/process")]
        [Authorize(Policy = "")]
        public IActionResult ProcessBookingRequest(Guid id, BookingRequestStatus status, [FromBody] string adminReply)
        {
            try
            {
                _service.ProcessBookingRequest(id, status, adminReply);
                var response = new ResponseMessage()
                {
                    Message = "Update successfully!",
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpPut("{id}/status")]
        [Authorize(Policy = "")]
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
        [Authorize(Policy = "User")]
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
