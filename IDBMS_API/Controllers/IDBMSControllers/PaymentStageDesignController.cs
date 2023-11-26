using BusinessObject.DTOs.Request;
using IDBMS_API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace IDBMS_API.Controllers.IDBMSControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentStageDesignsController : ODataController
    {
        private readonly PaymentStageDesignService _service;

        public PaymentStageDesignsController(PaymentStageDesignService service)
        {
            _service = service;
        }

        [EnableQuery]
        [HttpGet]
        public IActionResult GetPaymentStageDesigns()
        {
            return Ok(_service.GetAll());
        }

        [HttpPost]
        public IActionResult CreatePaymentStageDesign([FromBody] PaymentStageDesignRequest request)
        {
            try
            {
                _service.CreatePaymentStageDesign(request);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePaymentStageDesign(int id, [FromBody] PaymentStageDesignRequest request)
        {
            try
            {
                _service.UpdatePaymentStageDesign(id, request);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePaymentStageDesign(int id)
        {
            try
            {
                _service.DeletePaymentStageDesign(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Ok();
        }
    }

}
