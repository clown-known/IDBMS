using BLL.Services;
using BusinessObject.Enums;
using BusinessObject.Models;
using IDBMS_API.DTOs.Request;
using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces;
using UnidecodeSharpFork;

namespace IDBMS_API.Services
{
    public class ProjectCategoryService
    {
        private readonly IProjectCategoryRepository _repository;
        public ProjectCategoryService (IProjectCategoryRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<ProjectCategory> Filter(IEnumerable<ProjectCategory> list,
           bool? isHidden, string? name)
        {
            IEnumerable<ProjectCategory> filteredList = list;

            if (isHidden != null)
            {
                filteredList = filteredList.Where(item => item.IsHidden == isHidden);
            }

            if (name != null)
            {
                filteredList = filteredList.Where(item => (item.Name != null && item.Name.Unidecode().IndexOf(name.Unidecode(), StringComparison.OrdinalIgnoreCase) >= 0));
            }

            return filteredList;
        }

        public IEnumerable<ProjectCategory> GetAll(bool? isHidden, string? name)
        {
            var list = _repository.GetAll();

            return Filter(list, isHidden, name);
        }
        public ProjectCategory? GetById(int id)
        {
            return _repository.GetById(id);
        }
        public async Task<ProjectCategory?> CreateProjectCategory([FromForm] ProjectCategoryRequest projectCategory)
        {
            FirebaseService s = new FirebaseService();
            string link = await s.UploadImage(projectCategory.IconImage);
            var pc = new ProjectCategory
            {
                Name = projectCategory.Name,
                EnglishName = projectCategory.EnglishName,
                IconImageUrl = link,
                IsHidden = false,
            };

            var pcCreated = _repository.Save(pc);
            return pcCreated;
        }
        public async void UpdateProjectCategory(int id, [FromForm] ProjectCategoryRequest projectCategory)
        {
            FirebaseService s = new FirebaseService();
            string link = await s.UploadImage(projectCategory.IconImage);
            var pc = _repository.GetById(id) ?? throw new Exception("This object is not existed!");
            pc.Name = projectCategory.Name;
            pc.EnglishName = projectCategory.EnglishName;
            pc.IconImageUrl = link;
            
            _repository.Update(pc);
        }
        public void UpdateProjectCategoryStatus(int id, bool isHidden)
        {
            var pc = _repository.GetById(id) ?? throw new Exception("This object is not existed!");

            pc.IsHidden = isHidden;

            _repository.Update(pc);
        }
    }
}
