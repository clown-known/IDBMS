using BusinessObject.DTOs.Request.CreateRequests;
using BusinessObject.Models;
using IDBMS_API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Repository.Interfaces;

namespace IDBMS_API.Controllers.IDBMSControllers
{
    public class AdminController : ODataController
    {
        private readonly AdminService _service;
        private readonly IAdminRepository _repository;
        public AdminController(AdminService service, IAdminRepository repository)
        {
            _service = service;
            _repository = repository;
        }
        [EnableQuery]
        [HttpGet]
        public IActionResult GetAdmin()
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
            var admin = _service.GetById(id);
            if (admin == null) return NotFound();
            _service.UpdateAdmin(request);
            return Ok();
        }
        [HttpPut("{id}/isDeleted")]
        public IActionResult UpdateAdminStatus(Guid id, bool isDeleted)
        {
            var admin = _service.GetById(id);
            if (admin == null) return NotFound();
            admin.IsDeleted = isDeleted;
            _repository.Update(admin);  
            return Ok();
        }

    }
}
