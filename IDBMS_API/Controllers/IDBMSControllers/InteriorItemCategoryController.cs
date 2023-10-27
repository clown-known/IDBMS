using BusinessObject.DTOs.Request;
using IDBMS_API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace IDBMS_API.Controllers.IDBMSControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InteriorItemCategoryController : ODataController
    {
        private readonly InteriorItemCategoryService _service;

        public InteriorItemCategoryController(InteriorItemCategoryService service)
        {
            _service = service;
        }

        [EnableQuery]
        [HttpGet]
        public IActionResult GetInteriorItemCategories()
        {
            return Ok(_service.GetAll());
        }

        [HttpPost]
        public IActionResult CreateInteriorItemCategory([FromBody] InteriorItemCategoryRequest request)
        {
            try
            {
                _service.CreateInteriorItemCategory(request);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateInteriorItemCategory(int id, [FromBody] InteriorItemCategoryRequest request)
        {
            try
            {
                _service.UdpateInteriorItemCategory(id, request);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Ok();
        }

        [HttpPut("{id}/isDeleted")]
        public IActionResult UpdateInteriorItemCategoryStatus(int id, bool isDeleted)
        {
            try
            {
                _service.UdpateInteriorItemCategoryStatus(id, isDeleted);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Ok();
        }
    }

}
