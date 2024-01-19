using IDBMS_API.DTOs.Request;
using BusinessObject.Models;
using IDBMS_API.Constants;
using Repository.Interfaces;
using System.Text.RegularExpressions;
using BusinessObject.Enums;
using Microsoft.AspNetCore.Mvc;
using UnidecodeSharpFork;
using IDBMS_API.Supporters.UserHelper;
using Repository.Implements;
using IDBMS_API.Supporters.EmailSupporter;
using DocumentFormat.OpenXml.Spreadsheet;

namespace IDBMS_API.Services
{
    public class ProjectParticipationService
    {
        private readonly IProjectParticipationRepository _participationRepo;
        private readonly IProjectRepository _projectRepository;
        private readonly ITaskAssignmentRepository _assignmentRepo;
        private readonly IUserRepository _userRepo;
        private readonly IConfiguration configuration;
        public ProjectParticipationService(IProjectParticipationRepository participationRepo, ITaskAssignmentRepository assignmentRepo,IUserRepository userRepository, IProjectRepository projectRepository, IConfiguration configuration)
        {
            _participationRepo = participationRepo;
            _assignmentRepo = assignmentRepo;
            _userRepo = userRepository;
            _projectRepository = projectRepository;
            this.configuration = configuration;
        }

        public IEnumerable<ProjectParticipation> Filter(IEnumerable<ProjectParticipation> list,
            ParticipationRole? role, string? userName, ProjectStatus? projectStatus, string? projectName)
        {
            IEnumerable<ProjectParticipation> filteredList = list;
            
            if (role != null)
            {
                filteredList = filteredList.Where(item => item.Role == role);
            }

            if (userName != null)
            {
                filteredList = filteredList.Where(item => (item.User.Name != null && item.User.Name.Unidecode().IndexOf(userName.Unidecode(), StringComparison.OrdinalIgnoreCase) >= 0));
            }

            if (projectStatus != null)
            {
                filteredList = filteredList.Where(item => item.Project.Status == projectStatus);
            }

            if (projectName != null)
            {
                filteredList = filteredList.Where(item => (item.Project.Name != null && item.Project.Name.Unidecode().IndexOf(projectName.Unidecode(), StringComparison.OrdinalIgnoreCase) >= 0));
            }

            return filteredList;
        }

        public IEnumerable<ProjectParticipation> GetAll(ParticipationRole? role, string? name, ProjectStatus? projectStatus, string? projectName)
        {
            var list = _participationRepo.GetAll();

            return Filter(list, role, name, projectStatus, projectName);
        }

        public IEnumerable<ProjectParticipation> GetByUserId(Guid id, ParticipationRole? role, ProjectStatus? projectStatus, string? projectName)
        {
            var list = _participationRepo.GetByUserId(id);

            return Filter(list, role, null, projectStatus, projectName);
        }

        public IEnumerable<ProjectParticipation> GetUsersByParticipationInProject(Guid projectId)
        {
            var list = _participationRepo.GetUsersByParticipationInProject(projectId);

            return list;
        }

        public IEnumerable<ProjectParticipation> GetProjectMemberByProjectId(Guid id, ParticipationRole? role, string? name)
        {
            var list = _participationRepo.GetProjectMemberByProjectId(id);

            return Filter(list, role, name, null, null);
        }

        public ProjectParticipation? GetProjectManagerByProjectId(Guid id)
        {
            return _participationRepo.GetProjectManagerByProjectId(id);
        }

        public ProjectParticipation? GetProjectOwnerByProjectId(Guid id)
        {
            return _participationRepo.GetProjectOwnerByProjectId(id);
        }

        public ProjectParticipation? GetParticpationInProjectByUserId(Guid userId, Guid projectId)
        {
            return _participationRepo.GetParticpationInProjectByUserId(userId, projectId);
        }

        public IEnumerable<ProjectParticipation> GetCustomerViewersByProjectId(Guid projectId, string? name)
        {
            var list = _participationRepo.GetCustomerViewersByProjectId(projectId);
            return Filter(list, null, name, null, null);
        }

