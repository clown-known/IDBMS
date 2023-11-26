using BusinessObject.DTOs.Request;
using BusinessObject.Models;
using Repository.Interfaces;

namespace IDBMS_API.Services
{
    public class PaymentStageService
    {
        private readonly IPaymentStageRepository _repository;
        public PaymentStageService(IPaymentStageRepository repository)
        {
            _repository = repository;
        }
        public IEnumerable<PaymentStage> GetAll()
        {
            return _repository.GetAll();
        }
        public PaymentStage? GetById(Guid id)
        {
            return _repository.GetById(id) ?? throw new Exception("This object is not existed!");
        }
        public IEnumerable<PaymentStage?> GetByProjectId(Guid projectId)
        {
            return _repository.GetByProjectId(projectId) ?? throw new Exception("This object is not existed!");
        }
        public PaymentStage? CreatePaymentStage(PaymentStageRequest request)
        {
            var ps = new PaymentStage
            {
                Id = Guid.NewGuid(),
                StageNo = request.StageNo,
                Name = request.Name,
                Description = request.Description,
                IsPaid = request.IsPaid,
                TotalPaid = request.TotalPaid,
                IsPrepaid = request.IsPrepaid,
                PricePercentage = request.PricePercentage,
                PaidDate = request.PaidDate,
                StartedDate = request.StartedDate,
                EndDate = request.EndDate,
                EndTimePayment = request.EndTimePayment,
                PenaltyFee = request.PenaltyFee,
                EstimateBusinessDay = request.EstimateBusinessDay,
                ProjectId = request.ProjectId,
                IsHidden = false,
            };

            var psCreated = _repository.Save(ps);
            return psCreated;
        }
        public void UpdatePaymentStage(Guid id, PaymentStageRequest request)
        {
            var ps = _repository.GetById(id) ?? throw new Exception("This object is not existed!");

            ps.StageNo = request.StageNo;
            ps.Name = request.Name;
            ps.Description = request.Description;
            ps.IsPaid = request.IsPaid;
            ps.TotalPaid = request.TotalPaid;
            ps.PaidDate = request.PaidDate;
            ps.IsPrepaid = request.IsPrepaid;
            ps.PricePercentage = request.PricePercentage;
            ps.StartedDate = request.StartedDate;
            ps.EndDate = request.EndDate;
            ps.EndTimePayment = request.EndTimePayment;
            ps.PenaltyFee = request.PenaltyFee;
            ps.EstimateBusinessDay = request.EstimateBusinessDay;
            ps.ProjectId = request.ProjectId;
            
            _repository.Update(ps);
        }
        public void UpdatePaymentStageStatus(Guid id, bool isHidden)
        {
            var ps = _repository.GetById(id) ?? throw new Exception("This object is not existed!");

            ps.IsHidden = isHidden;

            _repository.Update(ps);
        }
    }
}
