using API.Supporters.JwtAuthSupport;
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
<<<<<<< HEAD:IDBMS_API/Controllers/IDBMSControllers/PrepayStageController.cs
    public class PrepayStageController : Controller
=======
    [Route("api/[controller]")]
    [ApiController]
    public class SitesController : ODataController
>>>>>>> dev:IDBMS_API/Controllers/IDBMSControllers/SiteController.cs
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
        [Authorize(Policy = "Admin, Participation, ProjectManager")]
        public IActionResult GetSites(Guid projectId, string? nameOrAddress, int? pageSize, int? pageNo)
        {
            try
            {
                var list = _service.GetAll(nameOrAddress);

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
        public IActionResult GetSiteById(Guid projectId, Guid id)
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
        [Authorize(Policy = "Admin")]
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
        [Authorize(Policy = "Admin, ProjectManager")]
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
        [Authorize(Policy = "Admin, ProjectManager")]
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
