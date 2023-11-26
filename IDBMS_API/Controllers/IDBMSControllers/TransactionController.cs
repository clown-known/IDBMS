using Microsoft.AspNetCore.Mvc;

namespace IDBMS_API.Controllers.IDBMSControllers
{
    public class TransactionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
