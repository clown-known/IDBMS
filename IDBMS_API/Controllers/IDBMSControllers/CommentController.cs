using Azure.Core;
using BusinessObject.DTOs.Request;
using BusinessObject.DTOs.Response;
using BusinessObject.Enums;
using IDBMS_API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace IDBMS_API.Controllers.IDBMSControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ODataController
    {
        private readonly CommentService _service;

        public CommentsController(CommentService service)
        {
            _service = service;
        }

        [EnableQuery]
        [HttpGet]
        public IActionResult GetComments()
        {
            return Ok(_service.GetAll());
        }
        //permission
        [EnableQuery]
        [HttpGet("project-task/{id}")]
        public IActionResult GetCommentsProjectTaskId(Guid id)
        {
            return Ok(_service.GetByProjectTaskId(id));
        }
        //permission
        [EnableQuery]
        [HttpGet("project/{id}")]
        public IActionResult GetCommentsProjectId(Guid id)
        {
            return Ok(_service.GetByProjectId(id));
        }
        [HttpPost]
        public IActionResult CreateComment([FromBody] CommentRequest request)
        {
            try
            {
                var result = _service.CreateComment(request);
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

        [HttpPut("{id}")]
        public IActionResult UpdateComment(Guid id, [FromBody] CommentRequest request)
        {
            try
            {
                _service.UpdateComment(id, request);
                var response = new ResponseMessage()
                {
                    Message = "Update successfully!",
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

        [HttpPut("{id}/status")]
        public IActionResult UpdateCommentStatus(Guid id, CommentStatus status)
        {
            try
            {
                _service.UpdateCommentStatus(id, status);
                var response = new ResponseMessage()
                {
                    Message = "Update successfully!",
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
        public IActionResult DeleteComment(Guid id)
        {
            try
            {
                _service.DeleteComment(id);
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
