using BusinessObject.DTOs.Request;
using BusinessObject.Models;
using Repository.Interfaces;

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
        public WarrantyClaim? CreateWarrantyClaim(WarrantyClaimRequest request)
        {
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
                ConfirmationDocument = request.ConfirmationDocument,
                ProjectId = request.ProjectId,
                UserId = request.UserId,
                IsDeleted = false,
            };
            var wcCreated = _repository.Save(wc);
            return wcCreated;
        }
        public void UpdateWarrantyClaim(Guid id, WarrantyClaimRequest request)
        {
            var wc = _repository.GetById(id) ?? throw new Exception("This object is not existed!");

            wc.Name = request.Name;
            wc.Reason = request.Reason;
            wc.Solution = request.Solution;
            wc.Note = request.Note;
            wc.TotalPaid = request.TotalPaid;
            wc.IsCompanyCover = request.IsCompanyCover;
            wc.EndDate = request.EndDate;
            wc.ConfirmationDocument = request.ConfirmationDocument;
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
