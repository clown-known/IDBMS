using Azure.Core;
using BusinessObject.DTOs.Request;
using IDBMS_API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Repository.Interfaces;
using System;

namespace IDBMS_API.Controllers.IDBMSControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ODataController
    {
        private readonly RoomService _service;

        public RoomController(RoomService service)
        {
            _service = service;
        }

        [EnableQuery]
        [HttpGet]
        public IActionResult GetRooms()
        {
            return Ok(_service.GetAll());
        }

        [EnableQuery]
        [HttpGet("project/{id}")]
        public IActionResult GetRoomsByProjectId([FromQuery] Guid id)
        {
            return Ok(_service.GetByProjectId(id));
        }

        [EnableQuery]
        [HttpGet("floor/{id}")]
        public IActionResult GetRoomsByFloorId([FromQuery] Guid id)
        {
            return Ok(_service.GetByFloorId(id));
        }

        [HttpPost]
        public IActionResult CreateRoom([FromBody] RoomRequest request)
        {
            try
            {
                var res = _service.CreateRoom(request);
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
        public IActionResult UpdateRoom(Guid id, [FromBody] RoomRequest request)
        {
            try
            {
                _service.UpdateRoom(id, request);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpPut("{id}/isHidden")]
        public IActionResult UpdateRoom(Guid id, bool isHidden)
        {
            try
            {
                _service.UpdateRoomStatus(id, isHidden);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteRoom(Guid id)
        {
            try
            {
                _service.DeleteRoom(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
    }
}
