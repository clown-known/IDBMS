using BusinessObject.DTOs.Request.CreateRequests;
using BusinessObject.Models;
using Repository.Interfaces;


namespace IDBMS_API.Services
{
    public class TransactionService
    {
        private readonly ITransactionRepository repository;
        public TransactionService(ITransactionRepository repository)
        {
            this.repository = repository;
        }   
        public IEnumerable<Transaction> GetAll()
        {
            return repository.GetAll();
        }
        public Transaction? GetById(Guid id)
        {
            return repository.GetById(id);
        }
        public async Task<Transaction?> CreateTransaction(TransactionRequest request)
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
            var transCreated = repository.Save(trans);
            return transCreated;
        }
        public async Task UpdateTransaction(TransactionRequest request)
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
            repository.Update(trans);
        }
        public async Task DeleteTransaction(Guid id)
        {
            repository.DeleteById(id);
        }
    }
}
