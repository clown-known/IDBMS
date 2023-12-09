using Azure.Core;
using BusinessObject.Models;
using IDBMS_API.DTOs.Request;
using IDBMS_API.Services;
using IDBMS_API.Services.PaginationService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Repository.Interfaces;

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
        [HttpGet]
        public IActionResult GetRoomTypes(bool? isHidden, string? name, int? pageSize, int? pageNo)
        {
            var list = _service.GetAll(isHidden, name);

            return Ok(_paginationService.PaginateList(list, pageSize, pageNo));
        }

        [HttpPost]
        public IActionResult CreateRoomType([FromBody][FromForm] RoomTypeRequest request)
        {
            try
            {
                var res = _service.CreateRoomType(request);
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
        public IActionResult UpdateRoomType(int id, [FromBody][FromForm] RoomTypeRequest request)
        {
            try
            {
                _service.UpdateRoomType(id, request);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpPut("{id}/isHidden")]
        public IActionResult UpdateRoomTypeStatus(int id, bool isHidden)
        {
            try
            {
                _service.UpdateRoomTypeStatus(id, isHidden);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
    }

}
