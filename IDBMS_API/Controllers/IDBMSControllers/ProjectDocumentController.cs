using API.Supporters.JwtAuthSupport;
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
using System.Threading.Tasks;

namespace IDBMS_API.Controllers.IDBMSControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectDocumentsController : ODataController
    {
        private readonly ProjectDocumentService _service;
        private readonly PaginationService<ProjectDocument> _paginationService;

        public ProjectDocumentsController(ProjectDocumentService service, PaginationService<ProjectDocument> paginationService)
        {
            _service = service;
            _paginationService = paginationService;
        }

        [EnableQuery]
        [HttpGet]
        [Authorize(Policy = "Admin, Participation, ProjectManager, Architect, ConstructionManager, Viewer")]
        public IActionResult GetProjectDocuments(ProjectDocumentCategory? category, string? name, int? pageSize, int? pageNo)
        {
            try
            {
                var list = _service.GetAll(category, name);

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
        [HttpGet("document-template/{id}")]
        [Authorize(Policy = "Admin, Participation, ProjectManager, Architect, ConstructionManager, Viewer")]
        public IActionResult GetProjectDocumentByProjectDocumentTemplateId(int id)
        {
            return Ok(_service.GetByFilter(null, id));
        }

        [EnableQuery]
        [HttpGet("project/{projectId}")]
        [Authorize(Policy = "Admin, Participation, ProjectManager, Architect, ConstructionManager, Viewer")]
        public IActionResult GetProjectDocumentsByProjectId(Guid projectId, ProjectDocumentCategory? category, string? name, int? pageSize, int? pageNo)
        {
            try
            {
                var list = _service.GetByProjectId(projectId, category, name);

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
        [Authorize(Policy = "Admin, Participation, ProjectManager, Architect, ConstructionManager, Viewer")]
        public IActionResult GetProjectDocumentById(Guid id)
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

        [HttpPost]
        [Authorize(Policy = "Admin, Participation, ProjectManager, Architect, ConstructionManager")]
        public async Task<IActionResult> CreateProjectDocument([FromForm][FromBody] ProjectDocumentRequest request)
        {
            try
            {
                var result = await _service.CreateProjectDocument(request);
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
        public IActionResult UpdateProjectDocument(Guid id, [FromForm][FromBody] ProjectDocumentRequest request)
        {
            try
            {
                _service.UpdateProjectDocument(id, request);
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
        [Authorize(Policy = "Admin, Participation, ProjectManager, Architect, ConstructionManager")]
        public IActionResult UpdateProjectDocumentStatus(Guid id)
        {
            try
            {
                _service.DeleteProjectDocument(id);
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
