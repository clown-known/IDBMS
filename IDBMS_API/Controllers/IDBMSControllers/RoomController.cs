using Azure.Core;
using BusinessObject.DTOs.Request;
using BusinessObject.DTOs.Response;
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
    public class RoomsController : ODataController
    {
        private readonly RoomService _service;

        public RoomsController(RoomService service)
        {
            _service = service;
        }

        [EnableQuery]
        [HttpGet]
        public IActionResult GetRooms()
        {
            var response = new ResponseMessage()
            {
                Message = "Get successfully!",
                Data = _service.GetAll()
            };
            return Ok(_service.GetAll());
        }

        /*[EnableQuery]
        [HttpGet("project/{id}")]
        public IActionResult GetRoomsByProjectId([FromQuery] Guid id)
        {
            var response = new ResponseMessage()
            {
                Message = "Get successfully!",
                Data = _service.GetByProjectId(id)
            };
            return Ok(response);
        }*/

        [EnableQuery]
        [HttpGet("floor/{id}")]
        public IActionResult GetRoomsByFloorId([FromQuery] Guid id)
        {
            var response = new ResponseMessage()
            {
                Message = "Get successfully!",
                Data = _service.GetByFloorId(id)
            };
            return Ok(_service.GetByFloorId(id));
        }

        [HttpPost]
        public IActionResult CreateRoom([FromBody] RoomRequest request)
        {
            try
            {
                var result = _service.CreateRoom(request);
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
        public IActionResult UpdateRoom(Guid id, [FromBody] RoomRequest request)
        {
            try
            {
                _service.UpdateRoom(id, request);
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

        [HttpPut("{id}/isHidden")]
        public IActionResult UpdateRoom(Guid id, bool isHidden)
        {
            try
            {
                _service.UpdateRoomStatus(id, isHidden);
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
        public IActionResult DeleteRoom(Guid id)
        {
            try
            {
                _service.DeleteRoom(id);
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
