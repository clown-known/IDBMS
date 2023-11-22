using Azure.Core;
using BusinessObject.DTOs.Request;
using BusinessObject.DTOs.Response;
using IDBMS_API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Repository.Interfaces;

namespace IDBMS_API.Controllers.IDBMSControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ODataController
    {
        private readonly NotificationService _service;

        public NotificationsController(NotificationService service)
        {
            _service = service;
        }

        [EnableQuery]
        [HttpGet]
        public IActionResult GetNotifications()
        {
            var response = new ResponseMessage()
            {
                Message = "Get successfully!",
                Data = _service.GetAll()
            };
            return Ok(_service.GetAll());
        }
        [EnableQuery]
        [HttpGet("user/{id}")]
        public IActionResult GetByUserId(Guid id)
        {
            var response = new ResponseMessage()
            {
                Message = "Get successfully!",
                Data = _service.GetByUserId(id)
            };
            return Ok(_service.GetByUserId(id));
        }
        [HttpPost]
        public IActionResult CreateNotificationsForAllUsers([FromBody] NotificationRequest request)
        {
            try
            {
                _service.CreateNotificatonsForAllUsers(request);

                var response = new ResponseMessage()
                {
                    Message = "Create successfully!"
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
        [HttpPost("user/{userId}")]
        public IActionResult CreateNotificationForUser(Guid userId, [FromBody] NotificationRequest request)
        {
            try
            {
                var result = _service.CreateNotificatonByUserId(request, userId);
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
        [HttpPost("users")]
        public IActionResult CreateNotificationForUsers([FromBody] NotificationRequest request)
        {
            try
            {
                _service.CreateNotificatonForUsers(request, request.listUserId);
                var response = new ResponseMessage()
                {
                    Message = "Create successfully!"
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
        [HttpPut("{id}/is-seen")]
        public IActionResult UpdateIsSeen(Guid id)
        {
            try
            {
                _service.UpdateIsSeenById(id);
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

        [HttpPut("is-seen/user/{id}")]
        public IActionResult UpdateIsSeenByUser(Guid userId)
        {
            try
            {
                _service.UpdateIsSeenByUserId(userId);
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
    }

}