        public ProjectParticipation? CreateParticipation(ProjectParticipationRequest request)
        {
            var project = _projectRepository.GetById(request.ProjectId);

            if (project == null)
                throw new Exception("Project not found!");

            if (project.BasedOnDecorProjectId!=null)
            {
                var dproject = _projectRepository.GetById(project.BasedOnDecorProjectId.Value);

                if (dproject == null)
                    throw new Exception("Cannot find project based on");

                bool checkUserExist = dproject.ProjectParticipations.Where(p => p.UserId == request.UserId).FirstOrDefault() == null;

                if (checkUserExist)
                {
                    var user = _userRepo.GetById(request.UserId);

                    if (user == null)
                        throw new Exception("User not found!");

                    if (user.Role != CompanyRole.Customer)
                    {
                        var dp = new ProjectParticipation
                        {
                            Id = Guid.NewGuid(),
                            UserId = request.UserId,
                            ProjectId = dproject.Id,
                            Role = ParticipationRole.Viewer,
                            IsDeleted = false,
                        };

                        _participationRepo.Save(dp);
                    }
                }
            }
            var p = new ProjectParticipation
            {
                Id = Guid.NewGuid(),
                UserId = request.UserId,
                ProjectId = request.ProjectId,
                Role = request.Role,
                IsDeleted = false,
            };

            var pCreated = _participationRepo.Save(p);
            string link = configuration["Server:Frontend"] + "/project/" + request.ProjectId.ToString();
            var u = _userRepo.GetById(request.UserId);
            EmailSupporter.SendInviteEnglishEmail(u.Email, link,u.Language== Language.English);

            return pCreated;
        }
        public ProjectParticipation? AddViewer(Guid projectId,string email)
        {
            User? user = _userRepo.GetByEmail(email);
            string link = configuration["Server:Frontend"]+"/project/" + projectId.ToString();
            if (user == null)
            {
                (user, string password) = UserHelper.GennarateViewerUserForProject(projectId,email);
                EmailSupporter.SendInviteEnglishEmail(email, link, password);

            }
            else
            {
                // send email
                EmailSupporter.SendInviteEnglishEmail(email, link,user.Language==Language.English);
            }
            var pCreated = _participationRepo.Save(new ProjectParticipation
            {
                IsDeleted = false,
                ProjectId = projectId,
                UserId = user.Id,
                Role = BusinessObject.Enums.ParticipationRole.Viewer
            });
            
            //
            return pCreated;
        }

        public List<ProjectParticipation?> CreateParticipationsByRole(CreateParticipationListRequest request)
        {
            List<ProjectParticipation?> list = new List<ProjectParticipation?>();
            if (request.Role == ParticipationRole.ProjectManager || request.Role == ParticipationRole.ProductOwner)
                throw new Exception("Only creating 1 Project Manager or 1 Project Owner!");
            foreach (var userId in request.ListUserId)
            {
                var p = new ProjectParticipation
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    ProjectId = request.ProjectId,
                    Role = request.Role,
                    IsDeleted = false,
                };

                var pCreated = _participationRepo.Save(p);
                list.Add(pCreated);
            }
            return list;
        }
        
        public void UpdateParticipation(Guid id, ProjectParticipationRequest request)
        {
            var p = _participationRepo.GetById(id) ?? throw new Exception("This project participant id is not existed!");

            p.UserId = request.UserId;
            p.Role = request.Role;

            _participationRepo.Update(p);
        }

        public void DeleteParticipation(Guid id)
        {
            var p = _participationRepo.GetById(id) ?? throw new Exception("This project participant id is not existed!");

            p.IsDeleted = true;

            _participationRepo.Update(p);

            TaskAssignmentService assignmentService = new TaskAssignmentService(_assignmentRepo);
            assignmentService.DeleteTaskAssignmentByUserId(p.UserId, p.ProjectId);
        }
    }
}
