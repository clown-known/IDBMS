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
            var role = (string?)context.HttpContext.Items["role"];
            
            if (user == null && role == null)
            {
                context.Result = new JsonResult(new { Message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
            if (role == null || !role.Equals("admin"))
            {
                if (Policy != null && Policy == "ParticipationAccess")
                {
                    var routeData = context.HttpContext.GetRouteData();
                    if (routeData != null)
                    {

                        var id = context.HttpContext.Request.Query["projectId"].ToString();

                        Guid.TryParse(id, out Guid pid);

                        bool accepted = _participationRepository.GetByProjectId(pid).Where(p => p.UserId == user.Id) != null;

                        if (!accepted)
                            context.Result = new UnauthorizedResult();

                        new JsonResult(
                                new { Message = "Unauthorized" })
                        {
                            StatusCode = StatusCodes.Status401Unauthorized
                        };
                    }
                }
                if (Policy != null && Policy == "Owner")
                {
                    var routeData = context.HttpContext.GetRouteData();
                    if (routeData != null)
                    {

                        var id = context.HttpContext.Request.Query["projectId"].ToString();

                        Guid.TryParse(id, out Guid pid);

                        bool accepted = _participationRepository.GetByProjectId(pid).Where(p => p.UserId == user.Id
                                                && p.Role == BusinessObject.Enums.ParticipationRole.ProductOwner) != null;

                        if (!accepted)
                            context.Result = new JsonResult(
                                new { Message = "Unauthorized" })
                            {
                                StatusCode = StatusCodes.Status401Unauthorized
                            };
                    }
                }
                if (Policy != null && Policy == "ArchitechOfProject")
                {
                    var routeData = context.HttpContext.GetRouteData();
                    if (routeData != null)
                    {

                        var id = context.HttpContext.Request.Query["projectId"].ToString();

                        Guid.TryParse(id, out Guid pid);

                        bool accepted = _participationRepository.GetByProjectId(pid).Where(p => p.UserId == user.Id
                                                && p.Role == BusinessObject.Enums.ParticipationRole.Architect) != null;

                        if (!accepted)
                            context.Result = new JsonResult(
                                new { Message = "Unauthorized" })
                            {
                                StatusCode = StatusCodes.Status401Unauthorized
                            };
                    }
                }
                if (Policy != null && Policy == "LeadArchitechOfProject")
                {
                    var routeData = context.HttpContext.GetRouteData();
                    if (routeData != null)
                    {

                        var id = context.HttpContext.Request.Query["projectId"].ToString();

                        Guid.TryParse(id, out Guid pid);

                        bool accepted = _participationRepository.GetByProjectId(pid).Where(p => p.UserId == user.Id
                                                && p.Role == BusinessObject.Enums.ParticipationRole.LeadArchitect) != null;

                        if (!accepted)
                            context.Result = new JsonResult(
                                new { Message = "Unauthorized" })
                            {
                                StatusCode = StatusCodes.Status401Unauthorized
                            };
                    }
                }
                if (Policy != null && Policy == "ConstructionManagerOfProject")
                {
                    var routeData = context.HttpContext.GetRouteData();
                    if (routeData != null)
                    {

                        var id = context.HttpContext.Request.Query["projectId"].ToString();

                        Guid.TryParse(id, out Guid pid);

                        bool accepted = _participationRepository.GetByProjectId(pid).Where(p => p.UserId == user.Id
                                                && p.Role == BusinessObject.Enums.ParticipationRole.ConstructionManager) != null;

                        if (!accepted)
                            context.Result = new JsonResult(
                                new { Message = "Unauthorized" })
                            {
                                StatusCode = StatusCodes.Status401Unauthorized
                            };
                    }
                }
                if (Policy != null && Policy == "Architect")
                {
                    bool accepted = user.UserRoles.Where(r => r.Role == BusinessObject.Enums.CompanyRole.Architect) != null;
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
}
