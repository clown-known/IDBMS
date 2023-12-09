using Azure.Core;
using IDBMS_API.DTOs.Request;
using IDBMS_API.DTOs.Response;
using BusinessObject.Enums;
using IDBMS_API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Repository.Interfaces;
using System;
using BusinessObject.Models;
using IDBMS_API.Services.PaginationService;

namespace IDBMS_API.Controllers.IDBMSControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ODataController
    {
        private readonly ProjectService _service;
        private readonly PaginationService<Project> _paginationService;

        public ProjectsController(ProjectService service, PaginationService<Project> paginationService)
        {
            _service = service;
            _paginationService = paginationService;
        }

        [EnableQuery]
        [HttpGet]
        public IActionResult GetProjects(int? pageSize, int? pageNo, ProjectType? status, string? name)
        {
            var list = _service.GetAll(status, name);

            return Ok(_paginationService.PaginateList(list, pageSize, pageNo));
        }

        [EnableQuery]
        [HttpGet("{id}")]
        public IActionResult GetProjectById(Guid id)
        {
            return Ok(_service.GetById(id));
        }

        [EnableQuery]
        [HttpGet("site/{id}")]
        public IActionResult GetProjectsBySiteId(Guid id, int? pageSize, int? pageNo, ProjectType? status, string? name)
        {
            var list = _service.GetBySiteId(id, status, name);

            return Ok(_paginationService.PaginateList(list, pageSize, pageNo));
        }

        [HttpPost]
        public IActionResult BookDecorProject([FromBody] ProjectRequest request)
        {
            try
            {
                var result = _service.CreateProject(request);
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
