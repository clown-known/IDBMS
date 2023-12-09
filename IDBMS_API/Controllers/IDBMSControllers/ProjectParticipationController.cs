using Azure;
using Azure.Core;
using BusinessObject.Enums;
using BusinessObject.Models;
using DocumentFormat.OpenXml.Wordprocessing;
using IDBMS_API.DTOs.Request;
using IDBMS_API.DTOs.Response;
using IDBMS_API.Services;
using IDBMS_API.Services.PaginationService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Repository.Interfaces;

namespace IDBMS_API.Controllers.IDBMSControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectParticipationsController : ODataController
    {
        private readonly ProjectParticipationService _service;
        private readonly PaginationService<ProjectParticipation> _paginationService;

        public ProjectParticipationsController(ProjectParticipationService service, PaginationService<ProjectParticipation> paginationService)
        {
            _service = service;
            _paginationService = paginationService;
        }

        [EnableQuery]
        [HttpGet]
        public IActionResult GetParticipations(ParticipationRole? role, string? username, int? pageSize, int? pageNo)
        {
            var list = _service.GetAll(role, username);

            return Ok(_paginationService.PaginateList(list, pageSize, pageNo));
        }

        [EnableQuery]
        [HttpGet("user/{id}")]
        public IActionResult GetParticipationsByUserId(Guid id, ParticipationRole? role, string? username, int? pageSize, int? pageNo)
        {
            var list = _service.GetByUserId(id, role, username) ?? throw new Exception("This object is not existed!");

            return Ok(_paginationService.PaginateList(list, pageSize, pageNo));
        }

        [EnableQuery]
        [HttpGet("project/{id}")]
        public IActionResult GetParticipationsByProjectId(Guid id, ParticipationRole? role, string? username, int? pageSize, int? pageNo)
        {
            var list = _service.GetByProjectId(id, role, username) ?? throw new Exception("This object is not existed!");

            return Ok(_paginationService.PaginateList(list, pageSize, pageNo));
        }

        [HttpPost]
        public IActionResult CreateParticipation([FromBody] ProjectParticipationRequest request)
        {
            try
            {
                var result = _service.CreateParticipation(request);
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

        [HttpPost("employees")]
        public IActionResult CreateParticipationsByRole([FromBody] CreateParticipationListRequest request)
        {
            try
            {
                _service.CreateParticipationsByRole(request);
                var response = new ResponseMessage()
                {
                    Message = "Create successfully!",
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
        public IActionResult UpdateParticipation(Guid projectId, [FromBody] ProjectParticipationRequest request)
        {
            try
            {
                _service.UpdateParticipation(request);
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
        public IActionResult DeleteParticipation(Guid projectId, Guid id)
        {
            try
            {
                _service.DeleteParticipation(id);
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
