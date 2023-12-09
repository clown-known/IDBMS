using BusinessObject.Models;
using IDBMS_API.DTOs.Request;
using IDBMS_API.DTOs.Response;
using IDBMS_API.Services;
using IDBMS_API.Services.PaginationService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace IDBMS_API.Controllers.IDBMSControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SitesController : ODataController
    {
        private readonly SiteService _service;
        private readonly PaginationService<Site> _paginationService;

        public SitesController(SiteService service, PaginationService<Site> paginationService)
        {
            _service = service;
            _paginationService = paginationService;
        }

        [EnableQuery]
        [HttpGet]
        public IActionResult GetSites(string? nameOrAddress, int? pageSize, int? pageNo)
        {
            var list = _service.GetAll(nameOrAddress);

            return Ok(_paginationService.PaginateList(list, pageSize, pageNo));
        }

        [EnableQuery]
        [HttpGet("{id}")]
        public IActionResult GetSiteById(Guid id)
        {
            return Ok(_service.GetById(id));
        }

        [HttpPost]
        public IActionResult CreateSite([FromBody] SiteRequest request)
        {
                try
                {
                    var result = _service.CreateSite(request);
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
        public IActionResult UpdateSite(Guid id, [FromBody] SiteRequest request)
        {
            try
            {
                _service.UpdateSite(id, request);
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
        public IActionResult DeleteSite(Guid id)
        {
            try
            {
                _service.DeleteSite(id);
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
