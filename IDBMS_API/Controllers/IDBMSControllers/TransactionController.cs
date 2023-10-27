using BusinessObject.DTOs.Request;
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
        [HttpGet]
        [Route("user/{id}")]
        public IActionResult GetTransactionsByUserId(Guid id)
        {
            return Ok(_service.GetByUserId(id));
        }
        [HttpPost]
        public IActionResult CreateTransaction([FromBody] TransactionRequest request)
        {
            try
            {
                _service.CreateTransaction(request);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTransaction(Guid id, [FromBody] TransactionRequest request)
        {
            try
            {
                _service.UpdateTransaction(id, request);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Ok();
        }

        [HttpPut]
        [Route("{id}/status")]
        public IActionResult UpdateTransactionStatus(Guid id, int status)
        {
            try
            {
                _service.UpdateTransactionStatus(id, status);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteTransaction(Guid id)
        {
            try
            {
                _service.DeleteTransaction(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Ok();
        }
    }

}
