using Microsoft.AspNetCore.Mvc;

namespace IDBMS_API.Controllers.IDBMSControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrepayStageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
