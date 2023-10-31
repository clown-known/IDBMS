using BusinessObject.DTOs.Request;
using BusinessObject.Models;
using Repository.Interfaces;

namespace IDBMS_API.Services
{
    public class ConstructionTaskService
    {
        private readonly IConstructionTaskRepository _constructionTaskRepository;
        public ConstructionTaskService(IConstructionTaskRepository constructionTaskRepository)
        {
            _constructionTaskRepository = constructionTaskRepository;
        }
        public IEnumerable<ConstructionTask> GetAll()
        {
            return _constructionTaskRepository.GetAll();
        }
        public ConstructionTask? GetById(Guid id)
        {
            return _constructionTaskRepository.GetById(id);
        }
        public ConstructionTask? CreateConstructionTask(ConstructionTaskRequest request)
        {
            var ct = new ConstructionTask
            {
                Code = request.Code,
                Name = request.Name,
                Description = request.Description,
                Percentage = request.Percentage,
                CalculationUnit = request.CalculationUnit,
                PricePerUnit = request.PricePerUnit,
                UnitInContract = request.UnitInContract,
                UnitUsed = request.UnitUsed,
                IsExceed = request.IsExceed,
                StartedDate = request.StartedDate,
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
            var ctCreated = _constructionTaskRepository.Save(ct);
            return ctCreated;
        }
        public void UpdateConstructionTask(ConstructionTaskRequest request)
        {
            var ct = new ConstructionTask
            {
                Code = request.Code,
                Name = request.Name,
                Description = request.Description,
                Percentage = request.Percentage,
                CalculationUnit = request.CalculationUnit,
                PricePerUnit = request.PricePerUnit,
                UnitInContract = request.UnitInContract,
                UnitUsed = request.UnitUsed,
                IsExceed = request.IsExceed,
                StartedDate = request.StartedDate,
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
            _constructionTaskRepository.Update(ct);
        }
        public void DeleteConstructionTask(Guid id)
        {
            _constructionTaskRepository.DeleteById(id);
        }
    }
}
