using Microsoft.AspNetCore.Mvc;

namespace IDBMS_API.Controllers.IDBMSControllers
{
    public class TaskDesignController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
