using IDBMS_API.DTOs.Request;
using IDBMS_API.DTOs.Response;
using BusinessObject.Enums;
using IDBMS_API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using BusinessObject.Models;
using DocumentFormat.OpenXml.Wordprocessing;
using IDBMS_API.Services.PaginationService;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using DocumentFormat.OpenXml.Drawing.Spreadsheet;
using DocumentFormat.OpenXml.EMMA;
using API.Supporters.JwtAuthSupport;

namespace IDBMS_API.Controllers.IDBMSControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectTasksController : ODataController
    {
        private readonly ProjectTaskService _service;
        private readonly PaginationService<ProjectTask> _paginationService;

        public ProjectTasksController(ProjectTaskService service, PaginationService<ProjectTask> paginationService)
        {
            _service = service;
            _paginationService = paginationService;
        }

        [EnableQuery]
        [HttpGet]
        [Authorize(Policy = "Admin, Participation, ProjectManager, Architect, ConstructionManager")]
        public IActionResult GetProjectTasks(Guid projectId)
        {
            return Ok(_service.GetAll());
        }        
        
        [EnableQuery]
        [HttpGet("{id}")]
        [Authorize(Policy = "Admin, Participation, ProjectManager, Architect, ConstructionManager")]
        public IActionResult GetProjectTaskById(Guid projectId, Guid id)
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
        [HttpGet("project/{projectId}")]
        [Authorize(Policy = "Admin, Participation, ProjectManager, Architect, ConstructionManager")]
        public IActionResult GetProjectTasksByProjectId(Guid projectId, int? pageSize, int? pageNo, 
                        string? codeOrName, Guid? stageId, ProjectTaskStatus? taskStatus, int? taskCategoryId, Guid? roomId, 
                        bool includeRoomIdFilter, bool includeStageIdFilter, Guid? participationId)
        {
            try
            {
                var list = _service.GetByProjectId(projectId, codeOrName, stageId, taskStatus, taskCategoryId, roomId, 
                    includeRoomIdFilter, includeStageIdFilter, participationId);

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
        [HttpGet("ids")]
        [Authorize(Policy = "Admin, Participation, ProjectManager, Architect, ConstructionManager")]
        public IActionResult GetAllProjectTaskIdByFilter(Guid projectId, int? pageSize, int? pageNo,
                string? codeOrName, Guid? stageId, ProjectTaskStatus? taskStatus, int? taskCategoryId, Guid? roomId, 
                bool includeRoomIdFilter, bool includeStageIdFilter, Guid? participationId)
        {
            try
            {
                var list = _service.GetAllProjectTaskIdByFilter(projectId, codeOrName, stageId, taskStatus, taskCategoryId, roomId, 
                    includeRoomIdFilter, includeStageIdFilter, participationId);

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
        [Authorize(Policy = "Admin, Participation, ProjectManager, Architect, ConstructionManager")]
        public IActionResult CreateProjectTask(Guid projectId, [FromBody] ProjectTaskRequest request)
        {
            try
            {
                var result = _service.CreateProjectTask(request);
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
        [Authorize(Policy = "Admin, Participation, ProjectManager, Architect, ConstructionManager")]
        public IActionResult UpdateProjectTask(Guid projectId, Guid id, [FromBody] ProjectTaskRequest request)
        {
            try
            {
                _service.UpdateProjectTask(id, request);
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

        [HttpPut("payment-stage/{id}")]
        [Authorize(Policy = "Admin, Participation, ProjectManager, Architect, ConstructionManager")]
        public IActionResult AssignTasksToStage(Guid id,[FromBody] List<Guid> listTaskId, Guid projectId)
        {
            try
            {
                _service.AssignTasksToStage(id, listTaskId, projectId);
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
        [Authorize(Policy = "Admin, Participation, ProjectManager, Architect, ConstructionManager")]
        public IActionResult UpdateProjectTaskStatus(Guid projectId, Guid id, ProjectTaskStatus status)
        {
            try
            {
                _service.UpdateProjectTaskStatus(id, status);
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
