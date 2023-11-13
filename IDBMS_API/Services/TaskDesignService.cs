using BusinessObject.DTOs.Request;
using BusinessObject.Models;
using Repository.Interfaces;

namespace IDBMS_API.Services
{
    public class TaskDesignService
    {
        private readonly ITaskDesignRepository _repository;
        public TaskDesignService(ITaskDesignRepository repository)
        {
            _repository = repository;
        }
        public IEnumerable<TaskDesign> GetAll()
        {
            return _repository.GetAll();
        }
        public TaskDesign? GetById(int id)
        {
            return _repository.GetById(id) ?? throw new Exception("This object is not existed!");
        }
        public TaskDesign? CreateTaskDesign(TaskDesignRequest request)
        {
            var ctd = new TaskDesign
            {
                Code = request.Code,
                Name = request.Name,
                Description = request.Description,
                CalculationUnit = request.CalculationUnit,
                EstimatePricePerUnit = request.EstimatePricePerUnit,
                IsDeleted = false,
                InteriorItemCategoryId = request.InteriorItemCategoryId,
                TaskCategoryId = request.TaskCategoryId,
            };

            var ctdCreated = _repository.Save(ctd);
            return ctdCreated;
        }
        public void UpdateTaskDesign(int id, TaskDesignRequest request)
        {
            var ctd = _repository.GetById(id) ?? throw new Exception("This object is not existed!");
            ctd.Code = request.Code;
            ctd.Name = request.Name;
            ctd.Description = request.Description;
            ctd.CalculationUnit = request.CalculationUnit;
            ctd.EstimatePricePerUnit = request.EstimatePricePerUnit;
            ctd.InteriorItemCategoryId = request.InteriorItemCategoryId;
            ctd.TaskCategoryId = request.TaskCategoryId;

            _repository.Update(ctd);
        }
        public void DeleteTaskDesign(int id)
        {
            var ctd = _repository.GetById(id) ?? throw new Exception("This object is not existed!");

            ctd.IsDeleted = true;

            _repository.Update(ctd);
        }
    }

}
