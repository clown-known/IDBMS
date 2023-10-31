using BusinessObject.DTOs.Request;
using BusinessObject.Models;
using Repository.Interfaces;

namespace IDBMS_API.Services
{
    public class DecorProgressReportService
    {
        private readonly IDecorProgressReportRepository _repository;
        public DecorProgressReportService(IDecorProgressReportRepository repository)
        {
            _repository = repository;
        }
        public IEnumerable<DecorProgressReport> GetAll()
        {
            return _repository.GetAll();
        }
        public DecorProgressReport? GetById(Guid id)
        {
            return _repository.GetById(id) ?? throw new Exception("This object is not existed!"); 
        }
        public IEnumerable<DecorProgressReport?> GetByPrepayStageId(Guid id)
        {
            return _repository.GetByPrepayStageId(id) ?? throw new Exception("This object is not existed!"); 
        }
        public DecorProgressReport? CreateDecorProgressReport(DecorProgressReportRequest request)
        {
            var dpr = new DecorProgressReport
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                AuthorId = request.AuthorId,
                CreatedDate = DateTime.Now,
                PaymentStageId = request.PaymentStageId,
                IsDeleted = false,
            };
            var dprCreated = _repository.Save(dpr);
            return dprCreated;
        }
        public void UpdateDecorProgressReport(Guid id, DecorProgressReportRequest request)
        {
            var dpr = _repository.GetById(id) ?? throw new Exception("This object is not existed!");

            dpr.Name = request.Name;
            dpr.Description = request.Description;
            dpr.AuthorId = request.AuthorId;
            dpr.PrepayStageId = request.PrepayStageId;
            
            _repository.Update(dpr);
        }
        public void DeleteDecorProgressReport(Guid id)
        {
            var dpr = _repository.GetById(id) ?? throw new Exception("This object is not existed!");

            dpr.IsDeleted = true;

            _repository.Update(dpr);
        }
    }
}
