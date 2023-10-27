using BusinessObject.DTOs.Request;
using IDBMS_API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace IDBMS_API.Controllers.IDBMSControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DecorProjectDesignController : ODataController
    {
        private readonly DecorProjectDesignService _service;

        public DecorProjectDesignController(DecorProjectDesignService service)
        {
            _service = service;
        }

        [EnableQuery]
        [HttpGet]
        public IActionResult GetDecorProjectDesign()
        {
            return Ok(_service.GetAll());
        }

        [HttpPost]
        public IActionResult CreateDecorProjectDesign([FromBody] DecorProjectDesignRequest request)
        {
            try
            {
                _service.CreateDecorProjectDesign(request);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateDecorProjectDesign(int id, [FromBody] DecorProjectDesignRequest request)
        {
            try
            {
                _service.UpdateDecorProjectDesign(id, request);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Ok();
        }

        [HttpPut("{id}/isDeleted")]
        public IActionResult UpdateDecorProjectDesignStatus(int id, bool isDeleted)
        {
            try
            {
                _service.UpdateDecorProjectDesignStatus(id, isDeleted);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Ok();
        }
    }

}
