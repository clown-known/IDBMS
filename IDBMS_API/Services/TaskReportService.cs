using IDBMS_API.DTOs.Request;
using BusinessObject.Models;
using Repository.Interfaces;

namespace IDBMS_API.Services
{
    public class TaskReportService
    {
        private readonly ITaskReportRepository _repository;
        public TaskReportService(ITaskReportRepository repository)
        {
            _repository = repository;
        }
        public IEnumerable<TaskReport> GetAll()
        {
            return _repository.GetAll();
        }
        public TaskReport? GetById(Guid id)
        {
            return _repository.GetById(id) ?? throw new Exception("This object is not existed!");
        }
        public IEnumerable<TaskReport?> GetByTaskId(Guid id)
        {
            return _repository.GetByTaskId(id) ?? throw new Exception("This object is not existed!");
        }
        public IEnumerable<TaskReport?> GetByUserId(Guid id)
        {
            return _repository.GetByUserId(id) ?? throw new Exception("This object is not existed!");
        }
        public TaskReport? CreateTaskReport(TaskReportRequest request)
        {
            var ctr = new TaskReport
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                CalculationUnit = request.CalculationUnit,
                UnitUsed = request.UnitUsed,
                Description = request.Description,
                CreatedTime = DateTime.Now,
                IsDeleted = false,
            };
            var ctrCreated = _repository.Save(ctr);
            return ctrCreated;
        }
        public void UpdateTaskReport(Guid id, TaskReportRequest request)
        {
            var ctr = _repository.GetById(id) ?? throw new Exception("This object is not existed!");

            ctr.Name = request.Name;
            ctr.CalculationUnit = request.CalculationUnit;
            ctr.UnitUsed = request.UnitUsed;
            ctr.Description = request.Description;
            ctr.UpdatedTime = DateTime.Now;

            _repository.Update(ctr);
        }
        public void DeleteTaskReport(Guid id)
        {
            var ctr = _repository.GetById(id) ?? throw new Exception("This object is not existed!");

            ctr.IsDeleted = true;

            _repository.Update(ctr);
        }
    }

}
