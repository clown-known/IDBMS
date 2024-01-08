using BusinessObject.Enums;
using BusinessObject.Models;
using IDBMS_API.DTOs.Request;
using IDBMS_API.Services;
using Repository.Interfaces;
using Microsoft.OData.Edm;
using UnidecodeSharpFork;
using DocumentFormat.OpenXml.Office2010.Excel;
using IDBMS_API.Supporters.TimeHelper;

public class ProjectService
{
    private readonly IProjectRepository _projectRepo;
    private readonly IFloorRepository _floorRepo;
    private readonly IRoomRepository _roomRepo;
    private readonly IRoomTypeRepository _roomTypeRepo;
    private readonly IProjectTaskRepository _projectTaskRepo;
    private readonly IPaymentStageRepository _stageRepo;
    private readonly IProjectDesignRepository _projectDesignRepo;
    private readonly IPaymentStageDesignRepository _stageDesignRepo;
    private readonly ITransactionRepository _transactionRepo;
    private readonly ITaskDesignRepository _taskDesignRepo;
    private readonly ITaskCategoryRepository _taskCategoryRepo;

    public ProjectService(
        IProjectRepository projectRepo,
        IRoomRepository roomRepo,
        IRoomTypeRepository roomTypeRepo,
        IProjectTaskRepository projectTaskRepo,
        IPaymentStageRepository stageRepo,
        IProjectDesignRepository projectDesignRepo,
        IPaymentStageDesignRepository stageDesignRepo,
        IFloorRepository floorRepo,
        ITransactionRepository transactionRepo,
        ITaskDesignRepository taskDesignRepo,
        ITaskCategoryRepository taskCategoryRepo)
    {
        _projectRepo = projectRepo;
        _roomRepo = roomRepo;
        _roomTypeRepo = roomTypeRepo;
        _projectTaskRepo = projectTaskRepo;
        _stageRepo = stageRepo;
        _projectDesignRepo = projectDesignRepo;
        _stageDesignRepo = stageDesignRepo;
        _floorRepo = floorRepo;
        _transactionRepo = transactionRepo;
        _taskDesignRepo = taskDesignRepo;
        _taskCategoryRepo = taskCategoryRepo;
    }


    private IEnumerable<Project> Filter(IEnumerable<Project> list,
            ProjectType? type, ProjectStatus? status, string? name)
    {
        IEnumerable<Project> filteredList = list;

        if (type != null)
        {
            filteredList = filteredList.Where(item => item.Type == type);
        }

        if (status != null)
        {
            filteredList = filteredList.Where(item => item.Status == status);
        }

        if (name != null)
        {
            filteredList = filteredList.Where(item => (item.Name != null && item.Name.Unidecode().IndexOf(name.Unidecode(), StringComparison.OrdinalIgnoreCase) >= 0));
        }

        return filteredList;
    }

    public IEnumerable<Project> GetAll(ProjectType? type, ProjectStatus? status, string? name)
    {
        var list = _projectRepo.GetAll();

        return Filter(list, type, status, name);
    }

    public Project? GetById(Guid id)
    {
        return _projectRepo.GetById(id) ?? throw new Exception("This project id is not existed!");
    }    
    public IEnumerable<Project> GetBySiteId(Guid id, ProjectType? type, ProjectStatus? status, string? name)
    {
        var list = _projectRepo.GetBySiteId(id) ?? throw new Exception("This project id is not found!");

        return Filter(list, type, status, name);
    }

    public IEnumerable<Project> GetRecentProjects()
    {
        return _projectRepo.GetRecentProjects();
    }

    public IEnumerable<Project> GetRecentProjectsByUserId(Guid id)
    {
        return _projectRepo.GetRecentProjectsByUserId(id);
    }

    public IEnumerable<Project> GetOngoingProjects()
    {
        return _projectRepo.GetOngoingProjects();
    }    
    
    public IEnumerable<Project> GetOngoingProjectsByUserId(Guid id)
    {
        return _projectRepo.GetOngoingProjectsByUserId(id);
    }

    public Dictionary<ProjectStatus, int> CountParticipationsByProjectStatus(Guid userId)
    {
        var list = _projectRepo.GetRecentProjectsByUserId(userId);

        var allStatusValues = Enum.GetValues(typeof(ProjectStatus)).Cast<ProjectStatus>();

        var statusCounts = allStatusValues
            .ToDictionary(status => status, status => list.Count(project => project.Status == status));

        return statusCounts;
    }

