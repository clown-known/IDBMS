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
using System;
using System.Threading.Tasks;

namespace IDBMS_API.Controllers.IDBMSControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ODataController
    {
        private readonly RoomService _service;
        private readonly PaginationService<Room> _paginationService;

        public RoomsController(RoomService service, PaginationService<Room> paginationService)
        {
            _service = service;
            _paginationService = paginationService;
        }

        [EnableQuery]
        [HttpGet]
        [Authorize(Policy = "Admin, Participation, ProjectManager")]
        public IActionResult GetRooms(string? usePurpose, bool? isHidden, int? pageSize, int? pageNo)
        {
            try
            {
                var list = _service.GetAll(usePurpose, isHidden);

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

        [EnableQuery]
        [HttpGet("{id}")]
        [Authorize(Policy = "Admin, Participation, ProjectManager")]
        public IActionResult GetRoomById(Guid id)
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
        [HttpGet("floor/{id}")]
        [Authorize(Policy = "Admin, Participation, ProjectManager")]
        public IActionResult GetRoomsByFloorId(Guid id, string? usePurpose, bool? isHidden, int? pageSize, int? pageNo)
        {
            try
            {
                var list = _service.GetByFloorId(id, usePurpose, isHidden);

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
        [Authorize(Policy = "Admin, Participation, ProjectManager")]
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
        [Authorize(Policy = "Admin, Participation, ProjectManager")]
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
        [Authorize(Policy = "Admin, Participation, ProjectManager")]
        public IActionResult UpdateRoomStatus(Guid id, bool isHidden, Guid projectId)
        {
            try
            {
                _service.UpdateRoomStatus(id, isHidden, projectId);
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
    }
}
