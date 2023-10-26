using BusinessObject.DTOs.Request;
using BusinessObject.Models;
using Repository.Interfaces;

namespace IDBMS_API.Services
{
    public class ProjectDocumentService
    {
        private readonly IProjectDocumentRepository projectDocumentRepository;
        public ProjectDocumentService(IProjectDocumentRepository projectDocumentRepository)
        {
            this.projectDocumentRepository = projectDocumentRepository;
        }
        public IEnumerable<ProjectDocument> GetAll()
        {
            return projectDocumentRepository.GetAll();
        }
        public ProjectDocument? GetById(Guid id)
        {
            return projectDocumentRepository.GetById(id);
        }
        public IEnumerable<ProjectDocument?> GetByFilter(Guid? projectId, Guid? constructionTaskReportId, Guid? decorProgressReportId, int? documentTemplateId)
        {
            return projectDocumentRepository.GetByFilter(projectId, constructionTaskReportId, decorProgressReportId, documentTemplateId);
        }
        public ProjectDocument? CreateProjectDocument(ProjectDocumentRequest request)
        {
            var pd = new ProjectDocument
            {
                Name = request.Name,
                Description = request.Description,
                Url = request.Url,
                CreatedDate = request.CreatedDate,
                Category = request.Category,
                ProjectId = request.ProjectId,
                ConstructionTaskReportId = request.ConstructionTaskReportId,
                DecorProgressReportId = request.DecorProgressReportId,
                ProjectDocumentTemplateId = request.ProjectDocumentTemplateId,
                IsDeleted = request.IsDeleted,
            };
            var pdCreated = projectDocumentRepository.Save(pd);
            return pdCreated;
        }
        public void UpdateProjectDocument(ProjectDocumentRequest request)
        {
            var pd = new ProjectDocument
            {
                Name = request.Name,
                Description = request.Description,
                Url = request.Url,
                CreatedDate = request.CreatedDate,
                Category = request.Category,
                ProjectId = request.ProjectId,
                ConstructionTaskReportId = request.ConstructionTaskReportId,
                DecorProgressReportId = request.DecorProgressReportId,
                ProjectDocumentTemplateId = request.ProjectDocumentTemplateId,
                IsDeleted = request.IsDeleted,
            };
            projectDocumentRepository.Update(pd);
        }
        public void DeleteProjectDocument(Guid id)
        {
            projectDocumentRepository.DeleteById(id);
        }
    }
}
