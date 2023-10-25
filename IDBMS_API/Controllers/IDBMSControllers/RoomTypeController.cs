using Azure.Core;
using BusinessObject.DTOs.Request.CreateRequests;
using IDBMS_API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Repository.Interfaces;

namespace IDBMS_API.Controllers.IDBMSControllers
{
    public class RoomTypeController : ODataController
    {
        private readonly RoomTypeService _service;
        private readonly IRoomTypeRepository _repository;

        public RoomTypeController(RoomTypeService service, IRoomTypeRepository repository)
        {
            _service = service;
            _repository = repository;
        }

        [EnableQuery]
        [HttpGet]
        public IActionResult GetRoomType()
        {
            return Ok(_service.GetAll());
        }

        [HttpPost]
        public IActionResult CreateRoomType([FromBody] RoomTypeRequest request)
        {
            try
            {
                _service.CreateRoomType(request);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateRoomType(int id, [FromBody] RoomTypeRequest request)
        {
            try
            {
                _service.UpdateRoomType(request, id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Ok();
        }

        [HttpPut("{id}/{isHidden}")]
        public IActionResult UpdateRoomTypeStatus(int id, bool isHidden)
        {
            try
            {
                var result = _service.GetById(id);
                if (result == null) return NotFound();
                result.IsHidden = isHidden;
                _repository.Update(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Ok();
        }
    }

}
