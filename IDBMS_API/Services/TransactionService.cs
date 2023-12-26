using IDBMS_API.DTOs.Request;
using BusinessObject.Enums;
using BusinessObject.Models;
using Repository.Interfaces;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;
using UnidecodeSharpFork;
using DocumentFormat.OpenXml.Wordprocessing;

namespace IDBMS_API.Services
{
    public class TransactionService
    {
        private readonly ITransactionRepository _transactionRepo;
        private readonly IPaymentStageRepository _stageRepo;
        private readonly IProjectRepository _projectRepo;
        private readonly IProjectDesignRepository _projectDesignRepo;
        private readonly IPaymentStageDesignRepository _stageDesignRepo;
        private readonly IProjectTaskRepository _taskRepo;
        private readonly IFloorRepository _floorRepo;
        private readonly IRoomRepository _roomRepo;
        private readonly IRoomTypeRepository _roomTypeRepo;

        public TransactionService(
                ITransactionRepository transactionRepo,
                IPaymentStageRepository stageRepo,
                IProjectRepository projectRepo,
                IProjectDesignRepository projectDesignRepo,
                IPaymentStageDesignRepository stageDesignRepo,
                IProjectTaskRepository taskRepo,
                IFloorRepository floorRepo,
                IRoomRepository roomRepo,
                IRoomTypeRepository roomTypeRepo)
        {
            _transactionRepo = transactionRepo;
            _stageRepo = stageRepo;
            _projectRepo = projectRepo;
            _projectDesignRepo = projectDesignRepo;
            _stageDesignRepo = stageDesignRepo;
            _taskRepo = taskRepo;
            _floorRepo = floorRepo;
            _roomRepo = roomRepo;
            _roomTypeRepo = roomTypeRepo;
        }

        public IEnumerable<Transaction> Filter(IEnumerable<Transaction> list, 
            string? payerName, TransactionType? type, TransactionStatus? status)
        {
            IEnumerable<Transaction> filteredList = list;

            if (payerName != null)
            {
                filteredList = filteredList.Where(item => (item.PayerName != null && item.PayerName.Unidecode().IndexOf(payerName.Unidecode(), StringComparison.OrdinalIgnoreCase) >= 0));

            }

            if (type != null)
            {
                filteredList = filteredList.Where(item => item.Type == type);
            }

            if (status != null)
            {
                filteredList = filteredList.Where(item => item.Status == status);
            }

            return filteredList;
        }

        public IEnumerable<Transaction> GetAll(string? payerName, TransactionType? type, TransactionStatus? status)
        {
            var list = _transactionRepo.GetAll();

            return Filter(list, payerName, type, status);
        }
        public Transaction? GetById(Guid id)
        {
            return _transactionRepo.GetById(id) ?? throw new Exception("This object is not existed!");
        }

        public IEnumerable<Transaction> GetByProjectId(Guid id, string? payerName, TransactionType? type, TransactionStatus? status)
        {
            var list = _transactionRepo.GetByProjectId(id) ?? throw new Exception("This object is not existed!");

            return Filter(list, payerName, type, status);
        }

        public IEnumerable<Transaction> GetByUserId(Guid id, string? payerName, TransactionType? type, TransactionStatus? status)
        {
            var list = _transactionRepo.GetByUserId(id) ?? throw new Exception("This object is not existed!");

            return Filter(list, payerName, type, status);
        }

        public async Task<Transaction?> CreateTransaction([FromForm] TransactionRequest request)
        {
            var trans = new Transaction
            {
                Id = Guid.NewGuid(),
                Type = request.Type,
                Amount = request.Amount,
                Note = request.Note,
                CreatedDate = DateTime.Now,
                UserId = request.UserId,
                ProjectId = request.ProjectId,
                Status = request.Status,
                IsDeleted = false,
                PayerName = request.PayerName,
            };

            if (request.TransactionReceiptImage != null)
            {
                FirebaseService s = new FirebaseService();
                string link = await s.UploadTransactionImage(request.TransactionReceiptImage);

                trans.TransactionReceiptImageUrl = link;
            }

            var transCreated = _transactionRepo.Save(trans);

            UpdateTotalPaidByProjectId(trans.ProjectId);

            return transCreated;
        }

        public Transaction? CreateTransactionByWarrantyClaim(Guid warrantyClaimId, [FromForm] TransactionRequest request)
        {
            var trans = new Transaction
            {
                Id = Guid.NewGuid(),
                Type = TransactionType.Warranty,
                Amount = request.Amount,
                Note = request.Note,
                CreatedDate = DateTime.Now,
                UserId = request.UserId,
                ProjectId = request.ProjectId,
                Status = TransactionStatus.Success,
                IsDeleted = false,
                PayerName = "Công ty IDT Décor",
                WarrantyClaimId = warrantyClaimId,
            };

            var transCreated = _transactionRepo.Save(trans);

            UpdateTotalPaidByProjectId(trans.ProjectId);

            return transCreated;
        }

        public async void UpdateTransaction(Guid id, TransactionRequest request)
        {
            var trans = _transactionRepo.GetById(id) ?? throw new Exception("This object is not existed!");

            if (request.TransactionReceiptImage != null)
            {
                FirebaseService s = new FirebaseService();
                string link = await s.UploadTransactionImage(request.TransactionReceiptImage);

                trans.TransactionReceiptImageUrl = link;
            }

            trans.Type = request.Type;
            trans.Amount = request.Amount;
            trans.Note = request.Note;
            trans.UserId = request.UserId;
            trans.ProjectId = request.ProjectId;
            trans.PayerName = request.PayerName;

            _transactionRepo.Update(trans);

            UpdateTotalPaidByProjectId(trans.ProjectId);
        }

        public void UpdateTransactionStatus(Guid id, TransactionStatus status)
        {
            var trans = _transactionRepo.GetById(id) ?? throw new Exception("This object is not existed!");

            trans.Status = status;

            _transactionRepo.Update(trans);

            UpdateTotalPaidByProjectId(trans.ProjectId);
        }

        public void UpdateTotalPaidByProjectId(Guid projectId)
        {
            var listTransaction = _transactionRepo
                                    .GetByProjectId(projectId)
                                    .Where(transaction => transaction.Status == TransactionStatus.Success);

            decimal totalPaid = listTransaction.Sum(transaction => transaction.Amount);

            PaymentStageService stageService = new (_stageRepo, _projectRepo, _projectDesignRepo, _stageDesignRepo, _taskRepo, _floorRepo, _roomRepo, _roomTypeRepo);
            stageService.UpdateStagePaid(projectId, totalPaid);

            ProjectService projectService = new(_projectRepo, _roomRepo, _roomTypeRepo, _taskRepo, _stageRepo, _projectDesignRepo, _stageDesignRepo, _floorRepo);
            projectService.UpdateProjectAmountPaid(projectId, totalPaid);
        }

        public void DeleteTransactionById(Guid id)
        {
            var trans = _transactionRepo.GetById(id) ?? throw new Exception("This object is not existed!");

            trans.IsDeleted = true;

            _transactionRepo.Update(trans);

            UpdateTotalPaidByProjectId(trans.ProjectId);
        }

        public void DeleteTransactionsByWarrantyId(Guid warrantyId, Guid projectId)
        {
            var list = _transactionRepo.GetByWarrantyId(warrantyId);

            foreach (var trans in list)
            {
                DeleteTransactionById(trans.Id);
            }

            UpdateTotalPaidByProjectId(projectId);
        }
    }
}
