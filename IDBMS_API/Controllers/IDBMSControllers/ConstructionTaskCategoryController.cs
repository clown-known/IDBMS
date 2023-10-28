using Azure.Core;
using BusinessObject.DTOs.Request;
using BusinessObject.DTOs.Response;
using IDBMS_API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Repository.Interfaces;

namespace IDBMS_API.Controllers.IDBMSControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConstructionTaskCategoriesController : ODataController
    {
        private readonly ConstructionTaskCategoryService _service;

        public ConstructionTaskCategoriesController(ConstructionTaskCategoryService service)
        {
            _service = service;
        }

        [EnableQuery]
        [HttpGet]
        public IActionResult GetConstructionTaskCategories()
        {
            var response = new ResponseMessage()
            {
                Message = "Get successfully!",
                Data = _service.GetAll()
            };
            return Ok(response);
        }

        [HttpPost]
        public IActionResult CreateConstructionTaskCategory([FromBody] ConstructionTaskCategoryRequest request)
        {
            try
            {
                var result = _service.CreateConstructionTaskCategory(request);
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
        public IActionResult UpdateConstructionTaskCategory(int id, [FromBody] ConstructionTaskCategoryRequest request)
        {
            try
            {
                _service.UpdateConstructionTaskCategory(id, request);
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

        [HttpPut("{id}/isDeleted")]
        public IActionResult UpdateConstructionTaskCategoryStatus(int id, bool isDeleted)
        {
            try
            {
                _service.UpdateConstructionTaskCategoryStatus(id, isDeleted);
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
