using BusinessObject.DTOs.Request;
using IDBMS_API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Repository.Interfaces;

namespace IDBMS_API.Controllers.IDBMSControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ODataController
    {
        private readonly NotificationService _service;

        public NotificationController(NotificationService service)
        {
            _service = service;
        }

        [EnableQuery]
        [HttpGet]
        public IActionResult GetNotification()
        {
            return Ok(_service.GetAll());
        }
        [EnableQuery]
        [HttpGet("user/{userId}")]
        public IActionResult GetByUserId(Guid userId)
        {
            return Ok(_service.GetByUserId(userId));
        }
        [HttpPost]
        public IActionResult CreateNotificationForAllUsers([FromBody] NotificationRequest request)
        {
            try
            {
                _service.CreateNotificatonForAll(request);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
        [HttpPost("user/{userId}")]
        public IActionResult CreateNotificationForUser(Guid userId, [FromBody] NotificationRequest request)
        {
            try
            {
                _service.CreateNotificatonByUserId(request, userId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
        [HttpPost("users")]
        public IActionResult CreateNotificationForListUser([FromBody] NotificationRequest request)
        {
            try
            {
                _service.CreateNotificatonByListUserId(request, request.listUserId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
        [HttpPut("{id}/is-seen")]
        public IActionResult UpdateIsSeen(Guid id)
        {
            try
            {
                _service.UpdateIsSeenById(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpPut("is-seen/user/{id}")]
        public IActionResult UpdateIsSeenByUser(Guid userId)
        {
            try
            {
                _service.UpdateIsSeenByUserId(userId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
    }

}
