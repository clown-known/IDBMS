using Azure.Core;
using BusinessObject.DTOs.Request;
using BusinessObject.DTOs.Request.BookingRequest;
using BusinessObject.DTOs.Response;
using BusinessObject.Enums;
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
    public class ProjectsController : ODataController
    {
        private readonly ProjectService _service;

        public ProjectsController(ProjectService service)
        {
            _service = service;
        }

        [EnableQuery]
        [HttpGet]
        public IActionResult GetProjects()
        {
            return Ok(_service.GetAll());
        }

        [EnableQuery]
        [HttpGet("{id}")]
        public IActionResult GetProjectById(Guid id)
        {
            return Ok(_service.GetById(id));
        }

        [HttpPost("decor")]
        public IActionResult BookDecorProject([FromBody] BookingDecorProjectRequest request)
        {
            try
            {
                var result = _service.BookDecorProject(request);
                var response = new ResponseMessage()
                {
                    Message = "Book successfully!",
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

        [HttpPost("construction")]
        public IActionResult BookConstructionProject([FromBody] BookingConstructionProjectRequest request)
        {
            try
            {
                var result = _service.BookConstructionProject(request);
                var response = new ResponseMessage()
                {
                    Message = "Book successfully!",
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
        public IActionResult UpdateProject(Guid id, [FromBody] ProjectRequest request)
        {
            try
            {
                _service.UpdateProject(id, request);
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

        [HttpPut("{id}/status")]
        public IActionResult UpdateProjectStatus(Guid id, ProjectStatus status)
        {
            try
            {
                _service.UpdateProjectStatus(id, status);
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

        [HttpPut("{id}/isAdvertisement")]
        public IActionResult UpdateProjectAdvertisementStatus(Guid id, AdvertisementStatus status)
        {
            try
            {
                _service.UpdateProjectAdvertisementStatus(id, status);
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
