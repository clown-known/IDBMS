using BusinessObject.DTOs.Request.CreateRequests;
using BusinessObject.Models;
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
        public ProjectCategory? CreateProjectCategory(ProjectCategoryRequest projectCategory)
        {
            var pc = new ProjectCategory
            {
                Name = projectCategory.Name,
                IconImageUrl = projectCategory.IconImageUrl,
                IsHidden = projectCategory.IsHidden,
            };
            var pcCreated = projectCategoryRepository.Save(pc);
            return pcCreated;
        }
        public void UpdateProjectCategory(int id, ProjectCategoryRequest projectCategory)
        {
            var rtCheck = projectCategoryRepository.GetById(id) ?? throw new Exception("This Project Category not existed");
            var pc = new ProjectCategory
            {
                Name = projectCategory.Name,
                IconImageUrl = projectCategory.IconImageUrl,
                IsHidden = projectCategory.IsHidden,
            };
            projectCategoryRepository.Update(pc);
        }
        public void UpdateProjectCategoryStatus(int id, bool isHidden)
        {
            var projectCategory = projectCategoryRepository.GetById(id) ?? throw new Exception("This Project Category not existed");
            var pc = new ProjectCategory
            {
                Name = projectCategory.Name,
                IconImageUrl = projectCategory.IconImageUrl,
                IsHidden = isHidden,
            };
            projectCategoryRepository.Update(pc);
        }
    }
}
