using Azure.Core;
using BusinessObject.DTOs.Request;
using IDBMS_API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Repository.Interfaces;

namespace IDBMS_API.Controllers.IDBMSControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipationsController : ODataController
    {
        private readonly ParticipationService _service;

        public ParticipationsController(ParticipationService service)
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
        [HttpGet]
        [Route("user/{id}")]
        public IActionResult GetParticipationsByUserId(Guid id)
        {
            return Ok(_service.GetByUserId(id));
        }

        [EnableQuery]
        [HttpGet]
        [Route("project/{id}")]
        public IActionResult GetParticipationsByProjectId(Guid id)
        {
            return Ok(_service.GetByProjectId(id));
        }

        [HttpPost]
        public IActionResult CreateParticipation([FromBody] ParticipationRequest request)
        {
            try
            {
                var res = _service.CreateParticipation(request);
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
        public IActionResult UpdateParticipation(Guid id, [FromBody] ParticipationRequest request)
        {
            try
            {
                _service.UpdateParticipation(id, request);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteParticipation(Guid id)
        {
            try
            {
                _service.DeleteParticipation(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
    }
}
