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
        public IEnumerable<Transaction?> GetByPrepayStageId(Guid psId)
        {
            return _repository.GetByPrepayStageId(psId) ?? throw new Exception("This object is not existed!");
        }
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
        public void UpdateTransaction(Guid id, TransactionRequest request)
        {
            var trans = _repository.GetById(id) ?? throw new Exception("This object is not existed!");

            trans.Type = request.Type;
            trans.Amount = request.Amount;
            trans.Note = request.Note;
            trans.CreatedDate = request.CreatedDate;
            trans.PrepayStageId = request.PrepayStageId;
            trans.UserId = request.UserId;
            trans.Status = request.Status;
            trans.TransactionReceiptImageUrl = request.TransactionReceiptImageUrl;
            trans.AdminNote = request.AdminNote;
            
            _repository.Update(trans);
        }
        public void UpdateTransactionStatus(Guid id, int status)
        {
            var trans = _repository.GetById(id) ?? throw new Exception("This object is not existed!");
            bool isValueDefined = Enum.IsDefined(typeof(TransactionStatus), status);
            if (isValueDefined)
            {
                trans.Status = (TransactionStatus)status;
                _repository.Update(trans);
            }
            else throw new Exception("The input is invalid!");
        }
        public void DeleteTransaction(Guid id)
        {
            _repository.DeleteById(id);
        }
    }
}
