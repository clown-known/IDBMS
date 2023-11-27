using IDBMS_API.DTOs.Request;
using BusinessObject.Models;
using IDBMS_API.Constants;
using Repository.Interfaces;
using System.Text.RegularExpressions;

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

        public void UpdateParticipation(Guid id, ProjectParticipationRequest request)
        {
            var p = _repository.GetById(id) ?? throw new Exception("This object is not existed!");

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
