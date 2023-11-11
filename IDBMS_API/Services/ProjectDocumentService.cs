using BusinessObject.DTOs.Request;
using BusinessObject.Models;
using Repository.Implements;
using Repository.Interfaces;

namespace IDBMS_API.Services
{
    public class ProjectDocumentService
    {
        private readonly IProjectDocumentRepository _repository;
        public ProjectDocumentService(IProjectDocumentRepository repository)
        {
            _repository = repository;
        }
        public IEnumerable<ProjectDocument> GetAll()
        {
            return _repository.GetAll();
        }
        public ProjectDocument? GetById(Guid id)
        {
            return _repository.GetById(id) ?? throw new Exception("This object is not existed!");
        }
        public IEnumerable<ProjectDocument?> GetByFilter(Guid? projectId, int? documentTemplateId)
        {
            return _repository.GetByFilter(projectId, documentTemplateId) ?? throw new Exception("This object is not existed!");
        }
        public ProjectDocument? CreateProjectDocument(ProjectDocumentRequest request)
        {
            var pd = new ProjectDocument
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                Url = request.Url,
                CreatedDate = DateTime.Now,
                Category = request.Category,
                ProjectId = request.ProjectId,
                ProjectDocumentTemplateId = request.ProjectDocumentTemplateId,
                IsPublicAdvertisement = request.IsPublicAdvertisement,
                IsDeleted = false,
            };

            var pdCreated = _repository.Save(pd);
            return pdCreated;
        }
        public void UpdateProjectDocument(Guid id, ProjectDocumentRequest request)
        {
            var pd = _repository.GetById(id) ?? throw new Exception("This object is not existed!");

            pd.Name = request.Name;
            pd.Description = request.Description;
            pd.Url = request.Url;
            pd.CreatedDate = request.CreatedDate;
            pd.Category = request.Category;
            pd.ProjectId = request.ProjectId;
            pd.ProjectDocumentTemplateId = request.ProjectDocumentTemplateId;
            pd.IsPublicAdvertisement = request.IsPublicAdvertisement;

            _repository.Update(pd);
        }
        public void DeleteProjectDocument(Guid id)
        {
            var pd = _repository.GetById(id) ?? throw new Exception("This object is not existed!");

            pd.IsDeleted = true;

            _repository.Update(pd);
        }
    }
}
