using API.Supporters.JwtAuthSupport;
using Azure;
using Azure.Core;
using BusinessObject.Enums;
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
using System.Threading.Tasks;

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
        [Authorize(Policy = "Admin, Participation, ProjectManager")]
        public IActionResult GetParticipations(Guid projectId, ParticipationRole? role, string? name, int? pageSize, int? pageNo, ProjectStatus? projectStatus)
        {
            try
            {
                var list = _service.GetAll(role, name, projectStatus);

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
        [HttpGet("user/{id}")]
        [Authorize(Policy = "Admin, Participation, ProjectManager")]
        public IActionResult GetParticipationsByUserId(Guid projectId, Guid id, ParticipationRole? role, string? name, int? pageSize, int? pageNo, ProjectStatus? projectStatus)
        {
            try
            {
                var list = _service.GetByUserId(id, role, name, projectStatus);

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
        [HttpGet("project/{projectId}")]
        [Authorize(Policy = "Admin, Participation, ProjectManager")]
        public IActionResult GetParticipationsByProjectId(Guid projectId, ParticipationRole? role, string? name, int? pageSize, int? pageNo)
        {
            try
            {
                var list = _service.GetProjectMemberByProjectId(projectId, role, name);

                var response = new ResponseMessage()
                {
                    Message = "Get successfully!",
                    Data = new
                    {
                        PaginatedList = _paginationService.PaginateList(list, pageSize, pageNo),
                        ProductOwner = _service.GetProjectOwnerByProjectId(projectId),
                        ProjectManager = _service.GetProjectManagerByProjectId(projectId),
                    }
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
        [HttpGet("project/{projectId}/users")]
        [Authorize(Policy = "Admin, Participation, ProjectManager")]
        public IActionResult GetUsersByParticipationInProject(Guid projectId)
        {
            try
            {
                var list = _service.GetUsersByParticipationInProject(projectId);

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
        [Authorize(Policy = "Admin, Participation, ProjectManager")]
        public IActionResult CreateParticipation(Guid projectId, [FromBody] ProjectParticipationRequest request)
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
        [Authorize(Policy = "Admin, Participation, ProjectManager")]
        public IActionResult CreateParticipationsByRole(Guid projectId, [FromBody] CreateParticipationListRequest request)
        {
            try
            {
                var result = _service.CreateParticipationsByRole(request);
                var response = new ResponseMessage()
                {
                    Message = "Create successfully!",
                    Data = result,
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
        public IActionResult UpdateParticipation(Guid projectId, Guid id, [FromBody] ProjectParticipationRequest request)
        {
            try
            {
                _service.UpdateParticipation(id, request);
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
        [Authorize(Policy = "Admin, Participation, ProjectManager")]
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
