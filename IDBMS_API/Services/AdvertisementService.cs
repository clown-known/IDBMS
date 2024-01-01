using BLL.Services;
using BusinessObject.Enums;
using BusinessObject.Models;
using DocumentFormat.OpenXml.Office2010.Excel;
using IDBMS_API.DTOs.Request;
using IDBMS_API.DTOs.Request.AdvertisementRequest;
using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces;
using System.Runtime.Intrinsics.Arm;
using UnidecodeSharpFork;
using static System.Net.Mime.MediaTypeNames;

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

        private IEnumerable<Project> FilterProject(IEnumerable<Project> list,
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

        private IEnumerable<ProjectDocument> FilterImage(IEnumerable<ProjectDocument> list,
        bool? isPublicAdvertisement)
        {
            IEnumerable<ProjectDocument> filteredList = list;

            if (isPublicAdvertisement != null)
            {
                filteredList = filteredList.Where(item => item.IsPublicAdvertisement == isPublicAdvertisement);
            }

            return filteredList;
        }

        public IEnumerable<Project> GetAllProjects(ProjectType? type, AdvertisementStatus? status, string? name)
        {
            var list = _projectRepo.GetAdvertisementAllowedProjects();

            return FilterProject(list, type, status, name);
        }
        
        public IEnumerable<ProjectDocument> GetImagesByProjectId(Guid projectId, bool? isPublicAdvertisement)
        {
            var list = _documentRepo.GetByProjectId(projectId).Where(d => d.Category == ProjectDocumentCategory.CompletionImage);

            return FilterImage(list, isPublicAdvertisement);
        }
        public Project GetAdProjectById(Guid id)
        {
            return _projectRepo.GetById(id) ?? throw new Exception("This project id is not existed!");
        }

        public async Task<Project?> CreateAdvertisementProject(AdvertisementProjectRequest request)
        {
            var newAdProject = new Project
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Type= request.Type,
                ProjectCategoryId= request.ProjectCategoryId,
                CreatedDate = DateTime.Now,
                CreatedAdminUsername= request.CreatedAdminUsername,
                CreatedByAdminId= request.CreatedByAdminId,
                FinalPrice = request.FinalPrice,
                AmountPaid = request.FinalPrice,
                Area = request.Area,
                Language= request.Language,
                AdvertisementStatus = AdvertisementStatus.Allowed,
                Status = ProjectStatus.Done,

                AdvertisementDescription= request.AdvertisementDescription,
                EnglishAdvertisementDescription= request.AdvertisementDescription,
            };

            var createdProject = _projectRepo.Save(newAdProject);

            var updateDescription = new AdvertisementDescriptionRequest
            {
                AdvertisementDescription = request.AdvertisementDescription,
                EnglishAdvertisementDescription = request.EnglishAdvertisementDescription,
                RepresentImage = request.RepresentImage,
            };

            UpdateAdProjectDescription(createdProject.Id, updateDescription);

            return createdProject;
        }

        public async Task CreateCompletionImage(List<AdvertisementImageRequest> requests)
        {
            if (requests.Any())
            {
                foreach (var request in requests)
                {
                    var image = new ProjectDocument
                    {
                        Id = Guid.NewGuid(),
                        Name = "AdvertisementImage",
                        CreatedDate = DateTime.Now,
                        Category = ProjectDocumentCategory.CompletionImage,
                        ProjectId = request.ProjectId,
                        IsPublicAdvertisement = request.IsPublicAdvertisement,
                        IsDeleted = false,
                    };

                    if (request.File != null)
                    {
                        FirebaseService s = new FirebaseService();
                        string link = await s.UploadDocument(request.File, request.ProjectId);

                        image.Url = link;
                    }

                    _documentRepo.Save(image);
                }
            }
        }

        public void DeleteCompletionImage(Guid documentId)
        {
            var pd = _documentRepo.GetById(documentId) ?? throw new Exception("This project document id is not existed!");

            pd.IsDeleted = true;

            _documentRepo.Update(pd);
        }

        public void UpdatePublicImage(Guid documentId, bool isPublicAdvertisement)
        {
            var pd = _documentRepo.GetById(documentId) ?? throw new Exception("This project document id is not existed!");

            pd.IsPublicAdvertisement = isPublicAdvertisement;

            _documentRepo.Update(pd);
        }

        public async void UpdateAdProjectDescription(Guid projectId, AdvertisementDescriptionRequest request)
        {
            var p = _projectRepo.GetById(projectId) ?? throw new Exception("This project id is not existed!");

            p.AdvertisementDescription = request.AdvertisementDescription;
            p.EnglishAdvertisementDescription = request.EnglishAdvertisementDescription;

            if (request.RepresentImage != null)
            {
                FirebaseService s = new FirebaseService();
                string link = await s.UploadDocument(request.RepresentImage, projectId);

                p.RepresentImageUrl = link;
            }

            _projectRepo.Update(p);
        }

        public void UpdateProjectAdvertisementStatus(Guid projectId, AdvertisementStatus status)
        {
            var project = _projectRepo.GetById(projectId) ?? throw new Exception("Not existed");

            project.AdvertisementStatus = status;

            _projectRepo.Update(project);
        }
    }
}
