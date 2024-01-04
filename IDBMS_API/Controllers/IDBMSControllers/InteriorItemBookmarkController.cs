using Azure.Core;
using IDBMS_API.DTOs.Request;
using IDBMS_API.DTOs.Response;
using BusinessObject.Models;
using IDBMS_API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Repository.Interfaces;
using API.Supporters.JwtAuthSupport;

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
        [Authorize(Policy = "User")]
        public IActionResult GetInteriorItemBookmarks()
        {
            return Ok(_service.GetAll());
        }

        [EnableQuery]
        [HttpGet("user/{id}")]
        [Authorize(Policy = "User")]
        public IActionResult GetInteriorItemBookmarksByUserId(Guid id)
        { 
            return Ok(_service.GetByUserId(id));
        }

        [HttpPost]
        [Authorize(Policy = "User")]
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
        [Authorize(Policy = "User")]
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
