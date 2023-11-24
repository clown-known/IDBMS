using Azure.Core;
using BusinessObject.DTOs.Request;
using BusinessObject.DTOs.Response;
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
    public class InteriorItemBookmarksController : ODataController
    {
        private readonly InteriorItemBookmarkService _service;

        public InteriorItemBookmarksController(InteriorItemBookmarkService service)
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
                var result = _service.CreateInteriorItemBookmark(request);
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

        [HttpDelete("{id}")]
        public IActionResult DeleteInteriorItemBookmark(Guid id)
        {
            try
            {
                _service.DeleteInteriorItemBookmark(id);
                var response = new ResponseMessage()
                {
                    Message = "Delete successfully!",
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
