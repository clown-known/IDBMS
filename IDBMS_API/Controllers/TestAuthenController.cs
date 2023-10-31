/*using API.Supporters.JwtAuthSupport;
using BusinessObject.DTOs.Request;
using BusinessObject.DTOs.Request.AccountRequest;
using BusinessObject.Models;
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
*/