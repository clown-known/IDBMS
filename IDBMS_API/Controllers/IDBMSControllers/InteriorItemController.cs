/*using BusinessObject.DTOs.Request;
using IDBMS_API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace IDBMS_API.Controllers.IDBMSControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InteriorItemController : ODataController
    {
        private readonly InteriorItemService _service;

        public InteriorItemController(InteriorItemService service)
        {
            _service = service;
        }

        [EnableQuery]
        [HttpGet]
        public IActionResult GetInteriorItems()
        {
            return Ok(_service.GetAll());
        }

        [HttpPost]
        public IActionResult CreateInteriorItem([FromBody] InteriorItemRequest request)
        {
            try
            {
                _service.CreateInteriorItem(request);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateInteriorItem(Guid id, [FromBody] InteriorItemRequest request)
        {
            try
            {
                _service.UpdateInteriorItem(id, request);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Ok();
        }

        [HttpPut("{id}/status")]
        public IActionResult UpdateInteriorItemStatus(Guid id, int status)
        {
            try
            {
                _service.UpdateInteriorItemStatus(id, status);
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