using Microsoft.AspNetCore.Mvc;

namespace IDBMS_API.Controllers.IDBMSControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
