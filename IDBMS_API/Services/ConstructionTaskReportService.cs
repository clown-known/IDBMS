using BusinessObject.DTOs.Request.CreateRequests;
using BusinessObject.Models;
using Repository.Interfaces;

namespace IDBMS_API.Services
{
    public class ConstructionTaskReportService
    {
        private readonly IConstructionTaskReportRepository repository;
        public ConstructionTaskReportService(IConstructionTaskReportRepository repository)
        {
            this.repository = repository;
        }
        public IEnumerable<ConstructionTaskReport> GetAll()
        {
            return repository.GetAll();
        }
        public ConstructionTaskReport? GetById(Guid id)
        {
            return repository.GetById(id);
        }
        public ConstructionTaskReport? CreateConstructionTaskReport(ConstructionTaskReportRequest request)
        {
            var ctr = new ConstructionTaskReport
            {
                Name = request.Name,
                CalculationUnit = request.CalculationUnit,
                UnitUsed = request.UnitUsed,
                Description = request.Description,
                ConstructionTaskId = request.ConstructionTaskId,
                CreatedTime = request.CreatedTime,
                AuthorId = request.AuthorId,
                IsDeleted = request.IsDeleted,
            };
            var ctrCreated = repository.Save(ctr);
            return ctrCreated;
        }
        public void UpdateConstructionTaskReport(ConstructionTaskReportRequest request)
        {
            var ctr = new ConstructionTaskReport
            {
                Name = request.Name,
                CalculationUnit = request.CalculationUnit,
                UnitUsed = request.UnitUsed,
                Description = request.Description,
                ConstructionTaskId = request.ConstructionTaskId,
                CreatedTime = request.CreatedTime,
                AuthorId = request.AuthorId,
                IsDeleted = request.IsDeleted,
            };
            repository.Update(ctr);
        }
    }
}
