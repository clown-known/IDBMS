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
    public class PrepayStagesController : ODataController
    {
        private readonly PrepayStageService _service;

        public PrepayStagesController(PrepayStageService service)
        {
            _service = service;
        }

        [EnableQuery]
        [HttpGet]
        public IActionResult GetPrepayStages()
        {
            var response = new ResponseMessage()
            {
                Message = "Get successfully!",
                Data = _service.GetAll()
            };
            return Ok(response);
        }
        //all
        [EnableQuery]
        [HttpGet("project/{projectId}")]
        public IActionResult GetPrepayStagesByProjectId(Guid projectId)
        {
            var response = new ResponseMessage()
            {
                Message = "Get successfully!",
                Data = _service.GetByProjectId(projectId)
            };
            return Ok(response);
        }
        //permission
        [EnableQuery]
        [HttpGet("{id}")]
        public IActionResult GetPrepayStagesById(Guid id)
        {
            var response = new ResponseMessage()
            {
                Message = "Get successfully!",
                Data = _service.GetById(id)
            };
            return Ok(response);
        }
        [HttpPost]
        public IActionResult CreatePrepayStage([FromBody] PrepayStageRequest request)
        {
            try
            {
                var result = _service.CreatePrepayStage(request);
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
        public IActionResult UpdatePrepayStage(Guid id, [FromBody] PrepayStageRequest request)
        {
            try
            {
                _service.UpdatePrepayStage(id, request);
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

        [HttpPut("{id}/isHidden")]
        public IActionResult UpdatePrepayStageStatus(Guid id, bool isHidden)
        {
            try
            {
                _service.UpdatePrepayStageStatus(id, isHidden);
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
