using BLL.Services;
using BusinessObject.Enums;
using BusinessObject.Models;
using IDBMS_API.Constants;
using IDBMS_API.Supporters.File;
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
        public async Task<byte[]> GenNewConstract(Guid Projectid)
        {
            var project = _projectRepository.GetById(Projectid);
            if(project.CompanyName == null || project.CompanyName.Equals(""))
            {
                _dataSample = await firebaseService.DownloadFile("ContractForCustomer.docx", null,FileType.Contract, true);
                ProjectDocumentTemplate template =  templateRepository.getByType(DocumentTemplateType.Contract);
                _file =  FileSupporter.GenContractForCustomerFileBytes(_dataSample, project,template, 2, 1, 1);
                return _file;
                //string fileName = "Contract1";
                //string link = await firebaseService.UploadByByte(_file,fileName,Projectid,nameof(ProjectDocumentCategory.Contract));
            }
            else
            {
                _dataSample = await firebaseService.DownloadFile("ContractForCompany.docx", null, FileType.Contract, true);
                ProjectDocumentTemplate template = templateRepository.getByType(DocumentTemplateType.Contract);
                _file =  FileSupporter.GenContractForCompanyFileBytes(_dataSample, project,template, 2, 1, 1);
               // string fileName = "Contract2";
                //string link = await firebaseService.UploadByByte(_file, fileName, Projectid, nameof(ProjectDocumentCategory.Contract));
                return _file;
            }
            
        }
        public async Task<byte[]> DownloadContract(Guid projectid)
        {
            var project = _projectRepository.GetById(projectid);
            ProjectDocument d = project.ProjectDocuments.Where(d => d.Category == ProjectDocumentCategory.Contract).FirstOrDefault();
            return await firebaseService.DownloadFileByDownloadUrl(d.Url);
        }
    }
}
