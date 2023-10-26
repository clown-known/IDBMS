using BusinessObject.DTOs.Request;
using IDBMS_API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace IDBMS_API.Controllers.IDBMSControllers
{
    public class ConstructionTaskDesignController : ODataController
    {
        private readonly ConstructionTaskDesignService _service;

        public ConstructionTaskDesignController(ConstructionTaskDesignService service)
        {
            _service = service;
        }

        [EnableQuery]
        [HttpGet]
        public IActionResult GetConstructionTaskDesign()
        {
            return Ok(_service.GetAll());
        }

        [HttpPost]
        public IActionResult CreateConstructionTaskDesign([FromBody] ConstructionTaskDesignRequest request)
        {
            try
            {
                _service.CreateConstructionTaskDesign(request);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateConstructionTaskDesign(int id, [FromBody] ConstructionTaskDesignRequest request)
        {
            try
            {
                _service.UpdateConstructionTaskDesign(id, request);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteConstructionTaskDesign(int id)
        {
            try
            {
                _service.DeleteConstructionTaskDesign(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Ok();
        }
    }

}
