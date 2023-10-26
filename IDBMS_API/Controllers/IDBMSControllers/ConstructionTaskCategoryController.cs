using BusinessObject.DTOs.Request;
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

        public ConstructionTaskCategoryController(ConstructionTaskCategoryService service)
        {
            _service = service;
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

                _service.UpdateConstructionTaskCategory(id, request);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Ok();
        }

        [HttpPut("{id}/isDeleted")]
        public IActionResult UpdateConstructionTaskCategoryStatus(int id, bool isDeleted)
        {
            try
            {
                _service.UpdateConstructionTaskCategoryStatus(id, isDeleted);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Ok();
        }
    }

}
