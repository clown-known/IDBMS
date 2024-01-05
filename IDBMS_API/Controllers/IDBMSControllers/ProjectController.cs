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
using DocumentFormat.OpenXml.Drawing.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Threading.Tasks;
using API.Supporters.JwtAuthSupport;
using DocumentFormat.OpenXml.Office2010.Excel;

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
        [Authorize(Policy = "Participation")]
        public IActionResult GetProjects(Guid projectId, int? pageSize, int? pageNo, ProjectType? type, ProjectStatus? status, string? name)
        {
            try
            {
                var list = _service.GetAll(type, status, name);

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
        [Authorize(Policy = "Participation")]
        public IActionResult GetProjectById(Guid projectId, Guid id)
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
        [HttpGet("site/{id}")]
        [Authorize(Policy = "")]
        public IActionResult GetProjectsBySiteId(Guid id, int? pageSize, int? pageNo, ProjectType? type, ProjectStatus? status, string? name)
        {
            try
            {
                var list = _service.GetBySiteId(id, type, status, name);

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

        [HttpGet("status")]
        [Authorize(Policy = "User")]
        public IActionResult CountParticipationsByProjectStatus(Guid userId)
        {
            try
            {
                var list = _service.CountParticipationsByProjectStatus(userId);

                var response = new ResponseMessage()
                {
                    Message = "Get successfully!",
                    Data = list
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
        [Authorize(Policy = "")]
        public IActionResult CreateProject([FromBody] ProjectRequest request)
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
        [Authorize(Policy = "ProjectManager")]
        public IActionResult UpdateProject(Guid projectId, Guid id, [FromBody] ProjectRequest request)
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
        [Authorize(Policy = "ProjectManager")]
        public IActionResult UpdateProjectStatus(Guid projectId, Guid id, ProjectStatus status)
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

    }
}
