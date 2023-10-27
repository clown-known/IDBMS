/*using BusinessObject.DTOs.Request;
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
    public class AdminController : ODataController
    {
        private readonly AdminService _service;
        public AdminController(AdminService service)
        {
            _service = service;
        }
        [EnableQuery]
        [HttpGet]
        public IActionResult GetAdmins()
        {
            return Ok(_service.GetAll());
        }
        [HttpPost]
        public IActionResult RegisterAdmin([FromBody] AdminRequest request)
        {
            try
            {
                _service.CreateAdmin(request);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);  
            }
            return Ok();
        }
        [HttpPut("{id}")]
        public IActionResult UpdateAdmin(Guid id, [FromBody] AdminRequest request)
        {
            try
            {
                _service.UpdateAdmin(id, request);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
        [HttpPut("{id}/isDeleted")]
        public IActionResult UpdateAdminStatus(Guid id, bool isDeleted)
        {
            try
            {
                _service.UpdateAdminStatus(id, isDeleted);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

    }
}
*/