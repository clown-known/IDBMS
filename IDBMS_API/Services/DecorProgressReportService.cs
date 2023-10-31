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
        public DecorProgressReport? Get(Guid id)
        {
            return _repository.GetById(id);
        }
        public DecorProgressReport? CreateDecorProgressReport(DecorProgressReportRequest request)
        {
            var dpr = new DecorProgressReport
            {
                Name = request.Name,
                Description = request.Description,
                AuthorId = request.AuthorId,
                CreatedDate = request.CreatedDate,
                PaymentStageId = request.PaymentStageId,
                IsDeleted = request.IsDeleted,
            };
            var dprCreated = _repository.Save(dpr);
            return dprCreated;
        }
        public void UpdateDecorProgressReport(DecorProgressReportRequest request)
        {
            var dpr = new DecorProgressReport
            {
                Name = request.Name,
                Description = request.Description,
                AuthorId = request.AuthorId,
                CreatedDate = request.CreatedDate,
                PaymentStageId = request.PaymentStageId,
                IsDeleted = request.IsDeleted,
            };
            _repository.Update(dpr);
        }
    }
}
