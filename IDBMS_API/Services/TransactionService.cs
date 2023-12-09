using IDBMS_API.DTOs.Request;
using BusinessObject.Enums;
using BusinessObject.Models;
using Repository.Interfaces;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;
using UnidecodeSharpFork;

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
            TransactionType? type, TransactionStatus? status)
        {
            IEnumerable<Transaction> filteredList = list;

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

        public IEnumerable<Transaction> GetAll(TransactionType? type, TransactionStatus? status)
        {
            var list = _repository.GetAll();

            return Filter(list, type, status);
        }
        public Transaction? GetById(Guid id)
        {
            return _repository.GetById(id) ?? throw new Exception("This object is not existed!");
        }
        public IEnumerable<Transaction?> GetByProjectId(Guid id, TransactionType? type, TransactionStatus? status)
        {
            var list = _repository.GetByProjectId(id) ?? throw new Exception("This object is not existed!");

            return Filter(list, type, status);
        }
        public IEnumerable<Transaction?> GetByUserId(Guid id, TransactionType? type, TransactionStatus? status)
        {
            var list = _repository.GetByUserId(id) ?? throw new Exception("This object is not existed!");

            return Filter(list, type, status);
        }
        public async Task<Transaction?> CreateTransaction([FromForm] TransactionRequest request)
        {
            FirebaseService s = new FirebaseService();
            string link = await s.UploadTransactionImage(request.TransactionReceiptImage);
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
                TransactionReceiptImageUrl = link,
            };
            var transCreated = _repository.Save(trans);
            return transCreated;
        }
        public async void UpdateTransaction(Guid id, TransactionRequest request)
        {
            var trans = _repository.GetById(id) ?? throw new Exception("This object is not existed!");
            FirebaseService s = new FirebaseService();
            string link = await s.UploadTransactionImage(request.TransactionReceiptImage);
            trans.Type = request.Type;
            trans.Amount = request.Amount;
            trans.Note = request.Note;
            trans.UserId = request.UserId;
            trans.ProjectId = request.ProjectId;
            trans.TransactionReceiptImageUrl = link;

            _repository.Update(trans);
        }
        public void UpdateTransactionStatus(Guid id, TransactionStatus status)
        {
            var trans = _repository.GetById(id) ?? throw new Exception("This object is not existed!");

            trans.Status = status;

            _repository.Update(trans);
        }
        public void DeleteTransactionStatus(Guid id)
        {
            var trans = _repository.GetById(id) ?? throw new Exception("This object is not existed!");

            trans.IsDeleted = true;

            _repository.Update(trans);
        }
    }
}
