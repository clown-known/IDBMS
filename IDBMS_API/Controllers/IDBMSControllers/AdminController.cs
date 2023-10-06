using Microsoft.AspNetCore.Mvc;

namespace IDBMS_API.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
