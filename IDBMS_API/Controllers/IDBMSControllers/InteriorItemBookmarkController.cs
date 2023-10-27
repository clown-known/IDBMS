/*using Azure.Core;
using BusinessObject.DTOs.Request;
using BusinessObject.Models;
using IDBMS_API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Repository.Interfaces;

namespace IDBMS_API.Controllers.IDBMSControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InteriorItemBookmarkController : ODataController
    {
        private readonly InteriorItemBookmarkService _service;

        public InteriorItemBookmarkController(InteriorItemBookmarkService service)
        {
            _service = service;
        }

        [EnableQuery]
        [HttpGet]
        public IActionResult GetInteriorItemBookmarks()
        {
            return Ok(_service.GetAll());
        }

        [EnableQuery]
        [HttpGet("user/{id}")]
        public IActionResult GetInteriorItemBookmarksByUserId(Guid id)
        {
            return Ok(_service.GetByUserId(id));
        }

        [HttpPost]
        public IActionResult CreateInteriorItemBookmark([FromBody] InteriorItemBookmarkRequest request)
        {
            try
            {
                var res = _service.CreateInteriorItemBookmark(request);
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

        [HttpDelete("{id}")]
        public IActionResult DeleteInteriorItemBookmark(Guid id)
        {
            try
            {
                _service.DeleteInteriorItemBookmark(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
    }
}
*/