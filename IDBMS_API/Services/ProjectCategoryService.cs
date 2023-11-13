using BusinessObject.DTOs.Request;
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
                EnglishName = projectCategory.EnglishName,
                IconImageUrl = projectCategory.IconImageUrl,
                IsHidden = false,
            };

            var pcCreated = projectCategoryRepository.Save(pc);
            return pcCreated;
        }
        public void UpdateProjectCategory(int id, ProjectCategoryRequest projectCategory)
        {
            var pc = projectCategoryRepository.GetById(id) ?? throw new Exception("This object is not existed!");
            pc.Name = projectCategory.Name;
            pc.EnglishName = projectCategory.EnglishName;
            pc.IconImageUrl = projectCategory.IconImageUrl;
            
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
