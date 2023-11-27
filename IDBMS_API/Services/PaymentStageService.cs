using Azure.Core;
using BusinessObject.DTOs.Request;
using BusinessObject.Models;
using DocumentFormat.OpenXml.Office2016.Excel;
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
                TotalContractPaid = request.TotalContractPaid,
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
        public void CreatePaymentStageByDesign(Guid projectId, decimal EstimatedPrice, List<PaymentStageDesign> listStageDesigns)
        {
            foreach (var stage in listStageDesigns)
            {
                var ps = new PaymentStage
                {
                    Id = Guid.NewGuid(),
                    StageNo = stage.StageNo, // Assuming StageNo is a property of PaymentStageDesign
                    Name = stage.Name,
                    Description = stage.Description,
                    IsPaid = false,
                    TotalContractPaid = (decimal)(stage.PricePercentage / 100) * EstimatedPrice,
                    IsPrepaid = stage.IsPrepaid,
                    PricePercentage = stage.PricePercentage,
                    EstimateBusinessDay = stage.EstimateBusinessDay,
                    ProjectId = projectId,
                    IsHidden = false,
                };

                _repository.Save(ps);
            }
        }
        public void UpdatePaymentStage(Guid id, PaymentStageRequest request)
        {
            var ps = _repository.GetById(id) ?? throw new Exception("This object is not existed!");

            ps.StageNo = request.StageNo;
            ps.Name = request.Name;
            ps.Description = request.Description;
            ps.IsPaid = request.IsPaid;
            ps.TotalContractPaid = request.TotalContractPaid;
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
