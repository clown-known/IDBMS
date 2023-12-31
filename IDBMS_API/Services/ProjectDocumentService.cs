using IDBMS_API.DTOs.Request;
using BusinessObject.Models;
using Repository.Implements;
using Repository.Interfaces;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;
using BusinessObject.Enums;
using UnidecodeSharpFork;
using DocumentFormat.OpenXml.Wordprocessing;

namespace IDBMS_API.Services
{
    public class ProjectDocumentService
    {
        private readonly IProjectDocumentRepository _repository;
        public ProjectDocumentService(IProjectDocumentRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<ProjectDocument> Filter(IEnumerable<ProjectDocument> list,
            ProjectDocumentCategory? category, string? name)
        {
            IEnumerable<ProjectDocument> filteredList = list;

            if (category != null)
            {
                filteredList = filteredList.Where(item => item.Category == category);
            }

            if (name != null)
            {
                filteredList = filteredList.Where(item => item.Name.Unidecode().IndexOf(name.Unidecode(), StringComparison.OrdinalIgnoreCase) >= 0);
            }

            return filteredList;
        }

        public IEnumerable<ProjectDocument> GetAll(ProjectDocumentCategory? category, string? name)
        {
            var list = _repository.GetAll();

            return Filter(list, category, name);
        }
        public ProjectDocument? GetById(Guid id)
        {
            return _repository.GetById(id) ?? throw new Exception("This object is not existed!");
        }
        public IEnumerable<ProjectDocument?> GetByFilter(Guid? projectId, int? documentTemplateId)
        {
            return _repository.GetByFilter(projectId, documentTemplateId) ?? throw new Exception("This object is not existed!");
        }
        public IEnumerable<ProjectDocument?> GetByProjectId(Guid id, ProjectDocumentCategory? category, string? name)
        {
            var list = _repository.GetByProjectId(id);

            return Filter(list, category, name);
        }
        public async Task<ProjectDocument?> CreateProjectDocument(ProjectDocumentRequest request)
        {
            var pd = new ProjectDocument
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                CreatedDate = DateTime.Now,
                Category = request.Category,
                ProjectId = request.ProjectId,
                ProjectDocumentTemplateId = request.ProjectDocumentTemplateId,
                IsPublicAdvertisement = request.IsPublicAdvertisement,
                IsDeleted = false,
            };

            if (request.file != null)
            {
                FirebaseService s = new FirebaseService();
                string link = await s.UploadDocument(request.file, request.ProjectId);

                pd.Url = link;
            }

            var pdCreated = _repository.Save(pd);
            return pdCreated;
        }

        public async void UpdateProjectDocument(Guid id, ProjectDocumentRequest request)
        {
            var pd = _repository.GetById(id) ?? throw new Exception("This object is not existed!");

            if (request.file != null)
            {
                FirebaseService s = new FirebaseService();
                string link = await s.UploadDocument(request.file, request.ProjectId);

                pd.Url = link;
            }

            pd.Name = request.Name;
            pd.Description = request.Description;
            pd.CreatedDate = DateTime.Now;
            pd.Category = request.Category;
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
