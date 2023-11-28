using IDBMS_API.DTOs.Request;
using BusinessObject.Models;
using Repository.Interfaces;
using BLL.Services;

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
        public async Task<TaskCategory?> CreateTaskCategory(TaskCategoryRequest request)
        {
            FirebaseService s = new FirebaseService();
            string link = await s.UploadImage(request.IconImage);
            var ctc = new TaskCategory
            {
                Name = request.Name,
                EnglishName = request.EnglishName,
                Description = request.Description,
                EnglishDescription = request.EnglishDescription,
                ProjectType = request.ProjectType,
                IconImageUrl = link,
                IsDeleted = false,
            };
            var ctcCreated = _repository.Save(ctc);
            return ctcCreated;
        }
        public async void UpdateTaskCategory(int id, TaskCategoryRequest request)
        {
            var ctc = _repository.GetById(id) ?? throw new Exception("This object is not existed!");
            FirebaseService s = new FirebaseService();
            string link = await s.UploadImage(request.IconImage);
            ctc.Name = request.Name;
            ctc.EnglishName = request.EnglishName;
            ctc.Description = request.Description;
            ctc.EnglishDescription = request.EnglishDescription;
            ctc.ProjectType = request.ProjectType;
            ctc.IconImageUrl = link;

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