    public Project? CreateProject(ProjectRequest request)
    {
        var newProject = new Project
        {
            Name = request.Name,
            Description = request.Description,
            Type = request.Type,
            ProjectCategoryId = request.ProjectCategoryId,
            CreatedAdminUsername = request.CreatedAdminUsername,
            CreatedByAdminId = request.CreatedByAdminId,
            EstimatedPrice = 0,
            FinalPrice = 0,
            TotalWarrantyPaid = 0,
            AmountPaid= 0,
            Area = 0,
            Language = request.Language,
            Status = request.Status,
            AdvertisementStatus = request.AdvertisementStatus,
            SiteId = request.SiteId,
            CreatedDate = DateTime.Now 
        };

        if (request.Type == ProjectType.Construction && request.BasedOnDecorProjectId != null)
        {
            newProject.BasedOnDecorProjectId = request.BasedOnDecorProjectId;

            var createdProject = _projectRepo.Save(newProject);

            FloorService floorService = new (_projectRepo, _roomRepo, _roomTypeRepo, _projectTaskRepo, _stageRepo, _projectDesignRepo, _stageDesignRepo, _floorRepo, _transactionRepo, _taskDesignRepo, _taskCategoryRepo);
            floorService.DuplicateFloorsByProjectId(createdProject.Id, request.BasedOnDecorProjectId.Value);

            return createdProject;
        }

        return _projectRepo.Save(newProject);
    }

    public void UpdateProject(Guid id, ProjectRequest request)
    {
        var p = _projectRepo.GetById(id) ?? throw new Exception("This project id is not existed!");

        p.Name = request.Name;
        p.Description = request.Description;
        p.Type = request.Type;
        p.ProjectCategoryId = request.ProjectCategoryId;
        p.Language = request.Language;
        p.Status = request.Status;
        p.AdvertisementStatus = request.AdvertisementStatus;
        p.SiteId= request.SiteId;
        p.UpdatedDate = TimeHelper.GetTime(DateTime.Now);

        _projectRepo.Update(p);
    }

    public void UpdateProjectDataByTask(Guid id, decimal estimatePrice, decimal finalPrice, int estimateBusinessDay)
    {
        var project = _projectRepo.GetById(id) ?? throw new Exception("This project id is not existed!");

        project.EstimatedPrice = estimatePrice;
        project.FinalPrice = finalPrice;
        project.UpdatedDate = TimeHelper.GetTime(DateTime.Now);
        project.EstimateBusinessDay = estimateBusinessDay;

        _projectRepo.Update(project);
    }

    public void UpdateProjectDataByRoom(Guid id, double totalArea)
    {
        var project = _projectRepo.GetById(id) ?? throw new Exception("This project id is not existed!");

        project.Area = (double)totalArea;
        project.UpdatedDate = TimeHelper.GetTime(DateTime.Now);

        _projectRepo.Update(project);
    }

    public void UpdateProjectDataByWarrantyClaim(Guid id, decimal totalWarrantyPaid)
    {
        var project = _projectRepo.GetById(id) ?? throw new Exception("This project id is not existed!");

        project.TotalWarrantyPaid = totalWarrantyPaid;
        project.UpdatedDate = TimeHelper.GetTime(DateTime.Now);

        _projectRepo.Update(project);
    }

    public void UpdateProjectAmountPaid(Guid id, decimal totalPaid)
    {
        var project = _projectRepo.GetById(id) ?? throw new Exception("This project id is not existed!");

        project.AmountPaid = totalPaid;
        project.UpdatedDate = TimeHelper.GetTime(DateTime.Now);

        _projectRepo.Update(project);
    }

    public void UpdateProjectTotalPenaltyFee(Guid id, decimal totalPenaltyFee)
    {
        var project = _projectRepo.GetById(id) ?? throw new Exception("This project id is not existed!");

        project.TotalPenaltyFee = totalPenaltyFee;
        project.UpdatedDate = TimeHelper.GetTime(DateTime.Now);

        _projectRepo.Update(project);
    }

    public void UpdateProjectWarrantyPeriodEndTime(Guid id, DateTime? warrantyPeriodEndTime)
    {
        var project = _projectRepo.GetById(id) ?? throw new Exception("This project id is not existed!");

        project.WarrantyPeriodEndTime = warrantyPeriodEndTime;
        project.UpdatedDate = TimeHelper.GetTime(DateTime.Now);

        _projectRepo.Update(project);
    }

    public void UpdateProjectStatus(Guid id, ProjectStatus status)
    {
        var project = _projectRepo.GetById(id) ?? throw new Exception("This project id is not existed!");

        project.Status = status;
        project.UpdatedDate = TimeHelper.GetTime(DateTime.Now);

        _projectRepo.Update(project);
    }
}
