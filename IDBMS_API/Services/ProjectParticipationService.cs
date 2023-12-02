using IDBMS_API.DTOs.Request;
using BusinessObject.Models;
using IDBMS_API.Constants;
using Repository.Interfaces;
using System.Text.RegularExpressions;
using BusinessObject.Enums;
using Microsoft.AspNetCore.Mvc;

namespace IDBMS_API.Services
{
    public class ProjectParticipationService
    {
        private readonly IProjectParticipationRepository _repository;
        public ProjectParticipationService(IProjectParticipationRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<ProjectParticipation> GetAll()
        {
            return _repository.GetAll();
        }

        public IEnumerable<ProjectParticipation> GetByUserId(Guid id)
        {
            return _repository.GetByUserId(id);
        }

        public IEnumerable<ProjectParticipation> GetByProjectId(Guid id)
        {
            return _repository.GetByProjectId(id);
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
