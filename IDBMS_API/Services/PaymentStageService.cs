using Azure.Core;
using IDBMS_API.DTOs.Request;
using BusinessObject.Models;
using DocumentFormat.OpenXml.Office2016.Excel;
using Repository.Interfaces;
using DocumentFormat.OpenXml.Office2010.Excel;
using BusinessObject.Enums;
using UnidecodeSharpFork;
using DocumentFormat.OpenXml.Wordprocessing;
using Repository.Implements;
using DocumentFormat.OpenXml.Drawing.Diagrams;

namespace IDBMS_API.Services
{
    public class PaymentStageService
    {
        private readonly IPaymentStageRepository _stageRepo;
        private readonly IProjectRepository _projectRepo;
        private readonly IProjectDesignRepository _projectDesignRepo;
        private readonly IPaymentStageDesignRepository _stageDesignRepo;
        private readonly IProjectTaskRepository _taskRepo;
        private readonly IFloorRepository _floorRepo;
        private readonly IRoomRepository _roomRepo;
        private readonly IRoomTypeRepository _roomTypeRepo;

        public PaymentStageService(
            IPaymentStageRepository stageRepo,
            IProjectRepository projectRepo,
            IProjectDesignRepository projectDesignRepo,
            IPaymentStageDesignRepository stageDesignRepo,
            IProjectTaskRepository taskRepo,
            IFloorRepository floorRepo,
            IRoomRepository roomRepo,
            IRoomTypeRepository roomTypeRepo)
        {
            _stageRepo = stageRepo;
            _projectRepo = projectRepo;
            _projectDesignRepo = projectDesignRepo;
            _stageDesignRepo = stageDesignRepo;
            _taskRepo = taskRepo;
            _floorRepo = floorRepo;
            _roomRepo = roomRepo;
            _roomTypeRepo = roomTypeRepo;
        }

        public IEnumerable<PaymentStage> Filter(IEnumerable<PaymentStage> list,
           StageStatus? status, string? name)
        {
            IEnumerable<PaymentStage> filteredList = list;

            if (status != null)
            {
                filteredList = filteredList.Where(item => item.Status == status);
            }

            if (name != null)
            {
                filteredList = filteredList.Where(item => (item.Name != null && item.Name.Unidecode().IndexOf(name.Unidecode(), StringComparison.OrdinalIgnoreCase) >= 0));
            }

            return filteredList;
        }

        public IEnumerable<PaymentStage> GetAll(StageStatus? status, string? name)
        {
            var list = _stageRepo.GetAll();

            return Filter(list, status, name);
        }
        public PaymentStage? GetById(Guid id)
        {
            return _stageRepo.GetById(id) ?? throw new Exception("This object is not existed!");
        }
        public IEnumerable<PaymentStage> GetByProjectId(Guid projectId, StageStatus? status, string? name)
        {
            var list = _stageRepo.GetByProjectId(projectId) ?? throw new Exception("This object is not existed!");

            return Filter(list, status, name);
        }
        public PaymentStage? CreatePaymentStage(PaymentStageRequest request)
        {
            ProjectService projectService = new(_projectRepo, _roomRepo, _roomTypeRepo, _taskRepo, _stageRepo, _projectDesignRepo, _stageDesignRepo, _floorRepo);
            var project = projectService.GetById(request.ProjectId);

            var ps = new PaymentStage
            {
                Id = Guid.NewGuid(),
                StageNo = 0,
                Name = request.Name,
                Description = request.Description,
                TotalContractPaid = (decimal)(request.PricePercentage / 100) * (decimal)project.EstimatedPrice,
                IsPrepaid = request.IsPrepaid,
                PricePercentage = request.PricePercentage,
                EndTimePayment = request.EndTimePayment,
                ProjectId = request.ProjectId,
                IsDeleted = false,
                Status = StageStatus.Unopen,
                EstimateBusinessDay = 0,
            };

            var psCreated = _stageRepo.Save(ps);

            UpdateStageNoByProjectId(request.ProjectId);

            return psCreated;
        }

