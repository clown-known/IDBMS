using Microsoft.AspNetCore.Mvc;

namespace IDBMS_API.Controllers.IDBMSControllers
{
    public class DocumentTemplateController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
