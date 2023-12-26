using IDBMS_API.DTOs.Request;
using BusinessObject.Models;
using Repository.Interfaces;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;
using Azure.Core;
using BusinessObject.Enums;
using UnidecodeSharpFork;

namespace IDBMS_API.Services
{
    public class WarrantyClaimService
    {
        private readonly IWarrantyClaimRepository _warrantyRepo;
        private readonly IProjectRepository _projectRepo;
        private readonly IProjectParticipationRepository _projectParticipationRepo;
        private readonly ITransactionRepository _transactionRepo;
        private readonly IFloorRepository _floorRepo;
        private readonly IRoomRepository _roomRepo;
        private readonly IRoomTypeRepository _roomTypeRepo;
        private readonly IProjectTaskRepository _projectTaskRepo;
        private readonly IPaymentStageRepository _stageRepo;
        private readonly IProjectDesignRepository _projectDesignRepo;
        private readonly IPaymentStageDesignRepository _stageDesignRepo;

        public WarrantyClaimService(
                IWarrantyClaimRepository warrantyRepo,
                IProjectRepository projectRepo,
                IProjectParticipationRepository projectParticipationRepo,
                ITransactionRepository transactionRepo,
                IFloorRepository floorRepo,
                IRoomRepository roomRepo,
                IRoomTypeRepository roomTypeRepo,
                IProjectTaskRepository projectTaskRepo,
                IPaymentStageRepository stageRepo,
                IProjectDesignRepository projectDesignRepo,
                IPaymentStageDesignRepository stageDesignRepo)
        {
            _warrantyRepo = warrantyRepo;
            _projectRepo = projectRepo;
            _projectParticipationRepo = projectParticipationRepo;
            _transactionRepo = transactionRepo;
            _floorRepo = floorRepo;
            _roomRepo = roomRepo;
            _roomTypeRepo = roomTypeRepo;
            _projectTaskRepo = projectTaskRepo;
            _stageRepo = stageRepo;
            _projectDesignRepo = projectDesignRepo;
            _stageDesignRepo = stageDesignRepo;
        }

        public IEnumerable<WarrantyClaim> Filter(IEnumerable<WarrantyClaim> list,
            bool? isCompanyCover, string? name)
        {
            IEnumerable<WarrantyClaim> filteredList = list;

            if (isCompanyCover != null)
            {
                filteredList = filteredList.Where(item => item.IsCompanyCover == isCompanyCover);
            }

            if (name != null)
            {
                filteredList = filteredList.Where(item => (item.Name != null && item.Name.Unidecode().IndexOf(name.Unidecode(), StringComparison.OrdinalIgnoreCase) >= 0));
            }

            return filteredList;
        }

        public IEnumerable<WarrantyClaim> GetAll(bool? isCompanyCover, string? name)
        {
            var list = _warrantyRepo.GetAll();

            return Filter(list, isCompanyCover, name);
        }

        public WarrantyClaim? GetById(Guid id)
        {
            return _warrantyRepo.GetById(id) ?? throw new Exception("This object is not existed!");
        }

        public IEnumerable<WarrantyClaim> GetByUserId(Guid id, bool? isCompanyCover, string? name)
        {
            var list = _warrantyRepo.GetByUserId(id) ?? throw new Exception("This object is not existed!");

            return Filter(list, isCompanyCover, name);
        }

        public IEnumerable<WarrantyClaim> GetByProjectId(Guid id, bool? isCompanyCover, string? name)
        {
            var list = _warrantyRepo.GetByProjectId(id) ?? throw new Exception("This object is not existed!");

            return Filter(list, isCompanyCover, name);
        }

        public void UpdateProjectTotalWarrantyPaid(Guid projectId)
        {
            var claimsInProject = _warrantyRepo.GetByProjectId(projectId);

            decimal totalPaid = 0;

            if (claimsInProject != null && claimsInProject.Any())
            {
                totalPaid = claimsInProject.Sum(claim =>
                {
                    if (claim != null && claim.IsDeleted != true)
                    {
                        return claim.TotalPaid;
                    }
                    return 0;
                });
            }

            ProjectService projectService = new(_projectRepo, _roomRepo, _roomTypeRepo, _projectTaskRepo, _stageRepo, _projectDesignRepo, _stageDesignRepo, _floorRepo);
            projectService.UpdateProjectDataByWarrantyClaim(projectId, totalPaid);

        }

