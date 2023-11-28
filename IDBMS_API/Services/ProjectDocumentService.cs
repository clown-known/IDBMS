using IDBMS_API.DTOs.Request;
using IDBMS_API.DTOs.Request.BookingRequest;
using BusinessObject.Models;
using Repository.Implements;
using Repository.Interfaces;
using BLL.Services;

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
        public IEnumerable<ProjectDocument?> GetByProjectId(Guid id)
        {
            return _repository.GetByProjectId(id) ?? throw new Exception("This object is not existed!");
        }
        public async Task<ProjectDocument?> CreateProjectDocument(ProjectDocumentRequest request)
        {
            FirebaseService s = new FirebaseService();
            string link = await s.UploadDocument(request.file,request.ProjectId);
            var pd = new ProjectDocument
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                Url = link,
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

        public async void CreateBookProjectDocument(Guid projectId, List<BookingDocumentRequest> requests)
        {
            FirebaseService s = new FirebaseService();
            foreach (var request in requests)
            {
                string link = "";
                if (request.file != null)
                {
                    link = await s.UploadBookingDocument(request.file,nameof(request.Category),projectId);
                }
                var pd = new ProjectDocument
                {
                    Id = Guid.NewGuid(),
                    Name = request.Name,
                    Description = request.Description,
                    Url = link,
                    CreatedDate = DateTime.Now,
                    Category = request.Category,
                    ProjectId = projectId,
                    IsPublicAdvertisement = false,
                    IsDeleted = false,
                };

                _repository.Save(pd);
            }
        }


        public async void UpdateProjectDocument(Guid id, ProjectDocumentRequest request)
        {
            var pd = _repository.GetById(id) ?? throw new Exception("This object is not existed!");
            FirebaseService s = new FirebaseService();
            string link = await s.UploadDocument(request.file, request.ProjectId);
            pd.Name = request.Name;
            pd.Description = request.Description;
            pd.Url = link;
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
