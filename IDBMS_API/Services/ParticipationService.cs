using BusinessObject.DTOs.Request;
using BusinessObject.Models;
using IDBMS_API.Constants;
using Repository.Interfaces;
using System.Text.RegularExpressions;

namespace IDBMS_API.Services
{
    public class ParticipationService
    {
        private readonly IParticipationRepository _repository;
        public ParticipationService(IParticipationRepository repository)
        {
            _repository = repository;
        }
        public IEnumerable<Participation> GetAll()
        {
            return _repository.GetAll();
        }
        public Participation? GetById(Guid id)
        {
            return _repository.GetById(id);
        }
        public IEnumerable<Participation> GetByUserId(Guid id)
        {
            return _repository.GetByUserId(id);
        }
        public IEnumerable<Participation> GetByProjectId(Guid id)
        {
            return _repository.GetByProjectId(id);
        }
        public Participation? CreateParticipation(ParticipationRequest request)
        {
            var p = new Participation
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
        public void UpdateParticipation(Guid id, ParticipationRequest request)
        {
            var p = _repository.GetById(id) ?? throw new Exception("This object is not existed!");

            p.UserId = request.UserId;
            p.ProjectId = request.ProjectId;
            p.Role = request.Role;
            p.IsDeleted = request.IsDeleted;

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
