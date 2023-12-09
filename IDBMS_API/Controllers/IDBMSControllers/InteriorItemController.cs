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
            var list = _service.GetAll(itemCategoryId, status, codeOrName, itemType);

            return Ok(_paginationService.PaginateList(list, pageSize, pageNo));
        }
        [EnableQuery]
        [HttpGet("interior-item-category/{id}")]
        public IActionResult GetInteriorItemsByCategory(int id, int? itemCategoryId, InteriorItemStatus? status, string? codeOrName, InteriorItemType? itemType, int? pageSize, int? pageNo)
        {
            var list = _service.GetByCategory(id, itemCategoryId, status, codeOrName, itemType);

            return Ok(_paginationService.PaginateList(list, pageSize, pageNo));
        }
        [HttpPost]
        public IActionResult CreateInteriorItem([FromBody][FromForm] InteriorItemRequest request)
        {
            try
            {
                var result = _service.CreateInteriorItem(request);
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
        public IActionResult UpdateInteriorItem(Guid id, [FromBody][FromForm] InteriorItemRequest request)
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
