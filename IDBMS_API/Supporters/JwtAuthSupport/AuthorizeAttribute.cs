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
        private TaskAssignmentRepository _taskAssignmentRepository;
        public AuthorizeAttribute()
        {
            _participationRepository = new ProjectParticipationRepository();
            _taskAssignmentRepository = new TaskAssignmentRepository();
            if (Policy == null) Policy = "";
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = (User?)context.HttpContext.Items["User"];
            var role = (string?)context.HttpContext.Items["role"];
            
            if (user == null && role == null)
            {
                context.Result = new JsonResult(new { Message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
            else if (role == null || !role.Equals("admin"))
            {
                List<string> policy = null;
                if(!string.IsNullOrEmpty(Policy)) policy = Policy.Split(",").ToList();
                bool accept = false;
                if(policy != null)
                {
                    var routeData = context.HttpContext.GetRouteData();
                    if (routeData != null)
                    {

                        var id = context.HttpContext.Request.Query["projectId"].ToString();

                        Guid.TryParse(id, out Guid pid);

                        foreach (var p in policy)
                        {
                            string s = p.Trim().ToLower();
                            switch (s)
                            {
                                case "user":
                                    accept = user != null;
                                    break;
                                case "participation":
                                    accept = _participationRepository.GetByProjectId(pid).Where(p => p.UserId == user.Id).FirstOrDefault() != null;
                                    break;
                                case "projectmanager":
                                    accept = _participationRepository.GetByProjectId(pid).Where(p => p.UserId == user.Id
                                                            && p.Role == BusinessObject.Enums.ParticipationRole.ProjectManager).FirstOrDefault() != null;
                                    break;
                                case "architect":
                                    accept = _participationRepository.GetByProjectId(pid).Where(p => p.UserId == user.Id
                                                            && p.Role == BusinessObject.Enums.ParticipationRole.Architect).FirstOrDefault() != null;
                                    break;
                                case "constructionmanager":
                                    accept = _taskAssignmentRepository.GetByProjectId(pid).Where(p => p.ProjectParticipation.UserId == user.Id
                                                            && p.ProjectParticipation.Role == BusinessObject.Enums.ParticipationRole.ConstructionManager).FirstOrDefault() != null;
                                    break;
                                case "owner":
                                    accept = _participationRepository.GetByProjectId(pid).Where(p => p.UserId == user.Id
                                                && p.Role == BusinessObject.Enums.ParticipationRole.ProductOwner).FirstOrDefault() != null;
                                    break;
                                case "viewer":
                                    accept = _participationRepository.GetByProjectId(pid).Where(p => p.UserId == user.Id
                                                && p.Role == BusinessObject.Enums.ParticipationRole.Viewer).FirstOrDefault() != null;
                                    break;
                            }

                            if (accept)
                                return;
                        };

                        if (!accept)
                            context.Result = new JsonResult(new { Message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
                    }
                }
                
            }
        }
    }
}
