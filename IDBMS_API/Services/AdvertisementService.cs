using BusinessObject.Enums;
using BusinessObject.Models;
using IDBMS_API.DTOs.Request;
using Repository.Interfaces;
using UnidecodeSharpFork;

namespace IDBMS_API.Services
{
    public class AdvertisementService
    {
        private readonly IProjectRepository _projectRepo;
        private readonly IProjectDocumentRepository _documentRepo;

        public AdvertisementService(IProjectRepository projectRepo, IProjectDocumentRepository documentRepo)
        {
            _projectRepo = projectRepo;
            _documentRepo = documentRepo;
        }

        private IEnumerable<Project> Filter(IEnumerable<Project> list,
                ProjectType? type, AdvertisementStatus? status, string? name)
        {
            IEnumerable<Project> filteredList = list;

            if (type != null)
            {
                filteredList = filteredList.Where(item => item.Type == type);
            }

            if (status != null)
            {
                filteredList = filteredList.Where(item => item.AdvertisementStatus == status);
            }

            if (name != null)
            {
                filteredList = filteredList.Where(item => (item.Name != null && item.Name.Unidecode().IndexOf(name.Unidecode(), StringComparison.OrdinalIgnoreCase) >= 0));
            }

            return filteredList;
        }

        public IEnumerable<Project> GetAll(ProjectType? type, AdvertisementStatus? status, string? name)
        {
            var list = _projectRepo.GetAll();

            return Filter(list, type, status, name);
        }

        public Project? GetById(Guid id)
        {
            return _projectRepo.GetById(id) ?? throw new Exception("This object is not existed!");
        }

        public void UpdateProject(Guid id, ProjectRequest request)
        {
            var p = _projectRepo.GetById(id) ?? throw new Exception("This object is not existed!");

            p.Name = request.Name;
            p.Description = request.Description;
            p.Type = request.Type;
            p.ProjectCategoryId = request.ProjectCategoryId;
            p.Language = request.Language;
            p.Status = request.Status;
            p.AdvertisementStatus = request.AdvertisementStatus;
            p.SiteId = request.SiteId;
            p.UpdatedDate = DateTime.Now;

            if (p.Type == ProjectType.Construction)
            {
                p.BasedOnDecorProjectId = request.BasedOnDecorProjectId;
            }

            _projectRepo.Update(p);
        }

        public void UpdateProjectAdvertisementStatus(Guid id, AdvertisementStatus status)
        {
            var project = _projectRepo.GetById(id) ?? throw new Exception("Not existed");

            project.AdvertisementStatus = status;

            _projectRepo.Update(project);
        }
    }
}
