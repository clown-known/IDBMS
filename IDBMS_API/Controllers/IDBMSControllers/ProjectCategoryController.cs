using BusinessObject.DTOs.Request;
using IDBMS_API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Repository.Interfaces;

namespace IDBMS_API.Controllers.IDBMSControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectCategoryController : ODataController
    {
        private readonly ProjectCategoryService _service;

        public ProjectCategoryController(ProjectCategoryService service)
        {
            _service = service;
        }

            [EnableQuery]
            [HttpGet]
            public IActionResult GetProjectCategory()
            {
                return Ok(_service.GetAll());
            }
            [EnableQuery]
            [HttpGet("{id}")]
            public IActionResult GetProjectCategoryById(int id)
            {
                return Ok(_service.GetById(id));
            }

        [HttpPost]
        public IActionResult CreateProjectCategory([FromBody] ProjectCategoryRequest request)
        {
            try
            {
                _service.CreateProjectCategory(request);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProjectCategory(int id, [FromBody] ProjectCategoryRequest request)
        {
            try
            {
                _service.UpdateProjectCategory(id, request);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Ok();
        }

        [HttpPut("{id}/{isHidden}")]
        public IActionResult UpdateProjectCategoryStatus(int id, bool isHidden)
        {
            try
            {
                _service.UpdateProjectCategoryStatus(id, isHidden);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Ok();
        }
    }

}
