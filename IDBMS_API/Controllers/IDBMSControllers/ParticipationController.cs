using Azure.Core;
using BusinessObject.DTOs.Request;
using IDBMS_API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Repository.Interfaces;

namespace IDBMS_API.Controllers.IDBMSControllers
{
    public class ParticipationController : ODataController
    {
        private readonly ParticipationService _service;

        public ParticipationController(ParticipationService service)
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
        public IActionResult GetParticipationsByUserId(Guid id)
        {
            return Ok(_service.GetByUserId(id));
        }

        [EnableQuery]
        [HttpGet("project/{id}")]
        public IActionResult GetParticipationsByProjectId(Guid id)
        {
            return Ok(_service.GetByProjectId(id));
        }

        [HttpPost]
        public IActionResult CreateParticipation([FromBody] ParticipationRequest request)
        {
            try
            {
                _service.CreateParticipation(request);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateParticipation(Guid id, [FromBody] ParticipationRequest request)
        {
            try
            {
                _service.UpdateParticipation(id, request);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteParticipation(Guid id)
        {
            try
            {
                _service.DeleteParticipation(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Ok();
        }
    }
}
