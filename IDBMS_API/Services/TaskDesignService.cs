using IDBMS_API.DTOs.Request;
using BusinessObject.Models;
using Repository.Interfaces;
using BusinessObject.Enums;
using UnidecodeSharpFork;

namespace IDBMS_API.Services
{
    public class TaskDesignService
    {
        private readonly ITaskDesignRepository _repository;
        public TaskDesignService(ITaskDesignRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<TaskDesign> Filter(IEnumerable<TaskDesign> list,
            string? code, string? name, int? taskCategoryId)
        {
            IEnumerable<TaskDesign> filteredList = list;

            if (code != null)
            {
                filteredList = filteredList.Where(item => (item.Name != null && item.Name.Unidecode().IndexOf(code.Unidecode(), StringComparison.OrdinalIgnoreCase) >= 0));
            }

            if (name != null)
            {
                filteredList = filteredList.Where(item => (item.Name != null && item.Name.Unidecode().IndexOf(name.Unidecode(), StringComparison.OrdinalIgnoreCase) >= 0));
            }

            if (taskCategoryId != null)
            {
                filteredList = filteredList.Where(item => item.TaskCategoryId == taskCategoryId);
            }

            return filteredList;
        }

        public IEnumerable<TaskDesign> GetAll(string? code, string? name, int? taskCategoryId)
        {
            var list = _repository.GetAll();

            return Filter(list, code, name, taskCategoryId);
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
                EnglishName = request.EnglishName,
                Description = request.Description,
                EnglishDescription = request.EnglishDescription,
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
            ctd.EnglishName = request.EnglishName;
            ctd.Description = request.Description;
            ctd.EnglishDescription = request.EnglishDescription;
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
