using API.Services;
using BusinessObject.DTOs.Request;
using BusinessObject.DTOs.Request.AccountRequest;
using BusinessObject.DTOs.Response;
using IDBMS_API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace IDBMS_API.Controllers.IDBMSControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ODataController
    {
        private readonly UserService _service;

        public UsersController(UserService service)
        {
            _service = service;
        }

        [EnableQuery]
        [HttpGet]
        public IActionResult GetUsers()
        {
            var response = new ResponseMessage()
            {
                Message = "Get successfully!",
                Data = _service.GetAll()
            };
            return Ok(_service.GetAll());
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] CreateUserRequest request)
        {
                try
                {
                    var result = _service.CreateUser(request);
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
        public IActionResult UpdateUser(string id, [FromBody] UpdateUserRequest request)
        {
            try
            {
                _service.UpdateUser(id, request);
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
