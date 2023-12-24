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
        private readonly ITransactionRepository _repository;
        public TransactionService(ITransactionRepository repository)
        {
            _repository = repository;
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
            var list = _repository.GetAll();

            return Filter(list, payerName, type, status);
        }
        public Transaction? GetById(Guid id)
        {
            return _repository.GetById(id) ?? throw new Exception("This object is not existed!");
        }
        public IEnumerable<Transaction?> GetByProjectId(Guid id, string? payerName, TransactionType? type, TransactionStatus? status)
        {
            var list = _repository.GetByProjectId(id) ?? throw new Exception("This object is not existed!");

            return Filter(list, payerName, type, status);
        }
        public IEnumerable<Transaction?> GetByUserId(Guid id, string? payerName, TransactionType? type, TransactionStatus? status)
        {
            var list = _repository.GetByUserId(id) ?? throw new Exception("This object is not existed!");

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

            var transCreated = _repository.Save(trans);
            return transCreated;
        }

        public async Task<Transaction?> CreateTransactionByWarrantyClaim(Guid warrantyClaimId, [FromForm] TransactionRequest request)
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

            var transCreated = _repository.Save(trans);
            return transCreated;
        }

        public async void UpdateTransaction(Guid id, TransactionRequest request)
        {
            var trans = _repository.GetById(id) ?? throw new Exception("This object is not existed!");

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

            _repository.Update(trans);
        }
        public void UpdateTransactionStatus(Guid id, TransactionStatus status)
        {
            var trans = _repository.GetById(id) ?? throw new Exception("This object is not existed!");

            trans.Status = status;

            _repository.Update(trans);
        }
        public void DeleteTransactionById(Guid id)
        {
            var trans = _repository.GetById(id) ?? throw new Exception("This object is not existed!");

            trans.IsDeleted = true;

            _repository.Update(trans);
        }
    }
}
