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
            string? codeOrName, int? taskCategoryId)
        {
            IEnumerable<TaskDesign> filteredList = list;

            if (codeOrName != null)
            {
                filteredList = filteredList.Where(item =>
                            (item.Code != null && item.Code.Unidecode().IndexOf(codeOrName.Unidecode(), StringComparison.OrdinalIgnoreCase) >= 0) ||
                            (item.Name != null && item.Name.Unidecode().IndexOf(codeOrName.Unidecode(), StringComparison.OrdinalIgnoreCase) >= 0));
            }

            if (taskCategoryId != null)
            {
                filteredList = filteredList.Where(item => item.TaskCategoryId == taskCategoryId);
            }

            return filteredList;
        }

        public IEnumerable<TaskDesign> GetAll(string? codeOrName, int? taskCategoryId)
        {
            var list = _repository.GetAll();

            return Filter(list, codeOrName, taskCategoryId);
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
