using IDBMS_API.DTOs.Request;
using BusinessObject.Models;
using IDBMS_API.Constants;
using Repository.Interfaces;
using System.Text.RegularExpressions;
using BusinessObject.Enums;
using Microsoft.AspNetCore.Mvc;
using UnidecodeSharpFork;

namespace IDBMS_API.Services
{
    public class ProjectParticipationService
    {
        private readonly IProjectParticipationRepository _participationRepo;
        private readonly ITaskAssignmentRepository _assignmentRepo;
        public ProjectParticipationService(IProjectParticipationRepository participationRepo, ITaskAssignmentRepository assignmentRepo)
        {
            _participationRepo = participationRepo;
            _assignmentRepo = assignmentRepo;
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

        public IEnumerable<ProjectParticipation> GetByUserId(Guid id, ParticipationRole? role, string? name, ProjectStatus? projectStatus, string? projectName)
        {
            var list = _participationRepo.GetByUserId(id);

            return Filter(list, role, name, projectStatus, projectName);
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

        public ProjectParticipation? CreateParticipation(ProjectParticipationRequest request)
        {
            var p = new ProjectParticipation
            {
                Id = Guid.NewGuid(),
                UserId = request.UserId,
                ProjectId = request.ProjectId,
                Role = request.Role,
                IsDeleted = false,
            };

            var pCreated = _participationRepo.Save(p);
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
            assignmentService.DeleteTaskAssignmentByParticipationId(id);
        }
    }
}
