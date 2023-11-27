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
        private ProjectParticipationRepository _participationRepository;
        public AuthorizeAttribute()
        {
            _participationRepository = new ProjectParticipationRepository();
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
                   
                    Guid.TryParse(id, out Guid pid);

                    bool accepted =  _participationRepository.GetByProjectId(pid) != null;

                    if (!accepted)
                        context.Result = new UnauthorizedResult();

                    new JsonResult(
                            new { Message = "Unauthorized" }) { 
                                StatusCode = StatusCodes.Status401Unauthorized 
                        };

                }
                //context.Result = new JsonResult(new { Message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            } 
            if(Policy != null && Policy == "Architect")
            {
                bool accepted = user.UserRoles.Where(r=>r.Role == BusinessObject.Enums.CompanyRole.Architect) != null;
                if (!accepted)
                    context.Result = new JsonResult(new { Message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };

            }
            if (Policy != null && Policy == "ConstructionManager")
            {
                bool accepted = user.UserRoles.Where(r => r.Role == BusinessObject.Enums.CompanyRole.ConstructionManager) != null;
                if (!accepted)
                    context.Result = new JsonResult(new { Message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };

            }
            if (Policy != null && Policy == "ParticipationIgnoreViewerAccess")
            {
                bool accepted = user.UserRoles.Where(r => r.Role == BusinessObject.Enums.CompanyRole.ConstructionManager) != null;
                if (!accepted)
                    context.Result = new JsonResult(new { Message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };

            }
        }
    }
}