        public void CreatePaymentStagesByProjectDesign(Guid projectId)
        {
            ProjectService projectService = new(_projectRepo, _roomRepo, _roomTypeRepo, _taskRepo, _stageRepo, _projectDesignRepo, _stageDesignRepo, _floorRepo);
            var project = projectService.GetById(projectId);

            ProjectDesignService pjDesignService = new ProjectDesignService(_projectDesignRepo);
            var projectDesigns = pjDesignService.GetByType(project.Type);

            var selectedDesign = projectDesigns.FirstOrDefault(design =>
                project.EstimatedPrice >= design.MinBudget &&
                project.EstimatedPrice <= design.MaxBudget);

            if (selectedDesign == null)
            {
                throw new Exception("No suitable project design found for the given budget range!");
            }

            PaymentStageDesignService pmDesignService = new (_stageDesignRepo);
            var pmDesigns = pmDesignService.GetByProjectDesignId(selectedDesign.Id, null);

            foreach (var stage in pmDesigns)
            {
                var ps = new PaymentStage
                {
                    Id = Guid.NewGuid(),
                    StageNo = stage.StageNo,
                    Name = stage.Name,
                    Description = stage.Description,
                    TotalContractPaid = (decimal)(stage.PricePercentage / 100) * (decimal)project.EstimatedPrice,
                    IsPrepaid = stage.IsPrepaid,
                    PricePercentage = stage.PricePercentage,
                    ProjectId = projectId,
                    IsDeleted = false,
                    Status = StageStatus.Unopen
                };

                _stageRepo.Save(ps);
            }
        }


        public void UpdatePaymentStage(Guid id, PaymentStageRequest request)
        {
            var ps = _stageRepo.GetById(id) ?? throw new Exception("This object is not existed!");

            ps.StageNo = 0;
            ps.Name = request.Name;
            ps.Description = request.Description;
            ps.IsPrepaid = request.IsPrepaid;
            ps.PricePercentage = request.PricePercentage;
            ps.EndTimePayment = request.EndTimePayment;
            ps.ProjectId = request.ProjectId;

            ProjectService projectService = new (_projectRepo, _roomRepo, _roomTypeRepo, _taskRepo, _stageRepo, _projectDesignRepo, _stageDesignRepo, _floorRepo);
            var project = projectService.GetById(request.ProjectId);

            ps.TotalContractPaid = (decimal)(request.PricePercentage / 100) * (decimal)project.EstimatedPrice;
            
            _stageRepo.Update(ps);

            UpdateStageNoByProjectId(ps.ProjectId);
        }

        public void UpdateStageNoByProjectId(Guid projectId)
        {
            var stageList = _stageRepo.GetByProjectId(projectId)
                                .OrderBy(s => s.EndTimePayment ?? DateTime.MaxValue)
                                    .ThenByDescending(s => DateTime.MaxValue)
                                .ToList();

            int stageNumber = 1;

            foreach (var stage in stageList)
            {
                stage.StageNo = stageNumber++;
                _stageRepo.Update(stage);
            }
        }

        public void StartStage(Guid id)
        {
            var ps = _stageRepo.GetById(id) ?? throw new Exception("This object is not existed!");

            ps.StartedDate = DateTime.Now;
            ps.Status = StageStatus.Ongoing;

            _stageRepo.Update(ps);

            ProjectTaskService taskService = new (_taskRepo, _projectRepo, _stageRepo, _projectDesignRepo, _stageDesignRepo, _floorRepo, _roomRepo, _roomTypeRepo);
            taskService.StartTasksOfStage(id, ps.ProjectId);
        }

        public void CloseStage(Guid id)
        {
            var ps = _stageRepo.GetById(id) ?? throw new Exception("This object is not existed!");

            ProjectTaskService taskService = new (_taskRepo, _projectRepo, _stageRepo, _projectDesignRepo, _stageDesignRepo, _floorRepo, _roomRepo, _roomTypeRepo);
            var tasksOfStage = taskService.GetAllProjectTaskIdByFilter(ps.ProjectId, null, id, ProjectTaskStatus.Ongoing, null, null, false, true, null);

            if (tasksOfStage.Count() > 0)
                throw new Exception("Some Task in Stage is not done!");

            ps.EndDate = DateTime.Now;
            ps.Status = StageStatus.Done;

            _stageRepo.Update(ps);
        }

