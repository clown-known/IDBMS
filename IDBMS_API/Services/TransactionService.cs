using BusinessObject.DTOs.Request;
using BusinessObject.Models;
using Repository.Interfaces;


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
            return _repository.GetById(id);
        }
        public Transaction? CreateTransaction(TransactionRequest request)
        {
            var trans = new Transaction
            {
                Type = request.Type,
                Amount = request.Amount,
                Note = request.Note,
                CreatedDate = request.CreatedDate,
                PrepayStageId = request.PrepayStageId,
                UserId = request.UserId,
                Status = request.Status,
                TransactionReceiptImageUrl = request.TransactionReceiptImageUrl,
                AdminNote = request.AdminNote,
            };
            var transCreated = _repository.Save(trans);
            return transCreated;
        }
        public void UpdateTransaction(TransactionRequest request)
        {
            var trans = new Transaction
            {
                Type = request.Type,
                Amount = request.Amount,
                Note = request.Note,
                CreatedDate = request.CreatedDate,
                PrepayStageId = request.PrepayStageId,
                UserId = request.UserId,
                Status = request.Status,
                TransactionReceiptImageUrl = request.TransactionReceiptImageUrl,
                AdminNote = request.AdminNote,
            };
            _repository.Update(trans);
        }
        public void DeleteTransaction(Guid id)
        {
            _repository.DeleteById(id);
        }
    }
}
