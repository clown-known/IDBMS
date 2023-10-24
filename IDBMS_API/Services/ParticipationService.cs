using BusinessObject.DTOs.Request.CreateRequests;
using BusinessObject.Models;
using Repository.Interfaces;

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
        public Participation? CreateParticipation(ParticipationRequest request)
        {
            var p = new Participation
            {
                UserId = request.UserId,
                ProjectId = request.ProjectId,
                Role = request.Role,
                IsDeleted = request.IsDeleted,
            };
            var pCreated = _repository.Save(p);
            return pCreated;
        }
        public void UpdateParticipation(ParticipationRequest request)
        {
            var p = new Participation
            {
                UserId = request.UserId,
                ProjectId = request.ProjectId,
                Role = request.Role,
                IsDeleted = request.IsDeleted,
            };
            _repository.Update(p);
        }
    }
}