        public async Task<WarrantyClaim?> CreateWarrantyClaim([FromForm]WarrantyClaimRequest request)
        {

            var projectOwner = _projectParticipationRepo.GetProjectOwnerByProjectId(request.ProjectId);

            string link = "";

            if (request.ConfirmationDocument != null)
            {
                FirebaseService s = new FirebaseService();
                link = await s.UploadDocument(request.ConfirmationDocument,request.ProjectId);
            }

            var wc = new WarrantyClaim
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Reason = request.Reason,
                Solution = request.Solution,
                Note = request.Note,
                TotalPaid = request.TotalPaid,
                IsCompanyCover = request.IsCompanyCover,
                CreatedDate = DateTime.Now,
                EndDate = request.EndDate,
                ConfirmationDocument = link,
                ProjectId = request.ProjectId,
                UserId = projectOwner == null ? null : projectOwner.UserId,
                IsDeleted = false,
            };
            var wcCreated = _warrantyRepo.Save(wc);

            UpdateProjectTotalWarrantyPaid(request.ProjectId);

            if (wcCreated != null)
            {
                var newTrans = new TransactionRequest
                {
                    Amount = wcCreated.IsCompanyCover ? 0 : wcCreated.TotalPaid,
                    UserId = wcCreated.UserId,
                    Note = wcCreated.Name,
                    ProjectId = wcCreated.ProjectId,
                };

                TransactionService transactionService = new (_transactionRepo, _stageRepo, _projectRepo, _projectDesignRepo, _stageDesignRepo, _projectTaskRepo, _floorRepo, _roomRepo, _roomTypeRepo);
                transactionService.CreateTransactionByWarrantyClaim(wcCreated.Id, newTrans);
            }

            return wcCreated;
        }

        public async void UpdateWarrantyClaim(Guid id, [FromForm] WarrantyClaimRequest request)
        {
            var wc = _warrantyRepo.GetById(id) ?? throw new Exception("This object is not existed!");

            decimal transactionBeforeUpdated = wc.IsCompanyCover ? 0 : wc.TotalPaid;
            decimal transactionAfterUpdated = request.IsCompanyCover ? 0 : request.TotalPaid;

            if (request.ConfirmationDocument != null)
            {
                FirebaseService s = new FirebaseService();
                string link = await s.UploadDocument(request.ConfirmationDocument, request.ProjectId);
                wc.ConfirmationDocument = link;
            }

            wc.Name = request.Name;
            wc.Reason = request.Reason;
            wc.Solution = request.Solution;
            wc.Note = request.Note;
            wc.TotalPaid = request.TotalPaid;
            wc.IsCompanyCover = request.IsCompanyCover;
            wc.EndDate = request.EndDate;

            _warrantyRepo.Update(wc);

            if (transactionAfterUpdated != transactionBeforeUpdated)
            {
                decimal difference = transactionAfterUpdated - transactionBeforeUpdated;

                var newTrans = new TransactionRequest
                {
                    Amount = difference,
                    UserId = wc.UserId,
                    Note = $"{wc.Name} ({DateTime.Now.ToString("dd/MM/yyyy")})",
                    ProjectId = wc.ProjectId,
                };

                TransactionService transactionService = new (_transactionRepo, _stageRepo, _projectRepo, _projectDesignRepo, _stageDesignRepo, _projectTaskRepo, _floorRepo, _roomRepo, _roomTypeRepo);
                transactionService.CreateTransactionByWarrantyClaim(wc.Id, newTrans);

            }

            UpdateProjectTotalWarrantyPaid(request.ProjectId);
        }
        public void DeleteWarrantyClaim(Guid id, Guid projectId)
        {
            var wc = _warrantyRepo.GetById(id) ?? throw new Exception("This object is not existed!");

            wc.IsDeleted = true;

            _warrantyRepo.Update(wc);

            TransactionService transactionService = new (_transactionRepo, _stageRepo, _projectRepo, _projectDesignRepo, _stageDesignRepo, _projectTaskRepo, _floorRepo, _roomRepo, _roomTypeRepo);
            transactionService.DeleteTransactionsByWarrantyId(id, projectId);

            UpdateProjectTotalWarrantyPaid(projectId);
        }
    }

}
