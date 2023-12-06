using Azure.Core;
using IDBMS_API.DTOs.Request;
using BusinessObject.Models;
using DocumentFormat.OpenXml.Office2016.Excel;
using Repository.Interfaces;
using DocumentFormat.OpenXml.Office2010.Excel;
using BusinessObject.Enums;

namespace IDBMS_API.Services
{
    public class PaymentStageService
    {
        private readonly IPaymentStageRepository _stageRepo;
        private readonly IProjectRepository _projectRepo;
        private readonly IProjectDesignRepository _projectDesignRepo;
        private readonly IPaymentStageDesignRepository _stageDesignRepo;

        public PaymentStageService(
            IPaymentStageRepository stageRepo, 
            IProjectRepository projectRepo, 
            IProjectDesignRepository projectDesignRepo, 
            IPaymentStageDesignRepository stageDesignRepo)
        {
            _stageRepo = stageRepo;
            _projectRepo = projectRepo;
            _projectDesignRepo = projectDesignRepo;
            _stageDesignRepo = stageDesignRepo;
        }
        public IEnumerable<PaymentStage> GetAll()
        {
            return _stageRepo.GetAll();
        }
        public PaymentStage? GetById(Guid id)
        {
            return _stageRepo.GetById(id) ?? throw new Exception("This object is not existed!");
        }
        public IEnumerable<PaymentStage?> GetByProjectId(Guid projectId)
        {
            return _stageRepo.GetByProjectId(projectId) ?? throw new Exception("This object is not existed!");
        }
        public PaymentStage? CreatePaymentStage(PaymentStageRequest request)
        {
            var ps = new PaymentStage
            {
                Id = Guid.NewGuid(),
                StageNo = request.StageNo, //danh so +1
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
                ProjectId = request.ProjectId,
                IsHidden = request.IsHidden,
                Status = request.Status,
                EstimateBusinessDay = 0,
            };

            var psCreated = _stageRepo.Save(ps);
            return psCreated;
        }

        public void CreatePaymentStagesByProjectDesign(Guid projectId)
        {
            var project = _projectRepo.GetById(projectId) ?? throw new Exception("This object is not existed!");

            ProjectDesignService pjDesignService = new ProjectDesignService(_projectDesignRepo);
            var projectDesigns = pjDesignService.GetByType(project.Type);

            var selectedDesign = projectDesigns.FirstOrDefault(design =>
                project.EstimatedPrice >= design.MinBudget &&
                project.EstimatedPrice <= design.MaxBudget);

            if (selectedDesign == null)
            {
                throw new Exception("No suitable project design found for the given budget range.");
            }

            PaymentStageDesignService pmDesignService = new PaymentStageDesignService(_stageDesignRepo);
            var pmDesigns = pmDesignService.GetByProjectDesignId(selectedDesign.Id);

            foreach (var stage in pmDesigns)
            {
                var ps = new PaymentStage
                {
                    Id = Guid.NewGuid(),
                    StageNo = stage.StageNo,
                    Name = stage.Name,
                    Description = stage.Description,
                    IsPaid = false,
                    TotalContractPaid = (decimal)(stage.PricePercentage / 100) * (decimal)project.EstimatedPrice,
                    IsPrepaid = stage.IsPrepaid,
                    PricePercentage = stage.PricePercentage,
                    ProjectId = projectId,
                    IsHidden = false,
                    Status = StageStatus.Unopen
                };

                _stageRepo.Save(ps);
            }
        }


        public void UpdatePaymentStage(Guid id, PaymentStageRequest request)
        {
            var ps = _stageRepo.GetById(id) ?? throw new Exception("This object is not existed!");

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
            ps.ProjectId = request.ProjectId;
            ps.IsHidden = request.IsHidden;
            ps.Status = request.Status;
            
            _stageRepo.Update(ps);
        }
        public void UpdatePaymentStageStatus(Guid id, bool isHidden)
        {
            var ps = _stageRepo.GetById(id) ?? throw new Exception("This object is not existed!");

            ps.IsHidden = isHidden;

            _stageRepo.Update(ps);
        }

        public void UpdateStagesDataByTask(Guid id, int estimateBusinessDay)
        {
            var ps = _stageRepo.GetById(id) ?? throw new Exception("This object is not existed!");

            ps.EstimateBusinessDay = estimateBusinessDay;

            _stageRepo.Update(ps);
        }
    }
}
