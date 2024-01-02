using API.Supporters.JwtAuthSupport;
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
        [Authorize(Policy = "Admin, Participation, ProjectManager")]
        public IActionResult GetPaymentStages(Guid projectId, StageStatus? status, string? name, int? pageSize, int? pageNo)
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
        [Authorize(Policy = "Admin, Participation, ProjectManager")]
        public IActionResult GetPaymentStagesByProjectId(Guid projectId, StageStatus? status, string? name, int? pageSize, int? pageNo)
        {
            try
            {
                var list = _service.GetByProjectId(projectId, status, name);

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
        [HttpGet("project/{id}/actions")]
        [Authorize(Policy = "Admin, Participation, ProjectManager")]
        public IActionResult GetPaymentStagesByProjectIdWithActions(Guid projectId, StageStatus? status, string? name, int? pageSize, int? pageNo)
        {
            try
            {
                var list = _service.GetByProjectIdWithActionAllowed(projectId, status, name);

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
        [Authorize(Policy = "Admin, Participation, ProjectManager")]
        public IActionResult GetPaymentStagesById(Guid projectId, Guid id)
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
        [Authorize(Policy = "Admin, Participation, ProjectManager")]
        public IActionResult CreatePaymentStage(Guid projectId, [FromBody] PaymentStageRequest request)
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
        [Authorize(Policy = "Admin, Participation, ProjectManager")]
        public IActionResult CreatePaymentStagesByDesigns(Guid projectId, Guid id)
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
        [Authorize(Policy = "Admin, Participation, ProjectManager")]
        public IActionResult UpdatePaymentStage(Guid projectId, Guid id, [FromBody] PaymentStageRequest request)
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
        [Authorize(Policy = "Admin, Participation, ProjectManager")]
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
        [Authorize(Policy = "Admin, Participation, ProjectManager")]
        public IActionResult CloseStage(Guid projectId, Guid id)
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
        [Authorize(Policy = "Admin, Participation, ProjectManager")]
        public IActionResult SuspendStage(Guid projectId, Guid id)
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
        [Authorize(Policy = "Admin, Participation, ProjectManager")]
        public IActionResult ReopenStage(Guid projectId, Guid id, [FromBody] decimal penaltyFee)
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
        [Authorize(Policy = "Admin, Participation, ProjectManager")]
        public IActionResult UpdateStagePenaltyFee(Guid projectId, Guid id, decimal penaltyFee)
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
        [Authorize(Policy = "Admin, Participation, ProjectManager")]
        public IActionResult DeletePaymentStage(Guid projectId, Guid id)
        {
            try
            {
                _service.DeletePaymentStage(id);
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
