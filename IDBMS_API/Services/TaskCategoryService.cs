using IDBMS_API.DTOs.Request;
using BusinessObject.Models;
using Repository.Interfaces;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;
using BusinessObject.Enums;
using UnidecodeSharpFork;

namespace IDBMS_API.Services
{
    public class TaskCategoryService
    {
        private readonly ITaskCategoryRepository _repository;

        public TaskCategoryService(ITaskCategoryRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<TaskCategory> Filter(IEnumerable<TaskCategory> list,
            ProjectType? type, string? name)
        {
            IEnumerable<TaskCategory> filteredList = list;

            if (type != null)
            {
                filteredList = filteredList.Where(item => item.ProjectType == type);
            }

            if (name != null)
            {
                filteredList = filteredList.Where(item => (item.Name != null && item.Name.Unidecode().IndexOf(name.Unidecode(), StringComparison.OrdinalIgnoreCase) >= 0));
            }

            return filteredList;
        }

        public IEnumerable<TaskCategory> GetAll(ProjectType? type, string? name)
        {
            var list = _repository.GetAll();

            return Filter(list, type, name);
        }
        public TaskCategory? GetById(int id)
        {
            return _repository.GetById(id) ?? throw new Exception("This task category id is not existed!");
        }
        public async Task<TaskCategory?> CreateTaskCategory(TaskCategoryRequest request)
        {
            var ctc = new TaskCategory
            {
                Name = request.Name,
                EnglishName = request.EnglishName,
                Description = request.Description,
                EnglishDescription = request.EnglishDescription,
                ProjectType = request.ProjectType,
                IsDeleted = false,
            };

            if (request.IconImage != null)
            {
                FirebaseService s = new FirebaseService();
                string link = await s.UploadImage(request.IconImage);

                ctc.IconImageUrl = link;
            }

            var ctcCreated = _repository.Save(ctc);
            return ctcCreated;
        }
        public async void UpdateTaskCategory(int id, TaskCategoryRequest request)
        {
            var ctc = _repository.GetById(id) ?? throw new Exception("This task category id is not existed!");

            if (request.IconImage != null)
            {
                FirebaseService s = new FirebaseService();
                string link = await s.UploadImage(request.IconImage);

                ctc.IconImageUrl = link;
            }

            ctc.Name = request.Name;
            ctc.EnglishName = request.EnglishName;
            ctc.Description = request.Description;
            ctc.EnglishDescription = request.EnglishDescription;
            ctc.ProjectType = request.ProjectType;

            _repository.Update(ctc);
        }
        public void DeleteTaskCategory(int id)
        {
            var ctc = _repository.GetById(id) ?? throw new Exception("This task category id is not existed!");

            ctc.IsDeleted = true;
            //cons task design

            _repository.Update(ctc);
        }
    }

}
