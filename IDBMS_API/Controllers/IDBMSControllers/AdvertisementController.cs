using API.Supporters.JwtAuthSupport;
using BusinessObject.Enums;
using BusinessObject.Models;
using DocumentFormat.OpenXml.Wordprocessing;
using IDBMS_API.DTOs.Request.AdvertisementRequest;
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
    public class AdvertisementProjectsController : ODataController
    {
        private readonly AdvertisementService _service;
        private readonly PaginationService<Project> _projectPaginationService;
        private readonly PaginationService<ProjectDocument> _documentPaginationService;

        public AdvertisementProjectsController(AdvertisementService service,
            PaginationService<Project> projectPaginationService, PaginationService<ProjectDocument> documentPaginationService)
        {
            _service = service;
            _projectPaginationService = projectPaginationService;
            _documentPaginationService = documentPaginationService;
        }

        [EnableQuery]
        [HttpGet]
        public IActionResult GetAdvertisementAllowedProjects(int? pageSize, int? pageNo, ProjectType? type, AdvertisementStatus? status, string? name)
        {
            try
            {
                var list = _service.GetAllProjects(type, status, name);

                var response = new ResponseMessage()
                {
                    Message = "Get successfully!",
                    Data = _projectPaginationService.PaginateList(list, pageSize, pageNo)
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
        [HttpGet("{projectId}/documents")]
        public IActionResult GetDocumentsByProjectId(Guid projectId, int? pageSize, int? pageNo,
            bool? isPublicAdvertisement)
        {
            try
            {
                var list = _service.GetImagesByProjectId(projectId, isPublicAdvertisement);

                var response = new ResponseMessage()
                {
                    Message = "Get successfully!",
                    Data = _documentPaginationService.PaginateList(list, pageSize, pageNo)
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
        public IActionResult GetAdvertisementProjectById(Guid id)
        {
            try
            {
                var response = new ResponseMessage()
                {
                    Message = "Get successfully!",
                    Data = _service.GetAdProjectById(id),
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

        [HttpPost("/project")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> CreateAdvertisementProject([FromForm][FromBody] AdvertisementProjectRequest request)
        {
            try
            {
                var result = await _service.CreateAdvertisementProject(request);

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

        [HttpPost("{projectId}/images")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> CreateCompletionImage([FromForm][FromBody] List<AdvertisementImageRequest> request)
        {
            try
            {
                await _service.CreateCompletionImage(request);

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

        [HttpDelete("image/{id}")]
        [Authorize(Policy = "Admin")]
        public IActionResult DeleteCompletionImage(Guid id)
        {
            try
            {
                _service.DeleteCompletionImage(id);

                var response = new ResponseMessage()
                {
                    Message = "Delete successfully!"
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

        [HttpPut("document/{documentId}/public")]
        [Authorize(Policy = "Admin")]
        public IActionResult UpdatePublicDocument(Guid documentId, [FromQuery] bool isPublicAdvertisement)
        {
            try
            {
                _service.UpdatePublicImage(documentId, isPublicAdvertisement);

                var response = new ResponseMessage()
                {
                    Message = "Update successfully!"
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

        [HttpPut("{projectId}/advertisementDescription")]
        [Authorize(Policy = "Admin")]
        public IActionResult UpdateAdvertisementDescription(Guid projectId, [FromForm][FromBody] AdvertisementDescriptionRequest request)
        {
            try
            {
                _service.UpdateAdProjectDescription(projectId, request);

                var response = new ResponseMessage()
                {
                    Message = "Update successfully!"
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

        [HttpPut("{projectId}/advertisementStatus")]
        [Authorize(Policy = "Admin")]
        public IActionResult UpdateProjectAdvertisementStatus(Guid projectId, AdvertisementStatus status)
        {
            try
            {
                _service.UpdateProjectAdvertisementStatus(projectId, status);
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
