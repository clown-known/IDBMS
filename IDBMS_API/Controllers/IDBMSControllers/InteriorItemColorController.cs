﻿using API.Supporters.JwtAuthSupport;
using BusinessObject.Enums;
using BusinessObject.Models;
using DocumentFormat.OpenXml.Drawing.Spreadsheet;
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
            try
            {
                var list = _service.GetAll(type, name);

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
        //guest
        [EnableQuery]
        [HttpGet("interior-item-category/{id}")]
        public IActionResult GetInteriorItemColorsByCategoryId(int id, ColorType? type, string? name, int? pageSize, int? pageNo)
        {
            try
            {
                var list = _service.GetByCategoryId(id, type, name);

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
        public IActionResult GetInteriorItemColorsById(int id)
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
        public async Task<IActionResult> CreateInteriorItemColor([FromForm][FromBody] InteriorItemColorRequest request)
        {
            try
            {
                try
                {
                    var result = await _service.CreateInteriorItemColor(request);
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
        [Authorize(Policy = "")]
        public async Task<IActionResult> UpdateInteriorItemColor(int id, [FromForm][FromBody] InteriorItemColorRequest request)
        {
            try
            {
                await _service.UpdateInteriorItemColor(id, request);
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
