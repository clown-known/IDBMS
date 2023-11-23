using Azure.Core;
using BusinessObject.DTOs.Request;
using BusinessObject.DTOs.Response;
using BusinessObject.Enums;
using IDBMS_API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace IDBMS_API.Controllers.IDBMSControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ODataController
    {
        private readonly TransactionService _service;

        public TransactionsController(TransactionService service)
        {
            _service = service;
        }

        [EnableQuery]
        [HttpGet]
        public IActionResult GetTransactions()
        {
            return Ok(_service.GetAll());
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
        public IActionResult GetTransactionsByUserId(Guid id)
        {
            return Ok(_service.GetByUserId(id));
        }
        //admin, owner
        [EnableQuery]
        [HttpGet("project/{id}")]
        public IActionResult GetTransactionsByProjectId(Guid id)
        {
            return Ok(_service.GetByProjectId(id));
        }
        [HttpPost]
        public IActionResult CreateTransaction([FromBody] TransactionRequest request)
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
        public IActionResult UpdateTransaction(Guid id, [FromBody] TransactionRequest request)
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
