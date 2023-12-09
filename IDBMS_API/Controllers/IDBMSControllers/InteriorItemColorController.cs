using BusinessObject.Enums;
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
    public class InteriorItemColorsController : ODataController
    {
        private readonly InteriorItemColorService _service;
        private readonly PaginationService<InteriorItemColor> _paginationService;

        public InteriorItemColorsController(InteriorItemColorService service, PaginationService<InteriorItemColor> paginationService)
        {
            _service = service;
            _paginationService = paginationService;
        }

        [EnableQuery]
        [HttpGet]
        public IActionResult GetInteriorItemColors(ColorType? type, string? name, int? pageSize, int? pageNo)
        {
            var list = _service.GetAll(type, name);

            return Ok(_paginationService.PaginateList(list, pageSize, pageNo));
        }
        //guest
        [EnableQuery]
        [HttpGet("interior-item-category/{id}")]
        public IActionResult GetInteriorItemColorsByCategoryId(int id, ColorType? type, string? name, int? pageSize, int? pageNo)
        {
            var list = _service.GetByCategoryId(id, type, name);

            return Ok(_paginationService.PaginateList(list, pageSize, pageNo));
        }
        [HttpPost]
        public IActionResult CreateInteriorItemColor([FromBody] InteriorItemColorRequest request)
        {
            try
            {
                try
                {
                    var result = _service.CreateInteriorItemColor(request);
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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateInteriorItemColor(int id, [FromBody] InteriorItemColorRequest request)
        {
            try
            {
                _service.UpdateInteriorItemColor(id, request);
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
        public IActionResult DeleteInteriorItemColor(int id)
        {
            try
            {
                _service.DeleteInteriorItemColor(id);
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
