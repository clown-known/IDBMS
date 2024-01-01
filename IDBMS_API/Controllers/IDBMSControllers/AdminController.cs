<<<<<<< HEAD
﻿using Microsoft.AspNetCore.Mvc;
=======
﻿using Azure.Core;
using IDBMS_API.DTOs.Request;
using IDBMS_API.DTOs.Response;
using BusinessObject.Models;
using IDBMS_API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Repository.Interfaces;
using DocumentFormat.OpenXml.Office2016.Excel;
using DocumentFormat.OpenXml.Wordprocessing;
using IDBMS_API.Services.PaginationService;
using BusinessObject.Enums;
using API.Supporters.JwtAuthSupport;
>>>>>>> dev

namespace IDBMS_API.Controllers.IDBMSControllers
{
    public class AdminController : Controller
    {
<<<<<<< HEAD
        public IActionResult Index()
        {
            return View();
        }
=======
        private readonly AdminService _service;
        private readonly PaginationService<Admin> _paginationService;

        public AdminsController(AdminService service, PaginationService<Admin> paginationService)
        {
            _service = service;
            _paginationService = paginationService;
        }

        [EnableQuery]
        [HttpGet]
        [Authorize(Policy = "Admin")]
        public IActionResult GetAdmins(string? searchValue, AdminStatus? status, int? pageSize, int? pageNo)
        {
            try
            {
                var list = _service.GetAll(searchValue, status);

                var response = new ResponseMessage()
                {
                    Message = "Get successfully!",
                    Data = _paginationService.PaginateList(list, pageSize, pageNo)
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

        [EnableQuery]
        [HttpGet("{id}")]
        [Authorize(Policy = "Admin")]
        public IActionResult GetAdminById(Guid id)
        {
            try
            {
                var response = new ResponseMessage()
                {
                    Message = "Get successfully!",
                    Data = _service.GetById(id),
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

        [EnableQuery]
        [HttpGet("username")]
        [Authorize(Policy = "Admin")]
        public IActionResult CheckUsernameExist(string username)
        {
            try
            {
                var result = _service.CheckByUsername(username);
                var response = new ResponseMessage()
                {
                    Message = "Get successfully!",
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

        [HttpPost]
        [Authorize(Policy = "Admin")]
        public IActionResult CreateAdmin([FromBody] AdminRequest request)
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
        [Authorize(Policy = "Admin")]
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
        [Authorize(Policy = "Admin")]
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

>>>>>>> dev
    }
}
