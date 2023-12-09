using Azure.Core;
using BusinessObject.Models;
using IDBMS_API.DTOs.Request;
using IDBMS_API.DTOs.Response;
using IDBMS_API.Services;
using IDBMS_API.Services.PaginationService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Repository.Interfaces;
using System;

namespace IDBMS_API.Controllers.IDBMSControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FloorsController : ODataController
    {
        private readonly FloorService _service;
        private readonly PaginationService<Floor> _paginationService;


        public FloorsController(FloorService service, PaginationService<Floor> paginationService)
        {
            _service = service;
            _paginationService = paginationService;
        }

        [EnableQuery]
        [HttpGet]
        public IActionResult GetFloors(int? noOfFloor, string? usePurpose, int? pageSize, int? pageNo)
        {
            var list = _service.GetAll(noOfFloor, usePurpose);

            return Ok(_paginationService.PaginateList(list, pageSize, pageNo));
        }

        [EnableQuery]
        [HttpGet("{id}")]
        public IActionResult GetFloorById(Guid id)
        {
            return Ok(_service.GetById(id));
        }

        [EnableQuery]
        [HttpGet("project/{id}")]
        public IActionResult GetFloorsByProjectId(Guid id, int? noOfFloor, string? usePurpose, int? pageSize, int? pageNo)
        {
            var list = _service.GetByProjectId(id, noOfFloor, usePurpose);

            return Ok(_paginationService.PaginateList(list, pageSize, pageNo));
        }

        [HttpPost]
        public IActionResult CreateFloor([FromBody] FloorRequest request)
        {
            try
            {
                var result = _service.CreateFloor(request);
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
        public IActionResult UpdateFloor(Guid id, [FromBody] FloorRequest request)
        {
            try
            {
                _service.UpdateFloor(id, request);
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
        public IActionResult DeleteFloor(Guid id)
        {
            try
            {
                _service.DeleteFloor(id);
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
