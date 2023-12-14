using Azure.Core;
using IDBMS_API.DTOs.Response;
using IDBMS_API.DTOs.Request;
using IDBMS_API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Repository.Interfaces;
using BusinessObject.Models;
using IDBMS_API.Services.PaginationService;
using DocumentFormat.OpenXml.Drawing.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Threading.Tasks;

namespace IDBMS_API.Controllers.IDBMSControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectCategoriesController : ODataController
    {
        private readonly ProjectCategoryService _service;
        private readonly PaginationService<ProjectCategory> _paginationService;

        public ProjectCategoriesController(ProjectCategoryService service, PaginationService<ProjectCategory> paginationService)
        {
            _service = service;
            _paginationService = paginationService;
        }

        [EnableQuery]
        [HttpGet]
        public IActionResult GetProjectCategories(bool? isHidden, string? name, int? pageSize, int? pageNo)
        {
            try
            {
                var list = _service.GetAll(isHidden, name);

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
        public IActionResult GetProjectCategoryById(int id)
        { 
            return Ok(_service.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateProjectCategory([FromForm][FromBody] ProjectCategoryRequest request)
        {
            try
            {
                var result = await _service.CreateProjectCategory(request);
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
        public IActionResult UpdateProjectCategory(int id, [FromForm][FromBody] ProjectCategoryRequest request)
        {
            try
            {
                _service.UpdateProjectCategory(id, request);
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
        public IActionResult UpdateProjectCategoryStatus(int id, bool isHidden)
        {
            try
            {
                _service.UpdateProjectCategoryStatus(id, isHidden);
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
