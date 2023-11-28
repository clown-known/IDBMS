using API.Supporters.JwtAuthSupport;
using BLL.Services;
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
        [Authorize]
        public async Task<IActionResult> Download(string url)
        {
            var content = await firebaseService.DownloadFileByDownloadUrl(url);
            string before = url.Split("?alt=")[0];
            string fileName = before.Substring(before.LastIndexOf("%2F")+3);
            return File(content, "application/octet-stream",fileName);
        }
    }
}
