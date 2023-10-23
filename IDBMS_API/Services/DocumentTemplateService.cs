using BusinessObject.DTOs.Request.CreateRequests;
using BusinessObject.Models;
using Repository.Interfaces;

namespace IDBMS_API.Services
{
    public class DocumentTemplateService
    {
        private readonly IProjectDocumentTemplateRepository repository;
        public DocumentTemplateService(IProjectDocumentTemplateRepository repository)
        {
            this.repository = repository;
        }
        public IEnumerable<ProjectDocumentTemplate> GetAll()
        {
            return repository.GetAll();
        }
        public ProjectDocumentTemplate? GetByID(int id)
        {
            return repository.GetById(id);
        }
        public async Task<ProjectDocumentTemplate?> CreateDocumentTemplate(ProjectDocumentTemplateRequest request)
        {
            var dt = new ProjectDocumentTemplate
            {
                Name = request.Name,
                Type = request.Type,
                Language = request.Language,
                CreatedDate = request.CreatedDate,
                UpdatedDate = request.UpdatedDate,
                CompanyName = request.CompanyName,
                CompanyAddress = request.CompanyAddress,
                CompanyPhone = request.CompanyPhone,
                CompanyFax = request.CompanyFax,
                TaxCode = request.TaxCode,
                AccountNo = request.AccountNo,
                AccountName = request.AccountName,
                BankBranchName = request.BankBranchName,
                BankBranchAddress = request.BankBranchAddress,
                SwiftCode = request.SwiftCode,
                RepresentedBy = request.RepresentedBy,
            };
            var dtCreated = repository.Save(dt);
            return dtCreated;
        }
        public async Task UpdateDocumentTemplate(ProjectDocumentTemplateRequest request)
        {
            var dt = new ProjectDocumentTemplate
            {
                Name = request.Name,
                Type = request.Type,
                Language = request.Language,
                CreatedDate = request.CreatedDate,
                UpdatedDate = request.UpdatedDate,
                CompanyName = request.CompanyName,
                CompanyAddress = request.CompanyAddress,
                CompanyPhone = request.CompanyPhone,
                CompanyFax = request.CompanyFax,
                TaxCode = request.TaxCode,
                AccountNo = request.AccountNo,
                AccountName = request.AccountName,
                BankBranchName = request.BankBranchName,
                BankBranchAddress = request.BankBranchAddress,
                SwiftCode = request.SwiftCode,
                RepresentedBy = request.RepresentedBy,
            };
            repository.Update(dt);
        }
        public async Task DeleteDocumentTemplate(int id)
        {
            repository.DeleteById(id);
        }
    }
}
