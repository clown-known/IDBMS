using Azure.Core;
using BusinessObject.DTOs.Request;
using BusinessObject.DTOs.Response;
using IDBMS_API.Services;
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

        public ProjectDocumentsController(ProjectDocumentService service)
        {
            _service = service;
        }

        [EnableQuery]
        [HttpGet]
        public IActionResult GetProjectDocuments()
        {
            var response = new ResponseMessage()
            {
                Message = "Get successfully!",
                Data = _service.GetAll()
            };
            return Ok(_service.GetAll());
        }
        [EnableQuery]
        [HttpGet("document-template/{id}")]
        public IActionResult GetProjectDocumentByProjectDocumentTemplateId(int id)
        {
            var response = new ResponseMessage()
            {
                Message = "Get successfully!",
                Data = _service.GetByFilter(null, id)
            };
            return Ok(_service.GetByFilter(null, id));
        }
        [HttpPost]
        public IActionResult CreateProjectDocument([FromBody] ProjectDocumentRequest request)
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
        public IActionResult UpdateProjectDocument(Guid id, [FromBody] ProjectDocumentRequest request)
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
