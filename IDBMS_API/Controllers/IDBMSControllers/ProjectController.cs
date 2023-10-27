using Azure.Core;
using BusinessObject.DTOs.Request;
using BusinessObject.Enums;
using IDBMS_API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Repository.Interfaces;
using System;

namespace IDBMS_API.Controllers.IDBMSControllers
{
    public class ProjectController : ODataController
    {
        private readonly ProjectService _service;

        public ProjectController(ProjectService service)
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

        [HttpPost]
        public IActionResult CreateProject([FromBody] ProjectRequest request)
        {
            try
            {
                var res = _service.CreateProject(request);
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
        public IActionResult UpdateProject(Guid id, [FromBody] ProjectRequest request)
        {
            try
            {
                _service.UpdateProject(id, request);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpPut("{id}/status")]
        public IActionResult DeleteProject(Guid id, ProjectStatus status)
        {
            try
            {
                _service.UpdateProjectStatus(id, status);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
    }
}
