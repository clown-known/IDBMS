/*using BusinessObject.DTOs.Request;
using BusinessObject.Models;
using Repository.Interfaces;

namespace IDBMS_API.Services
{
    public class ConstructionTaskReportService
    {
        private readonly IConstructionTaskReportRepository _repository;
        public ConstructionTaskReportService(IConstructionTaskReportRepository repository)
        {
            _repository = repository;
        }
        public IEnumerable<ConstructionTaskReport> GetAll()
        {
            return _repository.GetAll();
        }
        public ConstructionTaskReport? GetById(Guid id)
        {
            return _repository.GetById(id) ?? throw new Exception("This object is not existed!");
        }
        public IEnumerable<ConstructionTaskReport?> GetByConstructionTaskId(Guid id)
        {
            return _repository.GetByConstructionTaskId(id) ?? throw new Exception("This object is not existed!");
        }
        public ConstructionTaskReport? CreateConstructionTaskReport(ConstructionTaskReportRequest request)
        {
            var ctr = new ConstructionTaskReport
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                CalculationUnit = request.CalculationUnit,
                UnitUsed = request.UnitUsed,
                Description = request.Description,
                ConstructionTaskId = request.ConstructionTaskId,
                CreatedTime = DateTime.Now,
                AuthorId = request.AuthorId,
                IsDeleted = false,
            };
            var ctrCreated = _repository.Save(ctr);
            return ctrCreated;
        }
        public void UpdateConstructionTaskReport(Guid id, ConstructionTaskReportRequest request)
        {
            var ctr = _repository.GetById(id) ?? throw new Exception("This object is not existed!");

            ctr.Name = request.Name;
            ctr.CalculationUnit = request.CalculationUnit;
            ctr.UnitUsed = request.UnitUsed;
            ctr.Description = request.Description;
            ctr.ConstructionTaskId = request.ConstructionTaskId;
            ctr.AuthorId = request.AuthorId;
           
            _repository.Update(ctr);
        }
        public void DeleteConstructionTaskReport(Guid id)
        {
            var ctr = _repository.GetById(id) ?? throw new Exception("This object is not existed!");

            ctr.IsDeleted = true;

            _repository.Update(ctr);
        }
    }
}
*/