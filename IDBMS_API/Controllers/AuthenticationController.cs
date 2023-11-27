
using API.Services;
using API.Supporters.JwtAuthSupport;
using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using IDBMS_API.Supporters.Utils;
using IDBMS_API.Services;
using Azure;
using DocumentFormat.OpenXml.Spreadsheet;
using Azure.Core;
using DocumentFormat.OpenXml.Office2016.Excel;
using IDBMS_API.DTOs.Request.AccountRequest;
using IDBMS_API.DTOs.Response;

namespace API.Controllers
{
    [Route("api/[controller]")]
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
            try
            {
                var (token, user) = userService.Login(request.Email, request.Password);
                var response = new ResponseMessage();
                if (token == null)
                {
                    response.Message = "Incorrect email or password!";
                    return BadRequest(response);
                }

                response.Message = "Login successfully!";
                response.Data = new
                {
                    Token = token,
                    user.Name,
                    user.Id,
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new ResponseMessage()
                {
                    Message = $"Error: {ex.Message}"
                };
                return BadRequest(response);
            }
        }
        [HttpPut("logout")]
        [Authorize]
        public IActionResult Logout()
        {
            try
            {
                var response = new ResponseMessage();
                var user = (User?)HttpContext.Items["User"];
                if (user != null)
                {
                    //userService.Logout(user);
                    response.Message = "Logout successfully!" ;
                    return Ok(response);
                }
                response.Message = "Cannot logout, user not existed";
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                var response = new ResponseMessage()
                {
                    Message = $"Error: {ex.Message}"
                };
                return BadRequest(response);
            }
        }
        [HttpPost("register")]
        public IActionResult Register(CreateUserRequest request)
        {
            try
            {
                var user = userService.CreateUser(request);
                var response = new ResponseMessage()
                {
                    Message = "Update successfully!",
                };
                return Login(new LoginRequest() { Email = request.Email, Password = request.Password });
            }
            catch (Exception ex)
            {
                var response = new ResponseMessage()
                {
                    Message = $"Error: {ex.Message}"
                };
                return BadRequest(response);
            }
        }

        [HttpPut("password")]
        public IActionResult UpdateUserPassword(UpdatePasswordRequest request)
        {
            try
            {
                userService.UpdateUserPassword(request);
                var response = new ResponseMessage()
                {
                    Message = "Update successfully!",
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new ResponseMessage()
                {
                    Message = $"Error: {ex.Message}"
                };
                return BadRequest(response);
            }
        }

        [HttpPost("verify")]
        public IActionResult Verify(string email)
        {
            try
            {
                var result = authenticationCodeService.SendActivationEmail(email);
                var response = new ResponseMessage()
                {
                    Message = "Send successfully!",
                    Data= result
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new ResponseMessage()
                {
                    Message = $"Error: {ex.Message}"
                };
                return BadRequest(response);
            }

        }
    }
}
