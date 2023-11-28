using IDBMS_API.DTOs.Request;
using BusinessObject.Models;
using Repository.Interfaces;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace IDBMS_API.Services
{
    public class WarrantyClaimService
    {
        private readonly IWarrantyClaimRepository _repository;

        public WarrantyClaimService(IWarrantyClaimRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<WarrantyClaim> GetAll()
        {
            return _repository.GetAll();
        }
        public WarrantyClaim? GetById(Guid id)
        {
            return _repository.GetById(id) ?? throw new Exception("This object is not existed!");
        }
        public IEnumerable<WarrantyClaim?> GetByUserId(Guid id)
        {
            return _repository.GetByUserId(id) ?? throw new Exception("This object is not existed!");
        }
        public IEnumerable<WarrantyClaim?> GetByProjectId(Guid id)
        {
            return _repository.GetByProjectId(id) ?? throw new Exception("This object is not existed!");
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
        }
        public void DeleteWarrantyClaim(Guid id)
        {
            var wc = _repository.GetById(id) ?? throw new Exception("This object is not existed!");

            wc.IsDeleted = true;

            _repository.Update(wc);
        }
    }

}
