using BusinessObject.Enums;
using BusinessObject.Models;
using IDBMS_API.DTOs.Request.BookingRequest;
using IDBMS_API.DTOs.Request;
using IDBMS_API.Services;
using Repository.Interfaces;

public class ProjectService
{
    private readonly IProjectRepository _projectRepo;
    private readonly IProjectParticipationRepository _participationRepo;
    private readonly IPaymentStageDesignRepository _paymentStageDesignRepo;
    private readonly IPaymentStageRepository _paymentStageRepo;
    private readonly IProjectDocumentRepository _projectDocumentRepo;
    private readonly ISiteRepository _siteRepo;
    private readonly IFloorRepository _floorRepo;
    private readonly IRoomRepository _roomRepo;
    private readonly IProjectTaskRepository _taskRepo;

    public ProjectService(
            IProjectRepository projectRepo,
            IProjectParticipationRepository participationRepo,
            IPaymentStageDesignRepository paymentStageDesignRepo,
            IPaymentStageRepository paymentStageRepo,
            IProjectDocumentRepository projectDocumentRepo,
            ISiteRepository siteRepo,
            IFloorRepository floorRepo,
            IRoomRepository roomRepo,
            IProjectTaskRepository taskRepo)
    {
        _projectRepo = projectRepo;
        _participationRepo = participationRepo;
        _paymentStageDesignRepo = paymentStageDesignRepo;
        _paymentStageRepo = paymentStageRepo;
        _projectDocumentRepo = projectDocumentRepo;
        _siteRepo = siteRepo;
        _floorRepo = floorRepo;
        _roomRepo = roomRepo;
        _taskRepo = taskRepo;
    }

    public IEnumerable<Project> GetAll()
    {
        return _projectRepo.GetAll();
    }

    public Project? GetById(Guid id)
    {
        return _projectRepo.GetById(id) ?? throw new Exception("This object is not existed!");
    }

    /*public Project? BookDecorProject(BookingDecorProjectRequest request)
    {
        try
        {
            var project = new Project
            {
                Id = Guid.NewGuid(),
                CreatedDate = DateTime.Now,
                Status = ProjectStatus.Draft,
                AdvertisementStatus = AdvertisementStatus.None,
                Type = ProjectType.Decor,

                Name = request.Name,
                CompanyName = request.CompanyName,
                CompanyAddress = request.CompanyAddress,
                CompanyCode = request.CompanyCode,
                Description = request.Description,
                ProjectCategoryId = request.ProjectCategoryId,
                ProjectDesignId = request.ProjectDesignId,
                EstimatedPrice = request.EstimatedPrice,
                EstimateBusinessDay = request.EstimateBusinessDay,
                Language = request.Language,
            };

            var draftProject = _projectRepo.Save(project);
            if (draftProject != null)
            {

                ProjectParticipationService participationService = new ProjectParticipationService(_participationRepo);
                participationService.CreateProjectOwnerParticipation(draftProject.Id, request.UserId);

                if (request.Documents != null)
                {
                    ProjectDocumentService documentService = new ProjectDocumentService(_projectDocumentRepo);
                    documentService.CreateBookProjectDocument(draftProject.Id, request.Documents);
                }

                if (request.Sites != null)
                {
                    SiteService siteService = new SiteService(_siteRepo, _floorRepo, _roomRepo, _taskRepo);
                    siteService.CreateBookSite(draftProject.Id, request.Sites);
                }

                if (request.ProjectDesignId != null)
                {
                    PaymentStageDesignService paymentStageDesignService = new PaymentStageDesignService(_paymentStageDesignRepo);
                    var listStageDesigns = paymentStageDesignService.GetByProjectDesignId((int)request.ProjectDesignId);
                    if (listStageDesigns.Any() && draftProject.EstimatedPrice.HasValue)
                    {
                        PaymentStageService paymentStageService = new PaymentStageService(_paymentStageRepo);
                        paymentStageService.CreatePaymentStageByDesign(draftProject.Id, draftProject.EstimatedPrice.Value, (List<PaymentStageDesign>)listStageDesigns);
                    }
                }

                var projectCreated = _projectRepo.GetById(draftProject.Id) ?? throw new Exception("This object is not existed!");
                projectCreated.Status = ProjectStatus.PendingConfirmation;
                _projectRepo.Update(projectCreated);
                return projectCreated;
            }
            return draftProject;
        }
        catch
        {
            throw new Exception("Fail to book project!");
        }
    }
    public Project? BookConstructionProject(BookingConstructionProjectRequest request)
    {
        try
        {
            var project = new Project
            {
                Id = Guid.NewGuid(),
                CreatedDate = DateTime.Now,
                Status = ProjectStatus.Draft,
                AdvertisementStatus = AdvertisementStatus.None,
                Type = ProjectType.Decor,

                Name = request.Name,
                CompanyName = request.CompanyName,
                CompanyAddress = request.CompanyAddress,
                CompanyCode = request.CompanyCode,
                Description = request.Description,
                ProjectCategoryId = request.ProjectCategoryId,
                BasedOnDecorProjectId = request.BasedOnDecorProjectId,
                Language = request.Language,
            };

            var draftProject = _projectRepo.Save(project);
            if (draftProject != null)
            {
                ProjectParticipationService participationService = new ProjectParticipationService(_participationRepo);
                participationService.CreateProjectOwnerParticipation(draftProject.Id, request.UserId);

                if (request.Documents != null)
                {
                    ProjectDocumentService documentService = new ProjectDocumentService(_projectDocumentRepo);
                    documentService.CreateBookProjectDocument(draftProject.Id, request.Documents);
                }

                var projectCreated = _projectRepo.GetById(draftProject.Id) ?? throw new Exception("This object is not existed!");
                projectCreated.Status = ProjectStatus.PendingConfirmation;
                _projectRepo.Update(projectCreated);
                return projectCreated;
            }
            return draftProject;
        }
        catch
        {
            throw new Exception("Fail to book project!");
        }
    }*/

    public void UpdateProject(Guid id, ProjectRequest request)
    {
        var p = _projectRepo.GetById(id) ?? throw new Exception("This object is not existed!");

        p.Name = request.Name;
        p.Description = request.Description;
        p.Type = request.Type;
        p.ProjectCategoryId = request.ProjectCategoryId;
        p.UpdatedDate = DateTime.Now;
        p.NoStage = request.NoStage;
        p.EstimatedPrice = request.EstimatedPrice;
        p.FinalPrice = request.FinalPrice;
        p.TotalWarrantyPaid = request.TotalWarrantyPaid;
        p.EstimateBusinessDay= request.EstimateBusinessDay;
        p.CurrentStageId = request.CurrentStageId;
        p.Language = request.Language;
        p.Status = request.Status;

        _projectRepo.Update(p);
    }

    public void UpdateProjectStatus(Guid id, ProjectStatus status)
    {
        var project = _projectRepo.GetById(id) ?? throw new Exception("Not existed");

        project.Status = status;

        _projectRepo.Update(project);
    }

    public void UpdateProjectAdvertisementStatus(Guid id, AdvertisementStatus status)
    {
        var project = _projectRepo.GetById(id) ?? throw new Exception("Not existed");

        project.AdvertisementStatus = status;

        _projectRepo.Update(project);
    }
}
