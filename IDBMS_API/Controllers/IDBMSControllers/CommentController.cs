using Azure.Core;
using IDBMS_API.DTOs.Request;
using IDBMS_API.DTOs.Response;
using BusinessObject.Enums;
using IDBMS_API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using BusinessObject.Models;
using IDBMS_API.Services.PaginationService;
using API.Supporters.JwtAuthSupport;

namespace IDBMS_API.Controllers.IDBMSControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ODataController
    {
        private readonly CommentService _service;
        private readonly PaginationService<Comment> _paginationService;

        public CommentsController(CommentService service, PaginationService<Comment> paginationService)
        {
            _service = service;
            _paginationService = paginationService;

        }

        //permission
        [EnableQuery]
        [HttpGet("project-task/{id}")]
        public IActionResult GetCommentsProjectTaskId(Guid projectId, Guid id, CommentStatus? status, string? content, int? pageSize, int? pageNo)
        {
            var list = _service.GetByProjectTaskId(id, status, content);
            try
            {
                var response = new ResponseMessage()
                {
                    Message = "Get successfully!",
                    Data = _paginationService.PaginateList(list, pageSize, pageNo)
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
        //permission
        [EnableQuery]
        [HttpGet("project/{id}")]
        [Authorize(Policy = "Participation")]
        public IActionResult GetCommentsProjectId(Guid projectId, Guid id, CommentStatus? status, string? content, int? pageSize, int? pageNo)
        {
            var list = _service.GetByProjectId(id, status, content);
            try
            {
                var response = new ResponseMessage()
                {
                    Message = "Get successfully!",
                    Data = _paginationService.PaginateList(list, pageSize, pageNo)
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

        [EnableQuery]
        [HttpGet("{id}")]
        [Authorize(Policy = "Participation")]
        public IActionResult GetCommentById(Guid id, Guid projectId)
        {
            try
            {
                var response = new ResponseMessage()
                {
                    Message = "Get successfully!",
                    Data = _service.GetById(id),
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

        [HttpPost]
        [Authorize(Policy = "Participation")]
        public async Task<IActionResult> CreateComment(Guid projectId, [FromForm] CommentRequest request)
        {
            try
            {
                var result = await _service.CreateComment(request);
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
        [Authorize(Policy = "Participation")]
        public IActionResult UpdateComment(Guid projectId, Guid id, [FromForm] CommentRequest request)
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
        [Authorize(Policy = "ProjectManager, Architect, ConstructionManager, Owner")]
        public IActionResult UpdateCommentStatus(Guid projectId, Guid id, CommentStatus status)
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
        [Authorize(Policy = "ProjectManager, Architect, ConstructionManager, Owner")]
        public IActionResult DeleteComment(Guid projectId, Guid id)
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
