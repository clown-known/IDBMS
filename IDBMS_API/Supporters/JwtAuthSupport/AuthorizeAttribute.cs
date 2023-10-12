using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Repository.Implements;
using Repository.Interfaces;

namespace API.Supporters.JwtAuthSupport
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public string Policy { get; set; }
        private ParticipationRepository _participationRepository;
        public AuthorizeAttribute() { }
        public AuthorizeAttribute(Guid projectid)
        {
            _participationRepository = new ParticipationRepository();
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = (User?)context.HttpContext.Items["User"];
            
            if (user == null)
            {
                context.Result = new JsonResult(new { Message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
            if(Policy != null && Policy == "ParticipationAccess")
            {
                var routeData = context.HttpContext.GetRouteData();
                if (routeData != null)
                {

                    var id = context.HttpContext.Request.Query["id"].ToString();
                    if (id != null)
                        context.Result = new JsonResult(new { Message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };

                }
                //context.Result = new JsonResult(new { Message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            } 
        }
    }
}
