using BusinessObject.DTOs.Request;
using BusinessObject.Enums;
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
            var obj = new ProjectDesign
            {
                MinBudget = request.MinBudget,
                MaxBudget = request.MaxBudget,
                EstimateBusinessDay= request.EstimateBusinessDay,
                Name = request.Name,
                Description = request.Description,
                ProjectType= request.ProjectType,
                IsHidden = request.IsHidden,
            };
            var objCreated = _repository.Save(obj);
            return objCreated;
        }
        public void UpdateProjectDesign(int id, ProjectDesignRequest request)
        {
            var obj = _repository.GetById(id) ?? throw new Exception("This object is not existed!");

            obj.MinBudget = request.MinBudget;
            obj.MaxBudget = request.MaxBudget;
            obj.EstimateBusinessDay = request.EstimateBusinessDay;
            obj.Name = request.Name;
            obj.Description = request.Description;
            obj.ProjectType = request.ProjectType;
            obj.IsHidden = request.IsHidden;

            _repository.Update(obj);
        }
        public void UpdateProjectDesign(int id, bool isHidden)
        {
            var obj = _repository.GetById(id) ?? throw new Exception("This object is not existed!");

            obj.IsHidden = isHidden;

            _repository.Update(obj);
        }
    }
}
