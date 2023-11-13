using BusinessObject.DTOs.Request;
using BusinessObject.Models;
using Repository.Interfaces;

namespace IDBMS_API.Services
{
    public class ProjectDesignService
    {
        private readonly IProjectDesignRepository _repository;
        public ProjectDesignService(IProjectDesignRepository repository)
        {
            _repository = repository;
        }
        public IEnumerable<ProjectDesign> GetAll()
        {
            return _repository.GetAll();
        }
        public ProjectDesign? GetById(int id)
        {
            return _repository.GetById(id) ?? throw new Exception("This object is not existed!");
        }
        public ProjectDesign? CreateProjectDesign(ProjectDesignRequest request)
        {
            var dpd = new ProjectDesign
            {
                MinBudget = request.MinBudget,
                MaxBudget = request.MaxBudget,
                Name = request.Name,
                Description = request.Description,
                IsDeleted = false,
            };
            var dpdCreated = _repository.Save(dpd);
            return dpdCreated;
        }
        public void UpdateProjectDesign(int id, ProjectDesignRequest request)
        {
            var dpd = _repository.GetById(id) ?? throw new Exception("This object is not existed!");

            dpd.MinBudget = request.MinBudget;
            dpd.MaxBudget = request.MaxBudget;
            dpd.Name = request.Name;
            dpd.Description = request.Description;

            _repository.Update(dpd);
        }
        public void DeleteProjectDesign(int id)
        {
            var dpd = _repository.GetById(id) ?? throw new Exception("This object is not existed!");

            dpd.IsDeleted = true;

            _repository.Update(dpd);
        }
    }
}
