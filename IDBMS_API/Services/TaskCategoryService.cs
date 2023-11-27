using IDBMS_API.DTOs.Request;
using BusinessObject.Models;
using Repository.Interfaces;

namespace IDBMS_API.Services
{
    public class TaskCategoryService
    {
        private readonly ITaskCategoryRepository _repository;

        public TaskCategoryService(ITaskCategoryRepository repository)
        {
            _repository = repository;
        }
        public IEnumerable<TaskCategory> GetAll()
        {
            return _repository.GetAll();
        }
        public TaskCategory? GetById(int id)
        {
            return _repository.GetById(id) ?? throw new Exception("This object is not existed!");
        }
        public TaskCategory? CreateTaskCategory(TaskCategoryRequest request)
        {
            var ctc = new TaskCategory
            {
                Name = request.Name,
                EnglishName = request.EnglishName,
                Description = request.Description,
                EnglishDescription = request.EnglishDescription,
                ProjectType = request.ProjectType,
                IconImageUrl = request.IconImageUrl,
                IsDeleted = false,
            };
            var ctcCreated = _repository.Save(ctc);
            return ctcCreated;
        }
        public void UpdateTaskCategory(int id, TaskCategoryRequest request)
        {
            var ctc = _repository.GetById(id) ?? throw new Exception("This object is not existed!");

            ctc.Name = request.Name;
            ctc.EnglishName = request.EnglishName;
            ctc.Description = request.Description;
            ctc.EnglishDescription = request.EnglishDescription;
            ctc.ProjectType = request.ProjectType;
            ctc.IconImageUrl = request.IconImageUrl;

            _repository.Update(ctc);
        }
        public void DeleteTaskCategory(int id)
        {
            var ctc = _repository.GetById(id) ?? throw new Exception("This object is not existed!");

            ctc.IsDeleted = true;
            //cons task design

            _repository.Update(ctc);
        }
    }

}
