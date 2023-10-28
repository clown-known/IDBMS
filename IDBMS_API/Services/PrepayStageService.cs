using BusinessObject.DTOs.Request;
using BusinessObject.Models;
using Repository.Interfaces;

namespace IDBMS_API.Services
{
    public class PrepayStageService
    {
        private readonly IPrepayStageRepository _repository;
        public PrepayStageService(IPrepayStageRepository repository)
        {
            _repository = repository;
        }
        public IEnumerable<PrepayStage> GetAll()
        {
            return _repository.GetAll();
        }
        public PrepayStage? GetById(Guid id)
        {
            return _repository.GetById(id) ?? throw new Exception("This object is not existed!");
        }
        public IEnumerable<PrepayStage?> GetByProjectId(Guid projectId)
        {
            return _repository.GetByProjectId(projectId) ?? throw new Exception("This object is not existed!");
        }
        public PrepayStage? CreatePrepayStage(PrepayStageRequest request)
        {
            var ps = new PrepayStage
            {
                Id = Guid.NewGuid(),
                StageNo = request.StageNo,
                Name = request.Name,
                Description = request.Description,
                IsPaid = request.IsPaid,
                TotalPaid = request.TotalPaid,
                IsPrepaid = request.IsPrepaid,
                PricePercentage = request.PricePercentage,
                StartedDate = request.StartedDate,
                EndDate = request.EndDate,
                ProjectId = request.ProjectId,
                IsHidden = false,
            };

            var psCreated = _repository.Save(ps);
            return psCreated;
        }
        public void UpdatePrepayStage(Guid id, PrepayStageRequest request)
        {
            var ps = _repository.GetById(id) ?? throw new Exception("This object is not existed!");

            ps.StageNo = request.StageNo;
            ps.Name = request.Name;
            ps.Description = request.Description;
            ps.IsPaid = request.IsPaid;
            ps.TotalPaid = request.TotalPaid;
            ps.IsPrepaid = request.IsPrepaid;
            ps.PricePercentage = request.PricePercentage;
            ps.StartedDate = request.StartedDate;
            ps.EndDate = request.EndDate;
            ps.ProjectId = request.ProjectId;
            
            _repository.Update(ps);
        }
        public void UpdatePrepayStageStatus(Guid id, bool isHidden)
        {
            var ps = _repository.GetById(id) ?? throw new Exception("This object is not existed!");

            ps.IsHidden = isHidden;

            _repository.Update(ps);
        }
    }
}
