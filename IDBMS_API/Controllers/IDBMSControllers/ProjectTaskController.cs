using Microsoft.AspNetCore.Mvc;

namespace IDBMS_API.Controllers.IDBMSControllers
{
    public class ProjectTaskController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
