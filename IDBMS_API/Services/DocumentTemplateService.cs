using BusinessObject.DTOs.Request;
using BusinessObject.Models;
using Repository.Implements;
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
        public ProjectDocumentTemplate? CreateDocumentTemplate(ProjectDocumentTemplateRequest request)
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
        public void UpdateDocumentTemplate(int id, ProjectDocumentTemplateRequest request)
        {
            var dt = repository.GetById(id) ?? throw new Exception("This object is not existed!");
            dt.Name = request.Name;
            dt.Type = request.Type;
            dt.Language = request.Language;
            dt.CreatedDate = request.CreatedDate;
            dt.UpdatedDate = request.UpdatedDate;
            dt.CompanyName = request.CompanyName;
            dt.CompanyAddress = request.CompanyAddress;
            dt.CompanyPhone = request.CompanyPhone;
            dt.CompanyFax = request.CompanyFax;
            dt.TaxCode = request.TaxCode;
            dt.AccountNo = request.AccountNo;
            dt.AccountName = request.AccountName;
            dt.BankBranchName = request.BankBranchName;
            dt.BankBranchAddress = request.BankBranchAddress;
            dt.SwiftCode = request.SwiftCode;
            dt.RepresentedBy = request.RepresentedBy;

            repository.Update(dt);
        }
        public void DeleteDocumentTemplate(int id)
        {
            repository.DeleteById(id);
        }
    }
}
