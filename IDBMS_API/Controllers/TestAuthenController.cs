using API.Supporters.JwtAuthSupport;
using BLL.Services;
using IDBMS_API.DTOs.Request;
using BusinessObject.Models;
using Firebase.Storage;
using IDBMS_API.DTOs.Request.AccountRequest;
using IDBMS_API.Supporters.File;
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
        public async Task<ActionResult<User>> Register(CreateUserRequest request)
        {
            PasswordUtils.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            var user = new User();
            //user.Username = request.Username;
            //user.PasswordHash = passwordHash;
            //user.PasswordSalt = passwordSalt;

            return Ok(user);
        }
        [HttpPost("file")]
        public async Task<IActionResult> IndexAsync([FromForm] IFormFile imageFile)
        {
            if (imageFile != null && imageFile.Length != 0)
            {

                FirebaseService s = new FirebaseService();
                string filename = await s.UploadImage(imageFile);

                //byte[] file = await s.DownloadFile(filename);
                //byte[] file2 = FileSupporter.GenFileBytes(file);
                //string name = await s.UploadByByte(file2,"nam.docx");
                return Ok();
            }
            return Ok("false");
        }
        [HttpGet("case1")]
        [Authorize]
        public IActionResult Case1()
        {
            return Ok("success");
        }
        [HttpGet("case2")]
        [Authorize(Policy = "ParticipationAccess")]
        public IActionResult Case2(string? id)
        {
            return Ok("success");
        }
    }
}