        public void UpdateStageEstimateBusinessDay(Guid id, int estimateBusinessDay)
        {
            var ps = _stageRepo.GetById(id) ?? throw new Exception("This object is not existed!");

            ps.EstimateBusinessDay = estimateBusinessDay;

            _stageRepo.Update(ps);
        }

        public void UpdateStageStatus(Guid id, StageStatus status)
        {
            var ps = _stageRepo.GetById(id) ?? throw new Exception("This object is not existed!");

            ps.Status = status;

            _stageRepo.Update(ps);
        }

        public void UpdateStagePenaltyFee(Guid stageId, decimal penaltyFee)
        {
            var ps = _stageRepo.GetById(stageId) ?? throw new Exception("This object is not existed!");

            ps.PenaltyFee = penaltyFee;

            _stageRepo.Update(ps);

            UpdateProjectPenaltyFeeByStages(ps.ProjectId);
        }

        public void UpdateProjectPenaltyFeeByStages(Guid projectId)
        {
            var stageList = _stageRepo.GetByProjectId(projectId);

            decimal totalPenaltyFee = stageList.Sum(stage => stage.PenaltyFee ?? 0);

            ProjectService projectService = new(_projectRepo, _roomRepo, _roomTypeRepo, _taskRepo, _stageRepo, _projectDesignRepo, _stageDesignRepo, _floorRepo);
            projectService.UpdateProjectTotalPenaltyFee(projectId, totalPenaltyFee);
        }

        public void UpdateStagesTotalContractPaid(Guid projectId, decimal estimatePrice)
        {
            var stageList = _stageRepo.GetByProjectId(projectId);

            foreach (var stage in stageList)
            {
                stage.TotalContractPaid = (decimal)(stage.PricePercentage / 100) * (decimal)estimatePrice;

                _stageRepo.Update(stage);
            }
        }

        public void UpdateStageTotalIncurredPaid(Guid stageId, decimal totalIncurredPaid)
        {
            var stage = _stageRepo.GetById(stageId) ?? throw new Exception("This object is not existed!");

            stage.TotalIncurredPaid = totalIncurredPaid == 0 ? null : totalIncurredPaid;

            _stageRepo.Update(stage);
        }

        public void UpdateStagePaid(Guid projectId, decimal totalPaid)
        {
            var listStages = _stageRepo.GetByProjectId(projectId)
                                        .OrderBy(s => s.StageNo)
                                        .ToList();

            decimal remainingAmount = totalPaid;

            foreach (var stage in listStages)
            {
                if (remainingAmount >= stage.TotalContractPaid)
                {
                    stage.IsContractAmountPaid = true;
                    remainingAmount -= stage.TotalContractPaid;
                }
                else
                {
                    break;
                }

                if (stage.TotalIncurredPaid == null)
                    stage.IsIncurredAmountPaid = true;
                else
                {
                    if (remainingAmount >= stage.TotalIncurredPaid)
                    {
                        stage.IsIncurredAmountPaid = true;
                        remainingAmount -= stage.TotalIncurredPaid.Value;
                    }
                }

                _stageRepo.Update(stage);
            }
        }

        public void DeletePaymentStage(Guid id)
        {
            var ps = _stageRepo.GetById(id) ?? throw new Exception("This object is not existed!");

            ps.IsDeleted = true;

            _stageRepo.Update(ps);

            ProjectTaskService taskService = new (_taskRepo, _projectRepo, _stageRepo, _projectDesignRepo, _stageDesignRepo, _floorRepo, _roomRepo, _roomTypeRepo);
            taskService.UpdateTasksInDeletedStage(id, ps.ProjectId);

            UpdateProjectPenaltyFeeByStages(ps.ProjectId);
        }
    }
}
