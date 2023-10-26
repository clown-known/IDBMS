using BusinessObject.DTOs.Request;
using IDBMS_API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace IDBMS_API.Controllers.IDBMSControllers
{
    public class CommentController : ODataController
    {
        private readonly CommentService _service;

        public CommentController(CommentService service)
        {
            _service = service;
        }

        [EnableQuery]
        [HttpGet]
        public IActionResult GetComment()
        {
            return Ok(_service.GetAll());
        }

        [HttpPost]
        public IActionResult CreateComment([FromBody] CommentRequest request)
        {
            try
            {
                _service.CreateComment(request);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateComment(Guid id, [FromBody] CommentRequest request)
        {
            try
            {
                _service.UpdateComment(id, request);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Ok();
        }

        [HttpPut("{id}/status")]
        public IActionResult UpdateCommentStatus(Guid id, int status)
        {
            try
            {
                _service.UpdateCommentStatus(id, status);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Ok();
        }
    }

}
