using BusinessObject.Models;
using IDBMS_API.Services.PaginationService;
using IDBMS_API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using BusinessObject.Enums;
using IDBMS_API.DTOs.Response;
using API.Supporters.JwtAuthSupport;

namespace IDBMS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ODataController
    {
        private readonly DashboardService _service;

        public DashboardController(DashboardService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize(Policy = "")]
        public IActionResult GetDashboardDataByAdmin()
        {
            try
            {

                var response = new ResponseMessage()
                {
                    Message = "Get successfully!",
                    Data = _service.GetDashboardDataByAdmin(),
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

        [HttpGet("userId")]
        [Authorize(Policy = "User")]
        public IActionResult GetDashboardDataByEngineerId(Guid userId)
        {
            try
            {

                var response = new ResponseMessage()
                {
                    Message = "Get successfully!",
                    Data = _service.GetDashboardDataByUserId(userId),
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
