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
            var list = _service.GetAll(isHidden, name);

            return Ok(_paginationService.PaginateList(list, pageSize, pageNo));
        }
        [EnableQuery]
        [HttpGet("{id}")]
        public IActionResult GetProjectCategoryById(int id)
        { 
            return Ok(_service.GetById(id));
        }

        [HttpPost]
        public IActionResult CreateProjectCategory([FromBody][FromForm] ProjectCategoryRequest request)
        {
            try
            {
                var result = _service.CreateProjectCategory(request);
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
        public IActionResult UpdateProjectCategory(int id, [FromBody][FromForm] ProjectCategoryRequest request)
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
