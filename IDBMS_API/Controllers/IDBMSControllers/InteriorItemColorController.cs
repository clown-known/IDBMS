/*using BusinessObject.DTOs.Request;
using IDBMS_API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace IDBMS_API.Controllers.IDBMSControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InteriorItemColorController : ODataController
    {
        private readonly InteriorItemColorService _service;

        public InteriorItemColorController(InteriorItemColorService service)
        {
            _service = service;
        }

        [EnableQuery]
        [HttpGet]
        public IActionResult GetInteriorItemColors()
        {
            return Ok(_service.GetAll());
        }

        [HttpPost]
        public IActionResult CreateInteriorItemColor([FromBody] InteriorItemColorRequest request)
        {
            try
            {
                _service.CreateInteriorItemColor(request);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateInteriorItemColor(int id, [FromBody] InteriorItemColorRequest request)
        {
            try
            {
                _service.UpdateInteriorItemColor(id, request);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult DeleteInteriorItemColor(int id)
        {
            try
            {
                _service.DeleteInteriorItemColor(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Ok();
        }
    }

}
*/