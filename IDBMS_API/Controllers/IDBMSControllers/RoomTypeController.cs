using API.Supporters.JwtAuthSupport;
using Azure.Core;
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
using Repository.Interfaces;
using System.Threading.Tasks;

namespace IDBMS_API.Controllers.IDBMSControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomTypesController : ODataController
    {
        private readonly RoomTypeService _service;
        private readonly PaginationService<RoomType> _paginationService;

        public RoomTypesController(RoomTypeService service, PaginationService<RoomType> paginationService)
        {
            _service = service;
            _paginationService = paginationService;
        }

        [EnableQuery]
        [HttpGet("{id}")]
        [Authorize(Policy = "Admin, Participation, ProjectManager")]
        public IActionResult GetRoomTypeById(int id)
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

        [EnableQuery]
        [HttpGet]
        [Authorize(Policy = "Admin, Participation, ProjectManager")]
        public IActionResult GetRoomTypes(bool? isHidden, string? name, int? pageSize, int? pageNo)
        {
            try
            {
                var list = _service.GetAll(isHidden, name);

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
        [Authorize(Policy = "Admin, Participation")]
        public async Task<IActionResult> CreateRoomType([FromForm][FromBody] RoomTypeRequest request)
        {
            try
            {
                var res = await _service.CreateRoomType(request);
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
        [Authorize(Policy = "Admin, Participation")]
        public IActionResult UpdateRoomType(int id, [FromForm][FromBody] RoomTypeRequest request)
        {
            try
            {
                _service.UpdateRoomType(id, request);
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

        [HttpPut("{id}/isHidden")]
        [Authorize(Policy = "Admin, Participation")]
        public IActionResult UpdateRoomTypeStatus(int id, bool isHidden)
        {
            try
            {
                _service.UpdateRoomTypeStatus(id, isHidden);
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
    }

}
