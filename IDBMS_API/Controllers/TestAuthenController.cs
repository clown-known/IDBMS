using API.Supporters.JwtAuthSupport;
<<<<<<< HEAD
using BusinessObject.Models;
using IDBMS_API.DTOs.Request;
=======
using BLL.Services;
using IDBMS_API.DTOs.Request;
using BusinessObject.Models;
using Firebase.Storage;
using IDBMS_API.DTOs.Request.AccountRequest;
using IDBMS_API.Services;
using IDBMS_API.Supporters.File;
>>>>>>> dev
using IDBMS_API.Supporters.Utils;
using Microsoft.AspNetCore.Mvc;

namespace IDBMS_API.Controllers
{
    [Route("api/testauth")]
    [ApiController]
    public class TestAuthenController : Controller
    {
        public static Guid id = Guid.NewGuid();
        private readonly JwtTokenSupporter jwtTokenSupporter;
        public TestAuthenController(JwtTokenSupporter jwtTokenSupporter)
        {
            this.jwtTokenSupporter = jwtTokenSupporter;
        }
        [HttpGet]
        public IActionResult Login()
        {
            var token = jwtTokenSupporter.CreateToken(new BusinessObject.Models.User
            {
                Id = id
            });
            return Ok(token);
        }
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(CreateAccountRequest request)
        {
            PasswordUtils.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            var user = new User();
            //user.Username = request.Username;
            //user.PasswordHash = passwordHash;
            //user.PasswordSalt = passwordSalt;

            return Ok(user);
        }
<<<<<<< HEAD
        [HttpGet("case1")]
        [Authorize]
        public IActionResult Case1()
        {
            return Ok("success");
        }
        [HttpGet("case2")]
        [Authorize( Policy = "ParticipationAccess")]
        public IActionResult Case2(string? id) { 
=======
        [HttpPost("file")]
        [Authorize(Policy = "owner")]
        public async Task<IActionResult> IndexAsync([FromBody][FromForm] IFormFile id)
        {
            

                FirebaseService s = new FirebaseService();
            //string filename = await s.UploadImage(imageFile);
            var content = await s.DownloadFiletest("c4d07b71-c86d-45a9-9afd-076580bf82ea.jpg");
            //byte[] file = await s.DownloadFile(filename);
            //byte[] file2 = FileSupporter.GenFileBytes(file);
            //string name = await s.UploadByByte(file2,"nam.docx");
            return File(content, "application/octet-stream", "c4d07b71-c86d-45a9-9afd-076580bf82ea.jpg");

            // return Ok("false");
        }
        [HttpPost("file2")]
        public async Task<IActionResult> IndexAsync2(TransactionRequest request)
        {
            FirebaseService s = new FirebaseService();
            string link = await s.UploadTransactionImage(request.TransactionReceiptImage);
            //string name = await s.UploadByByte(file2,"nam.docx");
            //return File(content, "application/octet-stream", "c4d07b71-c86d-45a9-9afd-076580bf82ea.jpg");
            return Ok(link);
        }
        [HttpPost("file3")]
        public async Task<IActionResult> IndexAsync3([FromForm] TestRequest request)
        {
            FirebaseService s = new FirebaseService();
            string link = await s.UploadTransactionImage(request.file);
            //string name = await s.UploadByByte(file2,"nam.docx");
            //return File(content, "application/octet-stream", "c4d07b71-c86d-45a9-9afd-076580bf82ea.jpg");
            return Ok();
        }
        

        [HttpGet("admin")]
        [Authorize(Policy = "Admin")]
        public IActionResult CaseAdmin(Guid projectId)
        {
            return Ok("success");
        }

        [HttpGet("participation")]
        [Authorize(Policy = "Participation")]
        public IActionResult CaseParticipation(Guid projectId)
        {
            return Ok("success");
        }

        [HttpGet("admin_participation")]
        [Authorize(Policy = "Admin, Participation")]
        public IActionResult CaseAdminParticipation(Guid projectId)
        {
            return Ok("success");
        }

        [HttpGet("projectmanager")]
        [Authorize(Policy = "ProjectManager")]
        public IActionResult CaseProjectManager(Guid projectId)
        {
            return Ok("success");
        }

        [HttpGet("architect")]
        [Authorize(Policy = "Architect")]
        public IActionResult CaseArchitect(Guid projectId)
        {
            return Ok("success");
        }

        [HttpGet("constructionmanager")]
        [Authorize(Policy = "ConstructionManager")]
        public IActionResult CaseConstructionManager(Guid projectId)
        {
            return Ok("success");
        }

        [HttpGet("owner")]
        [Authorize(Policy = "Owner")]
        public IActionResult CaseOwner(Guid projectId)
        {
            return Ok("success");
        }

        [HttpGet("viewer")]
        [Authorize(Policy = "Viewer")]
        public IActionResult CaseViewer(Guid projectId)
        {
>>>>>>> dev
            return Ok("success");
        }
    }
}
