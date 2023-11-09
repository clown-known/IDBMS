using Microsoft.AspNetCore.Mvc;

namespace IDBMS_API.Controllers.IDBMSControllers
{
    public class TaskCategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
