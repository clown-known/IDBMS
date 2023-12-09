using Azure.Core;
using BusinessObject.Enums;
using BusinessObject.Models;
using IDBMS_API.DTOs.Request;
using IDBMS_API.DTOs.Response;
using IDBMS_API.Services;
using IDBMS_API.Services.PaginationService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

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
        public IActionResult GetProjectDocuments(ProjectDocumentCategory? category, string? name, int? pageSize, int? pageNo)
        {
            var list = _service.GetAll(category, name);

            return Ok(_paginationService.PaginateList(list, pageSize, pageNo));
        }
        [EnableQuery]
        [HttpGet("document-template/{id}")]
        public IActionResult GetProjectDocumentByProjectDocumentTemplateId(int id)
        {
            return Ok(_service.GetByFilter(null, id));
        }

        [EnableQuery]
        [HttpGet("project/{id}")]
        public IActionResult GetProjectDocumentsByProjectId(Guid id)
        {
            return Ok(_service.GetByProjectId(id));
        }

        [HttpPost]
        public IActionResult CreateProjectDocument([FromBody][FromForm] ProjectDocumentRequest request)
        {
            try
            {
                var result = _service.CreateProjectDocument(request);
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
        public IActionResult UpdateProjectDocument(Guid id, [FromBody][FromForm] ProjectDocumentRequest request)
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
