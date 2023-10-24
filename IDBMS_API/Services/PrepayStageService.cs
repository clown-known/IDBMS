using BusinessObject.DTOs.Request.CreateRequests;
using BusinessObject.Models;
using Repository.Interfaces;

namespace IDBMS_API.Services
{
    public class PrepayStageService
    {
        private readonly IPrepayStageRepository _prepayStageRepository;
        public PrepayStageService(IPrepayStageRepository prepayStageRepository)
        {
            _prepayStageRepository = prepayStageRepository;
        }
        public IEnumerable<PrepayStage> GetAll()
        {
            return _prepayStageRepository.GetAll();
        }
        public PrepayStage? GetById(Guid id)
        {
            return _prepayStageRepository.GetById(id);
        }
        public async Task<PrepayStage?> CreatePrepayStage(PrepayStageRequest request)
        {
            var ps = new PrepayStage
            {
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
                IsHidden = request.IsHidden,
            };
            var psCreated = _prepayStageRepository.Save(ps);
            return psCreated;
        }
        public async Task UpdatePrepayStage(PrepayStageRequest request)
        {
            var ps = new PrepayStage
            {
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
                IsHidden = request.IsHidden,
            };
            _prepayStageRepository.Update(ps);
        }
        public async Task DeletePrepayStage(Guid id)
        {
            _prepayStageRepository.DeleteById(id);
        }
    }
}
