using Azure.Core;
using BusinessObject.DTOs.Request;
using IDBMS_API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Repository.Interfaces;

namespace IDBMS_API.Controllers.IDBMSControllers
{
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
                _service.CreateInteriorItemBookmark(request);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteInteriorItemBookmark(Guid id)
        {
            try
            {
                _service.DeleteInteriorItemBookmark(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Ok();
        }
    }
}
