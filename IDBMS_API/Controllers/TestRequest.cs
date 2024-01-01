using Microsoft.AspNetCore.Mvc;

namespace IDBMS_API.Controllers
{
    public class TestRequest
    {
        public string name { get; set; }
        public IFormFile file { get; set; }
    }
}
