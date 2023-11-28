using IDBMS_API.DTOs.Request;
using BusinessObject.Enums;
using BusinessObject.Models;
using Repository.Interfaces;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace IDBMS_API.Services
{
    public class TransactionService
    {
        private readonly ITransactionRepository _repository;
        public TransactionService(ITransactionRepository repository)
        {
            _repository = repository;
        }
        public IEnumerable<Transaction> GetAll()
        {
            return _repository.GetAll();
        }
        public Transaction? GetById(Guid id)
        {
            return _repository.GetById(id) ?? throw new Exception("This object is not existed!");
        }
        public IEnumerable<Transaction?> GetByProjectId(Guid id)
        {
            return _repository.GetByProjectId(id) ?? throw new Exception("This object is not existed!");
        }
        public IEnumerable<Transaction?> GetByUserId(Guid id)
        {
            return _repository.GetByUserId(id) ?? throw new Exception("This object is not existed!");
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
                AdminNote = request.AdminNote,
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
            trans.AdminNote = request.AdminNote;

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
