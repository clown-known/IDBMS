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
    public class WarrantyClaimsController : ODataController
    {
        private readonly WarrantyClaimService _service;

        public WarrantyClaimsController(WarrantyClaimService service)
        {
            _service = service;
        }

        [EnableQuery]
        [HttpGet]
        public IActionResult GetWarrantyClaims()
        {
            return Ok(_service.GetAll());
        }

        [EnableQuery]
        [HttpGet("user/{id}")]
        public IActionResult GetWarrantyClaimsByUserId(Guid id)
        {
            return Ok(_service.GetByUserId(id));
        }
        [EnableQuery]
        [HttpGet("project/{id}")]
        public IActionResult GetWarrantyClaimsByProjectId(Guid id)
        {
            return Ok(_service.GetByProjectId(id));
        }
        [HttpPost]
        public IActionResult CreateWarrantyClaim([FromBody] WarrantyClaimRequest request)
        {
                try
                {
                    var result = _service.CreateWarrantyClaim(request);
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
        public IActionResult UpdateWarrantyClaim(Guid id, [FromBody] WarrantyClaimRequest request)
        {
            try
            {
                _service.UpdateWarrantyClaim(id, request);
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
        public IActionResult DeleteWarrantyClaim(Guid id)
        {
            try
            {
                _service.DeleteWarrantyClaim(id);
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
