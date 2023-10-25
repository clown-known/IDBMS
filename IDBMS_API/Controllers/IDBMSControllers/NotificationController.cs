using BusinessObject.DTOs.Request.CreateRequests;
using IDBMS_API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Repository.Interfaces;

namespace IDBMS_API.Controllers.IDBMSControllers
{
    public class NotificationController : ODataController
    {
        private readonly NotificationService _service;
        private readonly INotificationRepository _repository;

        public NotificationController(NotificationService service, INotificationRepository repository)
        {
            _service = service;
            _repository = repository;
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
        public IActionResult CreateNotificationForListUser(List<Guid> listUserId, [FromBody] NotificationRequest request)
        {
            try
            {
                _service.CreateNotificatonByListUserId(request, listUserId);
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
                var notification = _service.GetById(id);
                if (notification == null) return NotFound();
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
                var notification = _service.GetByUserId(userId);
                if (notification == null) return NotFound();
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
