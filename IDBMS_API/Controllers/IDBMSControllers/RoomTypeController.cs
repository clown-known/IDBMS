using Azure.Core;
using IDBMS_API.DTOs.Request;
using IDBMS_API.Services;
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

        public RoomTypesController(RoomTypeService service)
        {
            _service = service;
        }

        [EnableQuery]
        [HttpGet]
        public IActionResult GetRoomTypes()
        {
            return Ok(_service.GetAll());
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
