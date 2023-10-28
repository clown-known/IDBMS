using Azure.Core;
using BusinessObject.DTOs.Request;
using BusinessObject.DTOs.Response;
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
            var response = new ResponseMessage()
            {
                Message = "Get successfully!",
                Data = _service.GetAll()
            };
            return Ok(response);
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
        [HttpPut("{id}/isDeleted")]
        public IActionResult UpdateAdminStatus(Guid id, bool isDeleted)
        {
            try
            {
                _service.UpdateAdminStatus(id, isDeleted);
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
