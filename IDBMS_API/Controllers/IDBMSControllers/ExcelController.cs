using IDBMS_API.DTOs.Request;
using IDBMS_API.DTOs.Response;
using IDBMS_API.Services;
using IDBMS_API.Services.ExcelService;
using Microsoft.AspNetCore.Mvc;

namespace IDBMS_API.Controllers.IDBMSControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExcelController : Controller
    {
        ExcelService excelService;
        public ExcelController(ExcelService excelService)
        {
            this.excelService = excelService;
        }


        [HttpPost]
        public async Task<IActionResult> GenExcel(Guid request)
        {

                byte[] file = await excelService.GenNewExcel(request);
                //string fileName = "Contract-"+projectid.ToString()+".docx";
                string fileName = "TemplateExcel.xlsx";

                var response = new ResponseMessage()
                {
                    Message = "Generate successfully!",
                    Data = File(file, "application/octet-stream", fileName),
                };

                return File(file, "application/octet-stream", fileName);

        }
    }
}
