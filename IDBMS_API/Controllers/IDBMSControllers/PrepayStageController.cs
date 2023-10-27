using BusinessObject.DTOs.Request;
using IDBMS_API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace IDBMS_API.Controllers.IDBMSControllers
{
    public class PrepayStageController : ODataController
    {
        private readonly PrepayStageService _service;

        public PrepayStageController(PrepayStageService service)
        {
            _service = service;
        }

        [EnableQuery]
        [HttpGet]
        public IActionResult GetPrepayStages()
        {
            return Ok(_service.GetAll());
        }
        //all
        [EnableQuery]
        [HttpGet("project/{projectId}")]
        public IActionResult GetPrepayStagesByProjectId(Guid projectId)
        {
            return Ok(_service.GetByProjectId(projectId));
        }
        //permission
        [EnableQuery]
        [HttpGet("{id}")]
        public IActionResult GetPrepayStagesById(Guid id)
        {
            return Ok(_service.GetById(id));
        }
        [HttpPost]
        public IActionResult CreatePrepayStage([FromBody] PrepayStageRequest request)
        {
            try
            {
                _service.CreatePrepayStage(request);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePrepayStage(Guid id, [FromBody] PrepayStageRequest request)
        {
            try
            {
                _service.UpdatePrepayStage(id, request);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Ok();
        }

        [HttpPut("{id}/isHidden")]
        public IActionResult UpdatePrepayStageStatus(Guid id, bool isHidden)
        {
            try
            {
                _service.UpdatePrepayStageStatus(id, isHidden);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Ok();
        }
    }

}
