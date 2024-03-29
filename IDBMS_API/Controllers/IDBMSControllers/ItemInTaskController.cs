﻿using API.Supporters.JwtAuthSupport;
using Azure.Core;
using BusinessObject.Enums;
using BusinessObject.Models;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using IDBMS_API.DTOs.Request;
using IDBMS_API.DTOs.Response;
using IDBMS_API.Services;
using IDBMS_API.Services.PaginationService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using System.Threading.Tasks;

namespace IDBMS_API.Controllers.IDBMSControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemInTasksController : ODataController
    {
        private readonly ItemInTaskService _service;
        private readonly PaginationService<ItemInTask> _paginationService;

        public ItemInTasksController(ItemInTaskService service, PaginationService<ItemInTask> paginationService)
        {
            _service = service;
            _paginationService = paginationService;
        }

/*        [EnableQuery]
        [HttpGet]
        public IActionResult GetItemInTasks()
        {
            return Ok(_service.GetAll());
        }*/

        [EnableQuery]
        [HttpGet("{id}")]
        [Authorize(Policy = "Participation")]
        public IActionResult GetItemInTaskById(Guid projectId, Guid id)
        {
            return Ok(_service.GetById(id));
        }

        [EnableQuery]
        [HttpGet("project/{id}")]
        [Authorize(Policy = "Participation")]
        public IActionResult GetItemInTaskByProjectId(Guid projectId, Guid id,
            string? itemCodeOrName, int? pageSize, int? pageNo, int? itemCategoryId, ProjectTaskStatus? taskStatus)
        {
            try
            {
                var list = _service.GetByProjectId(id, itemCodeOrName, itemCategoryId, taskStatus);

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

/*        [EnableQuery]
        [HttpGet("room/{id}")]
        public IActionResult GetItemInTaskByRoomId(Guid id)
        {
            return Ok(_service.GetByRoomId(id));
        }*/

        [EnableQuery]
        [HttpGet("project-task/{id}")]
        [Authorize(Policy = "Participation")]
        public IActionResult GetItemInTaskByTaskId(Guid projectId, Guid id, 
            string? itemCodeOrName, int? pageSize, int? pageNo, int? itemCategoryId, ProjectTaskStatus? taskStatus)
        {
            try
            {
                var list = _service.GetByTaskId(id, itemCodeOrName, itemCategoryId, taskStatus);

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


        [HttpPost]
        [Authorize(Policy = "ProjectManager, Architect, ConstructionManager")]
        public async Task<IActionResult> CreateItemInTask(Guid projectId, [FromForm][FromBody] ItemInTaskRequest request)
        {
            try
            {
                var result = await _service.CreateItemInTask(request);
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

        [HttpPost("project-task/{id}")]
        [Authorize(Policy = "ProjectManager, Architect, ConstructionManager")]
        public async Task<IActionResult> CreateItemsByTaskId(Guid projectId, [FromForm][FromBody] List<ItemInTaskRequest> request)
        {
            try
            {
                await _service.CreateItemsByTaskId(request);
                var response = new ResponseMessage()
                {
                    Message = "Create successfully!",
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
        [Authorize(Policy = "ProjectManager, Architect, ConstructionManager")]
        public async Task<IActionResult> UpdateItemInTask(Guid projectId, Guid id, [FromForm][FromBody] ItemInTaskRequest request)
        {
            try
            {
                await _service.UpdateItemInTask(id, request);
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

        [HttpPut("{id}/quantity")]
        [Authorize(Policy = "ProjectManager, Architect, ConstructionManager")]
        public IActionResult UpdateItemInTaskQuantity(Guid projectId, Guid id, int quantity)
        {
            try
            {
                _service.UpdateItemInTaskQuantity(id, quantity);
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
        [Authorize(Policy = "ProjectManager, Architect, ConstructionManager")]
        public IActionResult DeleteItemInTask(Guid projectId, Guid id)
        {
            try
            {
                _service.DeleteItemInTask(id);
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
