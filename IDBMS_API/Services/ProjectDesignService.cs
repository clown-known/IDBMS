using IDBMS_API.DTOs.Request;
using BusinessObject.Enums;
using BusinessObject.Models;
using Repository.Interfaces;
using UnidecodeSharpFork;

namespace IDBMS_API.Services
{
    public class ProjectDesignService
    {
        private readonly IProjectDesignRepository _repository;
        public ProjectDesignService(IProjectDesignRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<ProjectDesign> Filter(IEnumerable<ProjectDesign> list,
            ProjectType? type, string? name, bool? isHidden)
        {
            IEnumerable<ProjectDesign> filteredList = list;

            if (type != null)
            {
                filteredList = filteredList.Where(item => item.ProjectType == type);
            }

            if (name != null)
            {
                filteredList = filteredList.Where(item => (item.Name != null && item.Name.Unidecode().IndexOf(name.Unidecode(), StringComparison.OrdinalIgnoreCase) >= 0));
            }

            if (isHidden != null)
            {
                filteredList = filteredList.Where(item => item.IsHidden == isHidden);
            }

            return filteredList;
        }

        public IEnumerable<ProjectDesign> GetAll(ProjectType? type, string? name, bool? isHidden)
        {
            var list = _repository.GetAll();

            return Filter(list, type, name, isHidden);
        }
        public IEnumerable<ProjectDesign> GetByType(ProjectType type)
        {
            return _repository.GetByType(type);
        }

        public ProjectDesign? GetProjectDesignMapped(decimal estimatePrice, ProjectType type)
        {
            var designList = _repository.GetByType(type);

            var selectedDesign = designList.FirstOrDefault(design =>
                                estimatePrice >= design.MinBudget &&
                                estimatePrice <= design.MaxBudget);

            return selectedDesign;
        }

        public ProjectDesign? GetById(int id)
        {
            return _repository.GetById(id) ?? throw new Exception("This project design id is not existed!");
        }
        public ProjectDesign? CreateProjectDesign(ProjectDesignRequest request)
        {
            var obj = new ProjectDesign
            {
                MinBudget = request.MinBudget,
                MaxBudget = request.MaxBudget,
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
            var obj = _repository.GetById(id) ?? throw new Exception("This project design id is not existed!");

            obj.MinBudget = request.MinBudget;
            obj.MaxBudget = request.MaxBudget;
            obj.Name = request.Name;
            obj.Description = request.Description;
            obj.ProjectType = request.ProjectType;
            obj.IsHidden = request.IsHidden;

            _repository.Update(obj);
        }
        public void UpdateProjectDesign(int id, bool isHidden)
        {
            var obj = _repository.GetById(id) ?? throw new Exception("This project design id is not existed!");

            obj.IsHidden = isHidden;

            _repository.Update(obj);
        }
    }
}
