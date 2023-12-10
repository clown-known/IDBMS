using BusinessObject.Models;
using DocumentFormat.OpenXml.Drawing.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using IDBMS_API.DTOs.Request;
using IDBMS_API.DTOs.Response;
using IDBMS_API.Services;
using IDBMS_API.Services.PaginationService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using System.Threading.Tasks;

namespace IDBMS_API.Controllers.IDBMSControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentStageDesignsController : ODataController
    {
        private readonly PaymentStageDesignService _service;
        private readonly PaginationService<PaymentStageDesign> _paginationService;

        public PaymentStageDesignsController(PaymentStageDesignService service, PaginationService<PaymentStageDesign> paginationService)
        {
            _service = service;
            _paginationService = paginationService;
        }
        [EnableQuery]
        [HttpGet]
        public IActionResult GetPaymentStageDesigns(string? name, int? pageSize, int? pageNo)
        {
            try
            {
                var list = _service.GetAll(name);

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
        [HttpGet("project-design/{id}")]
        public IActionResult GetPaymentStageDesignsByProjectDesignId(int id, string? name, int? pageSize, int? pageNo)
        {
            try
            {
                var list = _service.GetByProjectDesignId(id, name);

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
