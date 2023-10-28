using BusinessObject.DTOs.Request;
using IDBMS_API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace IDBMS_API.Controllers.IDBMSControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrepayStageDesignsController : ODataController
    {
        private readonly PrepayStageDesignService _service;

        public PrepayStageDesignsController(PrepayStageDesignService service)
        {
            _service = service;
        }

        [EnableQuery]
        [HttpGet]
        public IActionResult GetPrepayStageDesigns()
        {
            return Ok(_service.GetAll());
        }

        [HttpPost]
        public IActionResult CreatePrepayStageDesign([FromBody] PrepayStageDesignRequest request)
        {
            try
            {
                _service.CreatePrepayStageDesign(request);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePrepayStageDesign(int id, [FromBody] PrepayStageDesignRequest request)
        {
            try
            {
                _service.UpdatePrepayStageDesign(id, request);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult UpdatePrepayStageDesignStatus(int id)
        {
            try
            {
                _service.DeletePrepayStageDesign(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Ok();
        }
    }

}
