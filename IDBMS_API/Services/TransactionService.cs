using BusinessObject.DTOs.Request;
using BusinessObject.Enums;
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
            return _repository.GetById(id) ?? throw new Exception("This object is not existed!");
        }
/*        public IEnumerable<Transaction?> GetByPaymentStageId(Guid psId)
        {
            return _repository.GetByPaymentStageId(psId) ?? throw new Exception("This object is not existed!");
        }*/
        public IEnumerable<Transaction?> GetByUserId(Guid userId)
        {
            return _repository.GetByUserId(userId) ?? throw new Exception("This object is not existed!");
        }
        public Transaction? CreateTransaction(TransactionRequest request)
        {
            var trans = new Transaction
            {
                Id = Guid.NewGuid(),
                Type = request.Type,
                Amount = request.Amount,
                Note = request.Note,
                CreatedDate = DateTime.Now,
/*                PaymentStageId = request.PaymentStageId,*/
                UserId = request.UserId,
                Status = TransactionStatus.Pending,
                TransactionReceiptImageUrl = request.TransactionReceiptImageUrl,
                AdminNote = request.AdminNote,
            };
            var transCreated = _repository.Save(trans);
            return transCreated;
        }
        public void UpdateTransaction(Guid id, TransactionRequest request)
        {
            var trans = _repository.GetById(id) ?? throw new Exception("This object is not existed!");

            trans.Type = request.Type;
            trans.Amount = request.Amount;
            trans.Note = request.Note;
/*            trans.PaymentStageId = request.PaymentStageId;*/
            trans.UserId = request.UserId;
            trans.TransactionReceiptImageUrl = request.TransactionReceiptImageUrl;
            trans.AdminNote = request.AdminNote;

            _repository.Update(trans);
        }
        public void UpdateTransactionStatus(Guid id, TransactionStatus status)
        {
            var trans = _repository.GetById(id) ?? throw new Exception("This object is not existed!");

            trans.Status = status;

            _repository.Update(trans);
        }
    }
}
