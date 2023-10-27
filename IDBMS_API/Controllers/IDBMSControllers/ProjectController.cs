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
                _service.CreateProject(request);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProject(Guid id, [FromBody] ProjectRequest request)
        {
            try
            {
                _service.UpdateProject(id, request);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Ok();
        }

        [HttpPut("{id}/status")]
        public IActionResult DeleteProject(Guid id, ProjectStatus status)
        {
            try
            {
                _service.UpdateProjectStatus(id, status);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Ok();
        }
    }
}
