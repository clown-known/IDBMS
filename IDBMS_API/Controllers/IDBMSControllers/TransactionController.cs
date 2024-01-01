<<<<<<< HEAD
﻿using Microsoft.AspNetCore.Mvc;
=======
﻿using Azure.Core;
using IDBMS_API.DTOs.Request;
using IDBMS_API.DTOs.Response;
using BusinessObject.Enums;
using IDBMS_API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using BusinessObject.Models;
using IDBMS_API.Services.PaginationService;
using DocumentFormat.OpenXml.Drawing.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Threading.Tasks;
using API.Supporters.JwtAuthSupport;
>>>>>>> dev

namespace IDBMS_API.Controllers.IDBMSControllers
{
    public class TransactionController : Controller
    {
<<<<<<< HEAD
        public IActionResult Index()
        {
            return View();
=======
        private readonly TransactionService _service;
        private readonly PaginationService<Transaction> _paginationService;

        public TransactionsController(TransactionService service, PaginationService<Transaction> paginationService)
        {
            _service = service;
            _paginationService = paginationService;
        }

        [EnableQuery]
        [HttpGet]
        [Authorize(Policy = "Admin, Participation, ProjectManager")]
        public IActionResult GetTransactions(Guid projectId, string? payerName,TransactionType? type, TransactionStatus? status, int? pageSize, int? pageNo)
        {
            try
            {
                var list = _service.GetAll(payerName, type, status);

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
        //admin, owner
        [EnableQuery]
        [HttpGet("{id}")]
        [Authorize(Policy = "Admin, Participation, ProjectManager")]
        public IActionResult GetTransactionsById(Guid projectId, Guid id)
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
        //cus
        [EnableQuery]
        [HttpGet("user/{id}")]
        [Authorize(Policy = "Admin, Participation, ProjectManager")]
        public IActionResult GetTransactionsByUserId(Guid projectId, Guid id, string? payerName, TransactionType? type, TransactionStatus? status, int? pageSize, int? pageNo)
        {
            try
            {
                var list = _service.GetByUserId(id, payerName, type, status);

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
        //admin, owner
        [EnableQuery]
        [HttpGet("project/{id}")]
        [Authorize(Policy = "Admin, Participation, ProjectManager")]
        public IActionResult GetTransactionsByProjectId(Guid projectId, string? payerName, TransactionType? type, TransactionStatus? status, int? pageSize, int? pageNo)
        {
            try
            {
                var list = _service.GetByProjectId(projectId, payerName, type, status);

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
        [HttpPost]
        [Authorize(Policy = "Admin, Participation")]
        public async Task<IActionResult> CreateTransaction(Guid projectId, [FromForm][FromBody] TransactionRequest request)
        {
            try
            {
                var result = await _service.CreateTransaction(request);
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
        [Authorize(Policy = "Admin, Participation, ProjectManager")]
        public IActionResult UpdateTransaction(Guid projectId, Guid id, [FromForm][FromBody] TransactionRequest request)
        {
            try
            {
                _service.UpdateTransaction(id, request);
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

        [HttpPut("{id}/status")]
        [Authorize(Policy = "Admin, Participation, ProjectManager")]
        public IActionResult UpdateTransactionStatus(Guid projectId, Guid id, TransactionStatus status)
        {
            try
            {
                _service.UpdateTransactionStatus(id, status);
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
>>>>>>> dev
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "Admin, Participation, ProjectManager")]
        public IActionResult DeleteTransaction(Guid projectId, Guid id)
        {
            try
            {
                _service.DeleteTransactionById(id);
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
