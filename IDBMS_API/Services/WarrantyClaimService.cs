using IDBMS_API.DTOs.Request;
using BusinessObject.Models;
using Repository.Interfaces;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;
using Azure.Core;
using BusinessObject.Enums;
using UnidecodeSharpFork;

namespace IDBMS_API.Services
{
    public class WarrantyClaimService
    {
        private readonly IWarrantyClaimRepository _repository;
        private readonly IProjectRepository _projectRepo;

        public WarrantyClaimService(IWarrantyClaimRepository repository, IProjectRepository projectRepo)
        {
            _repository = repository;
            _projectRepo = projectRepo;
        }

        public IEnumerable<WarrantyClaim> Filter(IEnumerable<WarrantyClaim> list,
            bool? isCompanyCover, string? name)
        {
            IEnumerable<WarrantyClaim> filteredList = list;

            if (isCompanyCover != null)
            {
                filteredList = filteredList.Where(item => item.IsCompanyCover == isCompanyCover);
            }

            if (name != null)
            {
                filteredList = filteredList.Where(item => (item.Name != null && item.Name.Unidecode().IndexOf(name.Unidecode(), StringComparison.OrdinalIgnoreCase) >= 0));
            }

            return filteredList;
        }

        public IEnumerable<WarrantyClaim> GetAll(bool? isCompanyCover, string? name)
        {
            var list = _repository.GetAll();

            return Filter(list, isCompanyCover, name);
        }

        public WarrantyClaim? GetById(Guid id)
        {
            return _repository.GetById(id) ?? throw new Exception("This object is not existed!");
        }

        public IEnumerable<WarrantyClaim?> GetByUserId(Guid id, bool? isCompanyCover, string? name)
        {
            var list = _repository.GetByUserId(id) ?? throw new Exception("This object is not existed!");

            return Filter(list, isCompanyCover, name);
        }

        public IEnumerable<WarrantyClaim?> GetByProjectId(Guid id, bool? isCompanyCover, string? name)
        {
            var list = _repository.GetByProjectId(id) ?? throw new Exception("This object is not existed!");

            return Filter(list, isCompanyCover, name);
        }

        public void UpdateProjectTotalWarrantyPaid(Guid projectId)
        {
            var claimsInProject = _repository.GetByProjectId(projectId);

            decimal totalPaid = 0;

            if (claimsInProject != null && claimsInProject.Any())
            {
                totalPaid = claimsInProject.Sum(claim =>
                {
                    if (claim != null && claim.IsDeleted != true)
                    {
                        return claim.TotalPaid;
                    }
                    return 0;
                });
            }

            ProjectService projectService = new(_projectRepo);
            projectService.UpdateProjectDataByWarrantyClaim(projectId, totalPaid);

        }

        public async Task<WarrantyClaim?> CreateWarrantyClaim([FromForm]WarrantyClaimRequest request)
        {
            string link = "";
            if(request.ConfirmationDocument != null)
            {
                FirebaseService s = new FirebaseService();
                link = await s.UploadDocument(request.ConfirmationDocument,request.ProjectId);
            }
            var wc = new WarrantyClaim
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Reason = request.Reason,
                Solution = request.Solution,
                Note = request.Note,
                TotalPaid = request.TotalPaid,
                IsCompanyCover = request.IsCompanyCover,
                CreatedDate = DateTime.Now,
                EndDate = request.EndDate,
                ConfirmationDocument = link,
                ProjectId = request.ProjectId,
                UserId = request.UserId,
                IsDeleted = false,
            };
            var wcCreated = _repository.Save(wc);

            UpdateProjectTotalWarrantyPaid(request.ProjectId);

            return wcCreated;
        }

        public async void UpdateWarrantyClaim(Guid id, [FromForm] WarrantyClaimRequest request)
        {
            var wc = _repository.GetById(id) ?? throw new Exception("This object is not existed!");
            string link = wc.ConfirmationDocument;
            if (request.ConfirmationDocument != null)
            {
                FirebaseService s = new FirebaseService();
                link = await s.UploadDocument(request.ConfirmationDocument, request.ProjectId);
            }
            wc.Name = request.Name;
            wc.Reason = request.Reason;
            wc.Solution = request.Solution;
            wc.Note = request.Note;
            wc.TotalPaid = request.TotalPaid;
            wc.IsCompanyCover = request.IsCompanyCover;
            wc.EndDate = request.EndDate;
            wc.ConfirmationDocument = link;
            wc.ProjectId = request.ProjectId;
            wc.UserId = request.UserId;

            _repository.Update(wc);

            UpdateProjectTotalWarrantyPaid(request.ProjectId);
        }
        public void DeleteWarrantyClaim(Guid id, Guid projectId)
        {
            var wc = _repository.GetById(id) ?? throw new Exception("This object is not existed!");

            wc.IsDeleted = true;

            _repository.Update(wc);

            UpdateProjectTotalWarrantyPaid(projectId);
        }
    }

}
