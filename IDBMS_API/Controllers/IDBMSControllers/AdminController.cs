using Azure.Core;
using IDBMS_API.DTOs.Request;
using IDBMS_API.DTOs.Response;
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
    public class AdminsController : ODataController
    {
        private readonly AdminService _service;
        public AdminsController(AdminService service)
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
                var result = _service.CreateAdmin(request);
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
        public IActionResult UpdateAdmin(Guid id, [FromBody] AdminRequest request)
        {
            try
            {
                _service.UpdateAdmin(id, request);
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
        public IActionResult DeleteAdmin(Guid id)
        {
            try
            {
                _service.DeleteAdmin(id);
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
