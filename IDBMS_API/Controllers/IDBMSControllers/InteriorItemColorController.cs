using BusinessObject.DTOs.Request;
using BusinessObject.DTOs.Response;
using IDBMS_API.Services;
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

        public InteriorItemColorsController(InteriorItemColorService service)
        {
            _service = service;
        }

        [EnableQuery]
        [HttpGet]
        public IActionResult GetInteriorItemColors()
        {
            var response = new ResponseMessage()
            {
                Message = "Get successfully!",
                Data = _service.GetAll()
            };
            return Ok(_service.GetAll());
        }
        //guest
        [EnableQuery]
        [HttpGet("interior-item-category/{id}")]
        public IActionResult GetInteriorItemColorsByCategory(int id)
        {
            var response = new ResponseMessage()
            {
                Message = "Get successfully!",
                Data = _service.GetByCategory(id)
            };
            return Ok(_service.GetByCategory(id));
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
