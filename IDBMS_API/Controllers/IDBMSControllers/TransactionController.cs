using Azure.Core;
using IDBMS_API.DTOs.Request;
using IDBMS_API.DTOs.Response;
using BusinessObject.Enums;
using IDBMS_API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using BusinessObject.Models;
using IDBMS_API.Services.PaginationService;

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
        public IActionResult GetTransactions(TransactionType? type, TransactionStatus? status, int? pageSize, int? pageNo)
        {
            var list = _service.GetAll(type, status); 

            return Ok(_paginationService.PaginateList(list, pageSize, pageNo));
        }
        //admin, owner
        [EnableQuery]
        [HttpGet("{id}")]
        public IActionResult GetTransactionsById(Guid id)
        {
            return Ok(_service.GetById(id));
        }
        //cus
        [EnableQuery]
        [HttpGet("user/{id}")]
        public IActionResult GetTransactionsByUserId(Guid id, TransactionType? type, TransactionStatus? status, int? pageSize, int? pageNo)
        {
            var list = _service.GetByUserId(id, type, status);

            return Ok(_paginationService.PaginateList(list, pageSize, pageNo));
        }
        //admin, owner
        [EnableQuery]
        [HttpGet("project/{id}")]
        public IActionResult GetTransactionsByProjectId(Guid id, TransactionType? type, TransactionStatus? status, int? pageSize, int? pageNo)
        {
            var list = _service.GetByProjectId(id, type, status);

            return Ok(_paginationService.PaginateList(list, pageSize, pageNo));
        }
        [HttpPost]
        public IActionResult CreateTransaction([FromBody][FromForm] TransactionRequest request)
        {
            try
            {
                var result = _service.CreateTransaction(request);
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
        public IActionResult UpdateTransaction(Guid id, [FromBody][FromForm] TransactionRequest request)
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
    }

}
