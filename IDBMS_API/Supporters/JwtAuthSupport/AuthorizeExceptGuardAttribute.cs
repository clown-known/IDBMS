using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Dynamic;

namespace API.Supporters.JwtAuthSupport
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeExceptGuardAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = (User?) context.HttpContext.Items["User"];
            //if(user == null || user.RoleId == RoleConstant.GUARD)
            //{
            //    context.Result = new JsonResult(new { Message = "Unauthorized!" }) { StatusCode = StatusCodes.Status403Forbidden };
            //}
            // cai nay la check role cua tung project
        }
    }
}
