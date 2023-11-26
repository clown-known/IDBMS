
using API.Services;
using API.Supporters.JwtAuthSupport;
using BusinessObject.DTOs.Request.AccountRequest;
using BusinessObject.Models;
using BusinessObject.DTOs.Response;
using Microsoft.AspNetCore.Mvc;
using IDBMS_API.Supporters.Utils;
using IDBMS_API.Services;

namespace API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserService userService;
        private readonly AuthenticationCodeService authenticationCodeService;

        public AuthenticationController(UserService userService, AuthenticationCodeService authenticationCodeService )
        {
            this.userService = userService;
            this.authenticationCodeService = authenticationCodeService;
        }

        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            ResponseMessage response;
            var (token, user) = userService.Login(request.Email, request.Password);
            if (token == null)
            {
                response = new ResponseMessage() { Message = "Incorrect email or password" };
                return new JsonResult(response) { StatusCode = StatusCodes.Status400BadRequest };
            }
            response = new ResponseMessage() {
                Message = "Login successfully",
                Data = new
                {
                    Token=token,
                    user.Name,
                    user.Id,
                }
            };
            return new JsonResult(response) { StatusCode = StatusCodes.Status200OK };
        }
        [HttpPut("logout")]
        [Authorize]
        public IActionResult Logout()
        {
            ResponseMessage response;
            var user = (User?) HttpContext.Items["User"];
            if (user != null)
            {
                //userService.Logout(user);
                response = new ResponseMessage() { Message = "Logout successfully" };
                return new JsonResult(response) { StatusCode = 200 };
            }
            response = new ResponseMessage() { Message = "Cannot logout, user not existed" };
            return new JsonResult(response) { StatusCode = 400 };
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(CreateUserRequest request)
        {
            User user = userService.CreateUser(request);

            return Login(new LoginRequest() { Email = request.Email,Password = request.Password});
        }
        [HttpPost("verify")]
        public IActionResult Verify(string email)
        {
            
            return Ok(authenticationCodeService.SendActivationEmail(email));
        }
    }
}
