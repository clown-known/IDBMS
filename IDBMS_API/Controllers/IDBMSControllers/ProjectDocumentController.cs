using BusinessObject.DTOs.Request;
using IDBMS_API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace IDBMS_API.Controllers.IDBMSControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectDocumentController : ODataController
    {
        private readonly ProjectDocumentService _service;

        public ProjectDocumentController(ProjectDocumentService service)
        {
            _service = service;
        }

        [EnableQuery]
        [HttpGet]
        public IActionResult GetProjectDocuments()
        {
            return Ok(_service.GetAll());
        }

        [HttpPost]
        public IActionResult CreateProjectDocument([FromBody] ProjectDocumentRequest request)
        {
            try
            {
                _service.CreateProjectDocument(request);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProjectDocument(Guid id, [FromBody] ProjectDocumentRequest request)
        {
            try
            {
                _service.UpdateProjectDocument(id, request);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Ok();
        }

        [HttpPut("{id}/isDeleted")]
        public IActionResult UpdateProjectDocumentStatus(Guid id, bool isDeleted)
        {
            try
            {
                _service.UpdateProjectDocumentStatus(id, isDeleted);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Ok();
        }
    }

}
