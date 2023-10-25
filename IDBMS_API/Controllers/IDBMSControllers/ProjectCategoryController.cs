using BusinessObject.DTOs.Request.CreateRequests;
using IDBMS_API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Repository.Interfaces;

namespace IDBMS_API.Controllers.IDBMSControllers
{
    public class ProjectCategoryController : ODataController
    {
        private readonly ProjectCategoryService _service;
        private readonly IProjectCategoryRepository _repository;

        public ProjectCategoryController(ProjectCategoryService service, IProjectCategoryRepository repository)
        {
            _service = service;
            _repository = repository;
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
                var result = _service.GetById(id);
                if (result == null) return NotFound();
                _service.UpdateProjectCategory(request);
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
                var result = _service.GetById(id);
                if (result == null) return NotFound();
                result.IsHidden = isHidden;
                _repository.Update(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Ok();
        }
    }

}
