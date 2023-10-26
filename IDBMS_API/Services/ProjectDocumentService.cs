using BusinessObject.DTOs.Request;
using BusinessObject.Models;
using Repository.Implements;
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
                Id = Guid.NewGuid(),
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
        public void UpdateProjectDocument(Guid id, ProjectDocumentRequest request)
        {
            var pd = projectDocumentRepository.GetById(id) ?? throw new Exception("This object is not existed!");

            pd.Name = request.Name;
            pd.Description = request.Description;
            pd.Url = request.Url;
            pd.CreatedDate = request.CreatedDate;
            pd.Category = request.Category;
            pd.ProjectId = request.ProjectId;
            pd.ConstructionTaskReportId = request.ConstructionTaskReportId;
            pd.DecorProgressReportId = request.DecorProgressReportId;
            pd.ProjectDocumentTemplateId = request.ProjectDocumentTemplateId;
            pd.IsDeleted = request.IsDeleted;
            
            projectDocumentRepository.Update(pd);
        }
        public void UpdateProjectDocumentStatus(Guid id, bool isDeleted)
        {
            var pd = projectDocumentRepository.GetById(id) ?? throw new Exception("This object is not existed!");
            pd.IsDeleted = isDeleted;
            projectDocumentRepository.Update(pd);
        }
        public void DeleteProjectDocument(Guid id)
        {
            projectDocumentRepository.DeleteById(id);
        }
    }
}
