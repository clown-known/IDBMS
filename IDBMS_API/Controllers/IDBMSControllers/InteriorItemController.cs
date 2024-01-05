using Azure.Core;
using IDBMS_API.DTOs.Request;
using IDBMS_API.DTOs.Response;
using BusinessObject.Enums;
using IDBMS_API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using BusinessObject.Models;
using IDBMS_API.Services.PaginationService;
using DocumentFormat.OpenXml.Drawing.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Threading.Tasks;
using API.Supporters.JwtAuthSupport;

namespace IDBMS_API.Controllers.IDBMSControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InteriorItemsController : ODataController
    {
        private readonly InteriorItemService _service;
        private readonly PaginationService<InteriorItem> _paginationService;

        public InteriorItemsController(InteriorItemService service, PaginationService<InteriorItem> paginationService)
        {
            _service = service;
            _paginationService = paginationService;
        }

        [EnableQuery]
        [HttpGet]
        public IActionResult GetInteriorItems(int? itemCategoryId, InteriorItemStatus? status, string? codeOrName, InteriorItemType? itemType, int? pageSize, int? pageNo)
        {
            try
            {
                var list = _service.GetAll(itemCategoryId, status, codeOrName, itemType);

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
        [HttpGet("interior-item-category/{id}")]
        public IActionResult GetInteriorItemsByCategory(int id, int? itemCategoryId, InteriorItemStatus? status, string? codeOrName, InteriorItemType? itemType, int? pageSize, int? pageNo)
        {
            try
            {
                var list = _service.GetByCategory(id, itemCategoryId, status, codeOrName, itemType);

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
        public IActionResult GetInteriorItemById(Guid id)
        {
            try
            {
                var response = new ResponseMessage()
                {
                    Message = "Get successfully!",
                    Data = _service.GetById(id),
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

        [HttpPost]
        [Authorize(Policy = "")]
        public async Task<IActionResult> CreateInteriorItem([FromForm][FromBody] InteriorItemRequest request)
        {
            try
            {
                var result = await _service.CreateInteriorItem(request);
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
        [Authorize(Policy = "")]
        public IActionResult UpdateInteriorItem(Guid id, [FromBody] InteriorItemRequest request)
        {
            try
            {
                _service.UpdateInteriorItem(id, request);
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

        [HttpPut("{id}/status")]
        [Authorize(Policy = "")]
        public IActionResult UpdateInteriorItemStatus(Guid id, InteriorItemStatus status)
        {
            try
            {
                _service.UpdateInteriorItemStatus(id, status);
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
        [Authorize(Policy = "")]
        public IActionResult DeleteInteriorItem(Guid id)
        {
            try
            {
                _service.DeleteInteriorItem(id);
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
