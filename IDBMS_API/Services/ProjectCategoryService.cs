using BLL.Services;
using BusinessObject.Models;
using IDBMS_API.DTOs.Request;
using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces;

namespace IDBMS_API.Services
{
    public class ProjectCategoryService
    {
        private readonly IProjectCategoryRepository projectCategoryRepository;
        public ProjectCategoryService (IProjectCategoryRepository projectCategoryRepository)
        {
            this.projectCategoryRepository = projectCategoryRepository;
        }
        public IEnumerable<ProjectCategory> GetAll()
        {
            return projectCategoryRepository.GetAll();
        }
        public ProjectCategory? GetById(int id)
        {
            return projectCategoryRepository.GetById(id);
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

            var pcCreated = projectCategoryRepository.Save(pc);
            return pcCreated;
        }
        public async void UpdateProjectCategory(int id, [FromForm] ProjectCategoryRequest projectCategory)
        {
            FirebaseService s = new FirebaseService();
            string link = await s.UploadImage(projectCategory.IconImage);
            var pc = projectCategoryRepository.GetById(id) ?? throw new Exception("This object is not existed!");
            pc.Name = projectCategory.Name;
            pc.EnglishName = projectCategory.EnglishName;
            pc.IconImageUrl = link;
            
            projectCategoryRepository.Update(pc);
        }
        public void UpdateProjectCategoryStatus(int id, bool isHidden)
        {
            var pc = projectCategoryRepository.GetById(id) ?? throw new Exception("This object is not existed!");

            pc.IsHidden = isHidden;

            projectCategoryRepository.Update(pc);
        }
    }
}
