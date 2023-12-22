using BLL.Services;
using BusinessObject.Enums;
using BusinessObject.Models;
using IDBMS_API.Constants;
using IDBMS_API.DTOs.Request;
using IDBMS_API.DTOs.Response;
using IDBMS_API.Supporters.File;
using Microsoft.AspNetCore.Mvc;
using Repository.Implements;

namespace IDBMS_API.Services
{
    public class ContractService
    {
        ProjectRepository _projectRepository;
        ProjectDocumentRepository _projectDocumentRepository;
        FileSupporter fileSupporter;
        DocumentTemplateRepository templateRepository;
        byte[] _dataSample;
        byte[] _file;
        FirebaseService firebaseService;
        public ContractService()
        {
            _projectDocumentRepository = new ProjectDocumentRepository();
            _projectRepository = new ProjectRepository();
            firebaseService = new FirebaseService();
            templateRepository = new DocumentTemplateRepository();
        }
        public async Task<byte[]> GenNewConstractForCompany(ContractRequest request)
        {
            _dataSample = await firebaseService.DownloadFile("ContractForCompany.docx", null, FileType.Contract, true);
            _file = FileSupporter.GenContractForCompanyFileBytes(_dataSample, request);
            return _file;

        }
        public async Task<byte[]> GenNewConstractForCustomer(ContractForCustomerRequest request)
        {
            
            _dataSample = await firebaseService.DownloadFile("ContractForCustomer.docx", null, FileType.Contract, true);
            _file =  FileSupporter.GenContractForCustomerFileBytes(_dataSample,request);
            return _file; 
        }
        public ContractForCompanyResponse GetDataForCompanyContract(Guid projectid)
        {
            try
            {
                ProjectRepository projectRepository = new ProjectRepository();
                Project project = projectRepository.GetByIdWithSite(projectid);
                User owner = project.ProjectParticipations.Where(p => p.Role == ParticipationRole.ProductOwner).FirstOrDefault().User;
                Site site = project.Site;
                DocumentTemplateRepository documentTemplateRepository = new DocumentTemplateRepository();
                var doc = documentTemplateRepository.getByType(DocumentTemplateType.Contract);
                ContractForCompanyResponse contractForCompanyResponse = new ContractForCompanyResponse
                {
                    ACompanyAddress = site.Address,
                    AOwnerName = owner.Name,
                    APhone = site.ContactPhone,
                    ACompanyPosition = owner.JobPosition,
                    ACompanyCode = site.CompanyCode,
                    ACompanyName = owner.CompanyName,
                    AEmail = owner.Email,
                    BCompanyPhone = doc.CompanyPhone,
                    BCompanyAddress = doc.CompanyAddress,
                    BCompanyName = doc.CompanyName,
                    BRepresentedBy = doc.RepresentedBy,
                    BSwiftCode = doc.SwiftCode,
                    BEmail = doc.Email,
                    BPosition = doc.Position,
                    EstimateDays = project.EstimateBusinessDay.Value,
                    ProjectName = project.Name,
                    Value = project.EstimatedPrice.Value
                };
                return contractForCompanyResponse;
            }catch(Exception e)
            {
                return null;
            }
        }
        public ContractForCustomerResponse GetDataForCustomerContract(Guid projectid)
        {
            try
            {
                ProjectRepository projectRepository = new ProjectRepository();
                Project project = projectRepository.GetByIdWithSite(projectid);
                User owner = project.ProjectParticipations.Where(p => p.Role == ParticipationRole.ProductOwner).FirstOrDefault().User;
                Site site = project.Site;
                DocumentTemplateRepository documentTemplateRepository = new DocumentTemplateRepository();
                var doc = documentTemplateRepository.getByType(DocumentTemplateType.Contract);
                ContractForCustomerResponse contractForCustomerResponse = new ContractForCustomerResponse
                {
                    Address = site.Address,
                    CustomerName = owner.Name,
                    Phone = site.ContactPhone,
                    DateOfBirth = owner.DateOfBirth,
                    Email = owner.Email,
                    BCompanyPhone = doc.CompanyPhone,
                    BCompanyAddress = doc.CompanyAddress,
                    BCompanyName = doc.CompanyName,
                    BRepresentedBy = doc.RepresentedBy,
                    BSwiftCode = doc.SwiftCode,
                    BEmail = doc.Email,
                    BPosition = doc.Position,
                    EstimateDays = project.EstimateBusinessDay.Value,
                    ProjectName = project.Name,
                    Value = project.EstimatedPrice.Value
                };
                return contractForCustomerResponse;
            }catch(Exception e)
            {
                return null;
            }
        }
        
        public async Task<byte[]> DownloadContract(Guid projectid)
        {
            var project = _projectRepository.GetById(projectid);
            ProjectDocument d = project.ProjectDocuments.Where(d => d.Category == ProjectDocumentCategory.Contract).FirstOrDefault();
            return await firebaseService.DownloadFileByDownloadUrl(d.Url);
        }
        public async Task<bool> UploadContract(Guid projectId,[FromForm] IFormFile file)
        {
            if (file == null) return false;
            var project = _projectRepository.GetById(projectId);
            try
            {
                string link = await firebaseService.UploadContract(file, projectId);
                DocumentTemplateRepository documentTemplateRepository = new DocumentTemplateRepository();
                var temp = documentTemplateRepository.getByType(DocumentTemplateType.Contract);
                var currentDoc = _projectDocumentRepository.GetContractById(projectId);
                if (currentDoc != null)
                {
                    currentDoc.Url = link;
                    currentDoc.Category = ProjectDocumentCategory.Contract;
                    currentDoc.CreatedDate = DateTime.Now;
                    currentDoc.ProjectDocumentTemplateId = temp.Id;
                    currentDoc.IsDeleted = false;
                    _projectDocumentRepository.Update(currentDoc);
                }
                else
                {
                    _projectDocumentRepository.Save(new ProjectDocument
                    {
                        Category = ProjectDocumentCategory.Contract,
                        CreatedDate = DateTime.Now,
                        IsDeleted = false,
                        Name = "Contract",
                        Url = link,
                        ProjectId = projectId,
                        ProjectDocumentTemplateId = temp.Id,
                        IsPublicAdvertisement = project.AdvertisementStatus == AdvertisementStatus.Public,
                    });
                }
                return true;
            }catch(Exception e)
            {
                return false;
            }
        }
    }
}
