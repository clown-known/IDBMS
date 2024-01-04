using API.Supporters.JwtAuthSupport;
using BLL.Services;
using IDBMS_API.DTOs.Response;
using Microsoft.AspNetCore.Mvc;

namespace IDBMS_API.Controllers.IDBMSControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DownloadController : Controller
    {
        FirebaseService firebaseService;
        public DownloadController(FirebaseService firebaseService)
        {
            this.firebaseService = firebaseService;
        }
        [HttpGet]
        [Authorize(Policy = "User")]
        public async Task<DownloadResponse> Download(string url, string? name)
        {
            var content = await firebaseService.DownloadFileByDownloadUrl(url);
            string before = url.Split("?alt=")[0];

            string fileType = Path.GetExtension(before);
            string fileName = "";

            if (name != null)
            {
                fileName = $"{name}{fileType}";
            }
            else
            {
                fileName = before.Substring(before.LastIndexOf("%2F") + 3);
            }

            var response = new DownloadResponse
            {
                FileName = fileName,
                FileType = fileType,
                File = File(content, "application/octet-stream", fileName),
            };

            return response;
        }
    }
}
