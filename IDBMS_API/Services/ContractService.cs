using BLL.Services;
using BusinessObject.Enums;
using BusinessObject.Models;
using DocumentFormat.OpenXml.Office.CoverPageProps;
using IDBMS_API.Constants;
using IDBMS_API.DTOs.Request;
using IDBMS_API.DTOs.Response;
using IDBMS_API.Supporters.File;
using IDBMS_API.Supporters.TimeHelper;
using Microsoft.AspNetCore.Mvc;
using Repository.Implements;

namespace IDBMS_API.Services
{
    public class ContractService
    {
        ProjectRepository _projectRepository;
        ProjectDocumentRepository _projectDocumentRepository;
        DocumentTemplateRepository _templateRepository;
        byte[] _dataSample;
        byte[] _file;
        FirebaseService firebaseService;
        public ContractService()
        {
            _projectDocumentRepository = new ProjectDocumentRepository();
            _projectRepository = new ProjectRepository();
            firebaseService = new FirebaseService();
            _templateRepository = new DocumentTemplateRepository();
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
                Project? project = projectRepository.GetById(projectid);
                if (project == null) throw new Exception("Cannot found project!");
                var ownerParticipation = project.ProjectParticipations.FirstOrDefault(p => p.Role == ParticipationRole.ProductOwner);

                if (ownerParticipation == null)
                    throw new Exception("Project owner is not found!");

                User owner = ownerParticipation.User;
                Site? site = project.Site;
                if (site == null) throw new Exception("Site is null");
                DocumentTemplateRepository documentTemplateRepository = new DocumentTemplateRepository();
                var doc = documentTemplateRepository.getByType(DocumentTemplateType.Contract);
                ContractForCompanyResponse contractForCompanyResponse = new ContractForCompanyResponse
                {
                    ACompanyAddress = site.Address,
                    AOwnerName = owner.Name,
                    APhone = site.ContactPhone,
                    ACompanyPosition = owner.JobPosition??"",
                    ACompanyCode = site.CompanyCode??"",
                    ACompanyName = owner.CompanyName ?? "",
                    AEmail = owner.Email,

                    EstimateDays = project.EstimateBusinessDay != null? project.EstimateBusinessDay.Value:0,
                    ProjectName = project.Name,
                    Value = project.EstimatedPrice != null ? project.EstimatedPrice.Value : 0
                };
                if(doc != null){
                    contractForCompanyResponse.BCompanyPhone = doc.CompanyPhone;
                    contractForCompanyResponse.BCompanyAddress = doc.CompanyAddress;
                    contractForCompanyResponse.BCompanyName = doc.CompanyName;
                    contractForCompanyResponse.BRepresentedBy = doc.RepresentedBy;
                    contractForCompanyResponse.BSwiftCode = doc.SwiftCode;
                    contractForCompanyResponse.BEmail = doc.Email;
                    contractForCompanyResponse.BPosition = doc.Position;
                }
                return contractForCompanyResponse;
            }catch(Exception e)
            {
                throw;
            }
        }
        public ContractForCustomerResponse GetDataForCustomerContract(Guid projectid)
        {
            try
            {

                ProjectRepository projectRepository = new ProjectRepository();
                Project? project = projectRepository.GetById(projectid);
                if (project == null) throw new Exception("Cannot found project!");
                var ownerParticipation = project.ProjectParticipations.FirstOrDefault(p => p.Role == ParticipationRole.ProductOwner);

                if (ownerParticipation == null)
                    throw new Exception("Project owner is not found!");

                User owner = ownerParticipation.User;
                Site? site = project.Site;
                if (site == null) throw new Exception("Site is null!");
                var doc = _templateRepository.getByType(DocumentTemplateType.Contract);
                ContractForCustomerResponse contractForCustomerResponse = new ContractForCustomerResponse
                {
                    Address = site.Address,
                    CustomerName = owner.Name,
                    Phone = site.ContactPhone,
                    DateOfBirth = owner.DateOfBirth != null ? owner.DateOfBirth.Value : null,
                    Email = owner.Email,
                    EstimateDays = project.EstimateBusinessDay != null ? project.EstimateBusinessDay.Value : 0,
                    ProjectName = project.Name,
                    Value = project.EstimatedPrice != null ? project.EstimatedPrice.Value : 0,
                };
                if (doc != null)
                {
                    contractForCustomerResponse.BCompanyPhone = doc.CompanyPhone;
                    contractForCustomerResponse.BCompanyAddress = doc.CompanyAddress;
                    contractForCustomerResponse.BCompanyName = doc.CompanyName;
                    contractForCustomerResponse.BRepresentedBy = doc.RepresentedBy;
                    contractForCustomerResponse.BSwiftCode = doc.SwiftCode;
                    contractForCustomerResponse.BEmail = doc.Email;
                    contractForCustomerResponse.BPosition = doc.Position;
                }
                return contractForCustomerResponse;
            }catch(Exception e)
            {
                throw;
            }
        }
        
        public async Task<byte[]> DownloadContract(Guid projectid)
        {
            var project = _projectRepository.GetById(projectid);
            if (project == null) throw new Exception("Cannot found project");
            ProjectDocument? d = project.ProjectDocuments.Where(d => d.Category == ProjectDocumentCategory.Contract).FirstOrDefault();
            if (d == null) throw new Exception("Cannot found that document!");
            if (d.Url == null) throw new Exception("Document didn't have dowload url!");
            return await firebaseService.DownloadFileByDownloadUrl(d.Url);
        }
        public async Task<bool> UploadContract(Guid projectId, IFormFile file)
        {
            if (file == null) return false;
            var project = _projectRepository.GetById(projectId);
            if (project == null) throw new Exception("Cannot found project!");
            try
            {
                string link = await firebaseService.UploadContract(file, projectId);
                var temp = _templateRepository.getByType(DocumentTemplateType.Contract);
                if (temp == null) throw new Exception("Cannot get template");
                var currentDoc = _projectDocumentRepository.GetContractById(projectId);
                if (currentDoc != null)
                {
                    currentDoc.Url = link;
                    currentDoc.Category = ProjectDocumentCategory.Contract;
                    currentDoc.CreatedDate = TimeHelper.GetTime(DateTime.Now);
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
