using Azure;
using Azure.Core;
using IDBMS_API.DTOs.Request;
using IDBMS_API.DTOs.Response;
using IDBMS_API.Services;
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

        public ProjectParticipationsController(ProjectParticipationService service)
        {
            _service = service;
        }

        [EnableQuery]
        [HttpGet]
        public IActionResult GetParticipations()
        {
            return Ok(_service.GetAll());
        }

        [EnableQuery]
        [HttpGet("user/{id}")]
        public IActionResult GetParticipationsByUserId(Guid projectId, Guid id)
        {
            return Ok(_service.GetByUserId(id));
        }

        [EnableQuery]
        [HttpGet("project/{id}")]
        public IActionResult GetParticipationsByProjectId(Guid projectId, Guid id)
        {
            return Ok(_service.GetByProjectId(id));
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
