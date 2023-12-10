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
        private readonly IProjectParticipationRepository _repository;
        public ProjectParticipationService(IProjectParticipationRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<ProjectParticipation> Filter(IEnumerable<ProjectParticipation> list,
            ParticipationRole? role, string? name)
        {
            IEnumerable<ProjectParticipation> filteredList = list;
            
            if (role != null)
            {
                filteredList = filteredList.Where(item => item.Role == role);
            }

            if (name != null)
            {
                filteredList = filteredList.Where(item => (item.User.Name != null && item.User.Name.Unidecode().IndexOf(name.Unidecode(), StringComparison.OrdinalIgnoreCase) >= 0));
            }

            return filteredList;
        }

        public IEnumerable<ProjectParticipation> GetAll(ParticipationRole? role, string? username)
        {
            var list = _repository.GetAll();

            return Filter(list, role, username);
        }

        public IEnumerable<ProjectParticipation> GetByUserId(Guid id, ParticipationRole? role, string? username)
        {
            var list = _repository.GetByUserId(id);

            return Filter(list, role, username);
        }

        public IEnumerable<ProjectParticipation> GetByProjectId(Guid id, ParticipationRole? role, string? username)
        {
            var list = _repository.GetByProjectId(id);

            return Filter(list, role, username);
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

            var pCreated = _repository.Save(p);
            return pCreated;
        }

        public void CreateParticipationsByRole(CreateParticipationListRequest request)
        {
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

                var pCreated = _repository.Save(p);
            }
        }
        
        public void UpdateParticipation(ProjectParticipationRequest request)
        {
            var p = _repository.GetById(request.ProjectId) ?? throw new Exception("This object is not existed!");

            p.UserId = request.UserId;
            p.ProjectId = request.ProjectId;
            p.Role = request.Role;

            _repository.Update(p);
        }

        public void DeleteParticipation(Guid id)
        {

            var p = _repository.GetById(id) ?? throw new Exception("This object is not existed!");

            p.IsDeleted = true;

            _repository.Update(p);
        }
    }
}
