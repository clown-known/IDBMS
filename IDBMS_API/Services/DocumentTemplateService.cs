using IDBMS_API.DTOs.Request;
using BusinessObject.Models;
using Repository.Implements;
using Repository.Interfaces;
using BusinessObject.Enums;
using UnidecodeSharpFork;

namespace IDBMS_API.Services
{
    public class DocumentTemplateService
    {
        private readonly IProjectDocumentTemplateRepository _repository;
        public DocumentTemplateService(IProjectDocumentTemplateRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<ProjectDocumentTemplate> Filter(IEnumerable<ProjectDocumentTemplate> list,
           DocumentTemplateType? type, string? name)
        {
            IEnumerable<ProjectDocumentTemplate> filteredList = list;

            if (type != null)
            {
                filteredList = filteredList.Where(item => item.Type == type);
            }

            if (name != null)
            {
                filteredList = filteredList.Where(item => (item.Name != null && item.Name.Unidecode().IndexOf(name.Unidecode(), StringComparison.OrdinalIgnoreCase) >= 0));
            }

            return filteredList;
        }

        public IEnumerable<ProjectDocumentTemplate> GetAll(DocumentTemplateType? type, string? name)
        {
            var list = _repository.GetAll();

            return Filter(list, type, name);
        }
        public ProjectDocumentTemplate? GetById(int id)
        {
            return _repository.GetById(id) ?? throw new Exception("This object is not existed!");
        }
        public ProjectDocumentTemplate? CreateDocumentTemplate(ProjectDocumentTemplateRequest request)
        {
            var dt = new ProjectDocumentTemplate
            {
                Name = request.Name,
                Type = request.Type,
                Language = request.Language,
                CreatedDate = DateTime.Now,
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
                IsDeleted = false
            };
            var dtCreated = _repository.Save(dt);
            return dtCreated;
        }
        public void UpdateDocumentTemplate(int id, ProjectDocumentTemplateRequest request)
        {
            var dt = _repository.GetById(id) ?? throw new Exception("This object is not existed!");
            dt.Name = request.Name;
            dt.Type = request.Type;
            dt.Language = request.Language;
            dt.UpdatedDate = DateTime.Now;
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

            _repository.Update(dt);
        }
        public void DeleteDocumentTemplate(int id)
        {
            var dt = _repository.GetById(id) ?? throw new Exception("This object is not existed!");

            dt.IsDeleted = true;

            _repository.Update(dt);
        }
    }
}
