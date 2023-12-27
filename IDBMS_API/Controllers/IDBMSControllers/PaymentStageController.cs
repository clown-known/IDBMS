using Azure.Core;
using BusinessObject.Enums;
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
    public class PaymentStagesController : ODataController
    {
        private readonly PaymentStageService _service;
        private readonly PaginationService<PaymentStage> _paginationService;
        private readonly PaginationService<PaymentStageResponse> _paginationResponseService;

        public PaymentStagesController(PaymentStageService service, PaginationService<PaymentStage> paginationService, PaginationService<PaymentStageResponse> paginationResponseService)
        {
            _service = service;
            _paginationService = paginationService;
            _paginationResponseService = paginationResponseService;
        }

        [EnableQuery]
        [HttpGet]
        public IActionResult GetPaymentStages(StageStatus? status, string? name, int? pageSize, int? pageNo)
        {
            try
            {
                var list = _service.GetAll(status, name);

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
        //all
        [EnableQuery]
        [HttpGet("project/{id}")]
        public IActionResult GetPaymentStagesByProjectId(Guid id, StageStatus? status, string? name, int? pageSize, int? pageNo)
        {
            try
            {
                var list = _service.GetByProjectIdWithActionAllowed(id, status, name);

                var response = new ResponseMessage()
                {
                    Message = "Get successfully!",
                    Data = _paginationResponseService.PaginateList(list, pageSize, pageNo)
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
        //permission
        [EnableQuery]
        [HttpGet("{id}")]
        public IActionResult GetPaymentStagesById(Guid id)
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

        [HttpPost("project/{id}")]
        public IActionResult CreatePaymentStagesByDesigns( Guid id)
        {
            try
            {
                _service.CreatePaymentStagesByProjectDesign(id);
                var response = new ResponseMessage()
                {
                    Message = "Create successfully!",
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

        [HttpPut("{id}/start")]
        public IActionResult StartStage(Guid id)
        {
            try
            {
                _service.StartStage(id);
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

        [HttpPut("{id}/end")]
        public IActionResult CloseStage(Guid id)
        {
            try
            {
                _service.CloseStage(id);
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

        [HttpPut("{id}/suspend")]
        public IActionResult SuspendStage(Guid id)
        {
            try
            {
                _service.SuspendStage(id);
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

        [HttpPut("{id}/reopen")]
        public IActionResult ReopenStage(Guid id, [FromBody] decimal penaltyFee)
        {
            try
            {
                _service.ReopenStage(id, penaltyFee);
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

        [HttpPut("{id}/penaltyFee")]
        public IActionResult UpdateStagePenaltyFee(Guid id, decimal penaltyFee)
        {
            try
            {
                _service.UpdateStagePenaltyFee(id, penaltyFee);
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
        public IActionResult UpdatePaymentStageStatus(Guid id)
        {
            try
            {
                _service.DeletePaymentStage(id);
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
