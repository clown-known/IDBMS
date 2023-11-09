using Microsoft.AspNetCore.Mvc;

namespace IDBMS_API.Controllers.IDBMSControllers
{
    public class TaskReportController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
