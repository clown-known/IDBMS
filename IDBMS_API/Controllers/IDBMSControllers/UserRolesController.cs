using BusinessObject.DTOs.Request;
using BusinessObject.DTOs.Response;
using IDBMS_API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace IDBMS_API.Controllers.IDBMSControllers
{
    public class UserRolesController : Controller
    {
        private readonly UserRolesService _service;

        public UserRolesController(UserRolesService service)
        {
            _service = service;
        }
        //admin, owner
        [EnableQuery]
        [HttpGet("{id}")]
        public IActionResult GetUserRoleById(int id)
        {
            return Ok(_service.GetById(id));
        }
        //cus
        [EnableQuery]
        [HttpGet("user/{id}")]
        public IActionResult GetUserRolesByUserId(Guid id)
        {
            return Ok(_service.GetByUserId(id));
        }
        //admin, owner
        [HttpPost]
        public IActionResult AddUserRoles([FromBody] UserRoleRequest request)
        {
            try
            {
                var result = _service.AddRoleToUser(request);
                var response = new ResponseMessage()
                {
                    Message = "Add successfully!",
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
        public IActionResult UpdateUserRole(int id, [FromBody] UserRoleRequest request)
        {
            try
            {
                _service.UpdateUserRoles(id, request);
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
