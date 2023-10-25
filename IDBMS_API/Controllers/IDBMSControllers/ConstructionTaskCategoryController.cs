using BusinessObject.DTOs.Request.CreateRequests;
using IDBMS_API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Repository.Interfaces;

namespace IDBMS_API.Controllers.IDBMSControllers
{
    public class ConstructionTaskCategoryController : ODataController
    {
        private readonly ConstructionTaskCategoryService _service;
        private readonly IConstructionTaskCategoryRepository _repository;

        public ConstructionTaskCategoryController(ConstructionTaskCategoryService service, IConstructionTaskCategoryRepository repository)
        {
            _service = service;
            _repository = repository;
        }

        [EnableQuery]
        [HttpGet]
        public IActionResult GetConstructionTaskCategory()
        {
            return Ok(_service.GetAll());
        }

        [HttpPost]
        public IActionResult CreateConstructionTaskCategory([FromBody] ConstructionTaskCategoryRequest request)
        {
            try
            {
                _service.CreateConstructionTaskCategory(request);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateConstructionTaskCategory(int id, [FromBody] ConstructionTaskCategoryRequest request)
        {
            try
            {
                var result = _service.GetById(id);
                if (result == null) return NotFound();
                _service.UpdateConstructionTaskCategory(request);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Ok();
        }

        [HttpPut("{id}/{IsDeleted}")]
        public IActionResult UpdateConstructionTaskCategoryStatus(int id, bool IsDeleted)
        {
            try
            {
                var result = _service.GetById(id);
                if (result == null) return NotFound();
                result.IsDeleted = IsDeleted;
                _repository.Update(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteConstructionTaskCategory(int id)
        {
            try
            {
                var result = _service.GetById(id);
                if (result == null) return NotFound();
                _repository.DeleteById(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Ok();
        }
    }

}
