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

namespace IDBMS_API.Controllers.IDBMSControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ODataController
    {
        private readonly TransactionService _service;
        private readonly PaginationService<Transaction> _paginationService;

        public TransactionsController(TransactionService service, PaginationService<Transaction> paginationService)
        {
            _service = service;
            _paginationService = paginationService;
        }

        [EnableQuery]
        [HttpGet]
        [Authorize(Policy = "")]
        public IActionResult GetTransactions(string? payerName,TransactionType? type, TransactionStatus? status, int? pageSize, int? pageNo)
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
        [Authorize(Policy = "Owner, ProjectManager")]
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
        [Authorize(Policy = "User")]
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
        [Authorize(Policy = "Owner, ProjectManager")]
        public IActionResult GetTransactionsByProjectId(Guid projectId, Guid id, string? payerName, TransactionType? type, TransactionStatus? status, int? pageSize, int? pageNo)
        {
            try
            {
                var list = _service.GetByProjectId(id, payerName, type, status);

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
        [Authorize(Policy = "")]
        public async Task<IActionResult> CreateTransaction([FromForm][FromBody] TransactionRequest request)
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
        [Authorize(Policy = "")]
        public IActionResult UpdateTransaction(Guid id, [FromForm][FromBody] TransactionRequest request)
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
        [Authorize(Policy = "")]
        public IActionResult UpdateTransactionStatus(Guid id, TransactionStatus status)
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
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "")]
        public IActionResult DeleteTransaction(Guid id)
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
