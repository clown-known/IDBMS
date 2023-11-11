/*using BusinessObject.DTOs.Request;
using BusinessObject.Enums;
using BusinessObject.Models;
using Repository.Interfaces;

namespace IDBMS_API.Services
{
    public class ConstructionTaskService
    {
        private readonly IConstructionTaskRepository _repository;
        public ConstructionTaskService(IConstructionTaskRepository repository)
        {
            _repository = repository;
        }
        public IEnumerable<ConstructionTask> GetAll()
        {
            return _repository.GetAll();
        }
        public ConstructionTask? GetById(Guid id)
        {
            return _repository.GetById(id) ?? throw new Exception("This object is not existed!");
        }
        public IEnumerable<ConstructionTask?> GetByProjectId(Guid id)
        {
            return _repository.GetByProjectId(id);
        }
        public ConstructionTask? CreateConstructionTask(ConstructionTaskRequest request)
        {
            var ct = new ConstructionTask
            {
                Id = Guid.NewGuid(),
                Code = request.Code,
                Name = request.Name,
                Description = request.Description,
                Percentage = request.Percentage,
                CalculationUnit = request.CalculationUnit,
                PricePerUnit = request.PricePerUnit,
                UnitInContract = request.UnitInContract,
                UnitUsed = request.UnitUsed,
                IsExceed = request.IsExceed,
                StartedDate = DateTime.Now,
                EndDate = request.EndDate,
                NoDate = request.NoDate,
                ParentTaskId = request.ParentTaskId,
                ConstructionTaskCategoryId = request.ConstructionTaskCategoryId,
                ProjectId = request.ProjectId,
                PaymentStageId = request.PaymentStageId,
                InteriorItemId = request.InteriorItemId,
                ConstructionTaskDesignId = request.ConstructionTaskDesignId,
                Status = request.Status,
            };
            var ctCreated = _repository.Save(ct);
            return ctCreated;
        }
        public void UpdateConstructionTask(Guid id, ConstructionTaskRequest request)
        {
            var ct = _repository.GetById(id) ?? throw new Exception("This object is not existed!");

            ct.Code = request.Code;
            ct.Name = request.Name;
            ct.Description = request.Description;
            ct.Percentage = request.Percentage;
            ct.CalculationUnit = request.CalculationUnit;
            ct.PricePerUnit = request.PricePerUnit;
            ct.UnitInContract = request.UnitInContract;
            ct.UnitUsed = request.UnitUsed;
            ct.IsExceed = request.IsExceed;
            ct.EndDate = request.EndDate;
            ct.NoDate = request.NoDate;
            ct.ParentTaskId = request.ParentTaskId;
            ct.ConstructionTaskCategoryId = request.ConstructionTaskCategoryId;
            ct.ProjectId = request.ProjectId;
            ct.PaymentStageId = request.PaymentStageId;
            ct.InteriorItemId = request.InteriorItemId;
            ct.ConstructionTaskDesignId = request.ConstructionTaskDesignId;
            ct.Status = request.Status;
            
            _repository.Update(ct);
        }
        public void UpdateConstructionTaskStatus(Guid id, ProjectTaskStatus status)
        {
            var ct = _repository.GetById(id) ?? throw new Exception("This object is not existed!");

            ct.Status = status; 
            
            _repository.Update(ct);
        }
    }
}
*/