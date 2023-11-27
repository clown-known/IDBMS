using Azure.Core;
using IDBMS_API.DTOs.Request;
using IDBMS_API.DTOs.Response;
using IDBMS_API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace IDBMS_API.Controllers.IDBMSControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentStagesController : ODataController
    {
        private readonly PaymentStageService _service;

        public PaymentStagesController(PaymentStageService service)
        {
            _service = service;
        }

        [EnableQuery]
        [HttpGet]
        public IActionResult GetPaymentStages()
        {
            return Ok(_service.GetAll());
        }
        //all
        [EnableQuery]
        [HttpGet("project/{id}")]
        public IActionResult GetPaymentStagesByProjectId(Guid id)
        {
            return Ok(_service.GetByProjectId(id));
        }
        //permission
        [EnableQuery]
        [HttpGet("{id}")]
        public IActionResult GetPaymentStagesById(Guid id)
        {
            return Ok(_service.GetById(id));
        }
        [HttpPost]
        public IActionResult CreatePaymentStage([FromBody] PaymentStageRequest request)
        {
            try
            {
                var result = _service.CreatePaymentStage(request);
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
        public IActionResult UpdatePaymentStage(Guid id, [FromBody] PaymentStageRequest request)
        {
            try
            {
                _service.UpdatePaymentStage(id, request);
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

        [HttpPut("{id}/isHidden")]
        public IActionResult UpdatePaymentStageStatus(Guid id, bool isHidden)
        {
            try
            {
                _service.UpdatePaymentStageStatus(id, isHidden);
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
