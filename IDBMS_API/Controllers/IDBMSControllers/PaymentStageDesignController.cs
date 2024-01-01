using API.Supporters.JwtAuthSupport;
using BusinessObject.Models;
using DocumentFormat.OpenXml.Drawing.Spreadsheet;
using DocumentFormat.OpenXml.Office2010.Excel;
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
        [Authorize(Policy = "Admin, Participation, ProjectManager")]
        public IActionResult GetPaymentStageDesigns(Guid projectId, string? name, int? pageSize, int? pageNo)
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
        [Authorize(Policy = "Admin, Participation, ProjectManager")]
        public IActionResult GetPaymentStageDesignsByProjectDesignId(Guid projectId, int id, string? name, int? pageSize, int? pageNo)
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

        [EnableQuery]
        [HttpGet("{id}")]
        [Authorize(Policy = "Admin, Participation, ProjectManager")]
        public IActionResult GetPaymentStageDesignById(Guid projectId, int id)
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
        [Authorize(Policy = "Admin, ProjectManager")]
        public IActionResult CreatePaymentStageDesign([FromBody] PaymentStageDesignRequest request)
        {
            try
            {
                _service.CreatePaymentStageDesign(request);
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
        [Authorize(Policy = "Admin, ProjectManager")]
        public IActionResult UpdatePaymentStageDesign(int id, [FromBody] PaymentStageDesignRequest request)
        {
            try
            {
                _service.UpdatePaymentStageDesign(id, request);
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
        [Authorize(Policy = "Admin, ProjectManager")]
        public IActionResult DeletePaymentStageDesign(int id)
        {
            try
            {
                _service.DeletePaymentStageDesign(id);
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
