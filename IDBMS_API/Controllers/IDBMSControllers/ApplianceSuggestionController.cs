using BusinessObject.DTOs.Request;
using IDBMS_API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace IDBMS_API.Controllers.IDBMSControllers
{
    public class ApplianceSuggestionController : ODataController
    {
        private readonly ApplianceSuggestionService _service;

        public ApplianceSuggestionController(ApplianceSuggestionService service)
        {
            _service = service;
        }

        [EnableQuery]
        [HttpGet]
        public IActionResult GetApplianceSuggestions()
        {
            return Ok(_service.GetAll());
        }

        [HttpPost]
        public IActionResult CreateApplianceSuggestion([FromBody] ApplianceSuggestionRequest request)
        {
            try
            {
                _service.CreateApplianceSuggestion(request);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateApplianceSuggestion(Guid id, [FromBody] ApplianceSuggestionRequest request)
        {
            try
            {
                _service.UpdateApplianceSuggestion(id, request);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteApplianceSuggestion(Guid id)
        {
            try
            {
                _service.DeleteApplianceSuggestion(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Ok();
        }
    }

}
