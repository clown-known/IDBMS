using Azure.Core;
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
        public async Task<ProjectCategory?> CreateProjectCategory(ProjectCategoryRequest projectCategory)
        {
            var pc = new ProjectCategory
            {
                Name = projectCategory.Name,
                EnglishName = projectCategory.EnglishName,
                IsHidden = false,
            };

            if (projectCategory.IconImage != null)
            {
                FirebaseService s = new FirebaseService();
                string link = await s.UploadImage(projectCategory.IconImage);

                pc.IconImageUrl = link;
            }

            var pcCreated = _repository.Save(pc);
            return pcCreated;
        }
        public async void UpdateProjectCategory(int id, ProjectCategoryRequest projectCategory)
        {
            var pc = _repository.GetById(id) ?? throw new Exception("This project category id is not existed!");

            if (projectCategory.IconImage != null)
            {
                FirebaseService s = new FirebaseService();
                string link = await s.UploadImage(projectCategory.IconImage);

                pc.IconImageUrl = link;
            }

            pc.Name = projectCategory.Name;
            pc.EnglishName = projectCategory.EnglishName;
            
            _repository.Update(pc);
        }
        public void UpdateProjectCategoryStatus(int id, bool isHidden)
        {
            var pc = _repository.GetById(id) ?? throw new Exception("This project category id is not existed!");

            pc.IsHidden = isHidden;

            _repository.Update(pc);
        }
    }
}
