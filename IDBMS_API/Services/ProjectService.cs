using BusinessObject.Enums;
using BusinessObject.Models;
using IDBMS_API.DTOs.Request;
using IDBMS_API.Services;
using Repository.Interfaces;
using Microsoft.OData.Edm;
using UnidecodeSharpFork;
using DocumentFormat.OpenXml.Office2010.Excel;

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

    public ProjectService(
            IProjectRepository projectRepo,
            IRoomRepository roomRepo,
            IRoomTypeRepository roomTypeRepo,
            IProjectTaskRepository projectTaskRepo,
            IPaymentStageRepository stageRepo,
            IProjectDesignRepository projectDesignRepo,
            IPaymentStageDesignRepository stageDesignRepo,
            IFloorRepository floorRepo)
    {
        _projectRepo = projectRepo;
        _roomRepo = roomRepo;
        _roomTypeRepo = roomTypeRepo;
        _projectTaskRepo = projectTaskRepo;
        _stageRepo = stageRepo;
        _projectDesignRepo = projectDesignRepo;
        _stageDesignRepo = stageDesignRepo;
        _floorRepo = floorRepo;
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
        return _projectRepo.GetById(id) ?? throw new Exception("This object is not existed!");
    }    
    public IEnumerable<Project> GetBySiteId(Guid id, ProjectType? type, ProjectStatus? status, string? name)
    {
        var list = _projectRepo.GetBySiteId(id) ?? throw new Exception("This object is not found!");

        return Filter(list, type, status, name);
    }

    public IEnumerable<Project> GetRecentProjects()
    {
        return _projectRepo.GetRecentProjects();
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

            FloorService floorService = new (_projectRepo, _roomRepo, _roomTypeRepo, _projectTaskRepo, _stageRepo, _projectDesignRepo, _stageDesignRepo, _floorRepo);
            floorService.DuplicateFloorsByProjectId(createdProject.Id, request.BasedOnDecorProjectId.Value);

            return createdProject;
        }

        return _projectRepo.Save(newProject);
    }

    public void UpdateProject(Guid id, ProjectRequest request)
    {
        var p = _projectRepo.GetById(id) ?? throw new Exception("This object is not existed!");

        p.Name = request.Name;
        p.Description = request.Description;
        p.Type = request.Type;
        p.ProjectCategoryId = request.ProjectCategoryId;
        p.Language = request.Language;
        p.Status = request.Status;
        p.AdvertisementStatus = request.AdvertisementStatus;
        p.SiteId= request.SiteId;
        p.UpdatedDate = DateTime.Now;

        _projectRepo.Update(p);
    }

    public void UpdateProjectDataByTask(Guid id, decimal estimatePrice, decimal finalPrice, int estimateBusinessDay)
    {
        var project = _projectRepo.GetById(id) ?? throw new Exception("This object is not existed!");

        project.EstimatedPrice = (decimal?)estimatePrice;
        project.FinalPrice = (decimal?)finalPrice;
        project.UpdatedDate = DateTime.Now;
        project.EstimateBusinessDay = estimateBusinessDay;

        _projectRepo.Update(project);
    }

    public void UpdateProjectDataByRoom(Guid id, double totalArea)
    {
        var project = _projectRepo.GetById(id) ?? throw new Exception("This object is not existed!");

        project.Area = (double)totalArea;
        project.UpdatedDate = DateTime.Now;

        _projectRepo.Update(project);
    }

    public void UpdateProjectDataByWarrantyClaim(Guid id, decimal totalWarrantyPaid)
    {
        var project = _projectRepo.GetById(id) ?? throw new Exception("This object is not existed!");

        project.TotalWarrantyPaid = totalWarrantyPaid;
        project.UpdatedDate = DateTime.Now;

        _projectRepo.Update(project);
    }

    public void UpdateProjectStatus(Guid id, ProjectStatus status)
    {
        var project = _projectRepo.GetById(id) ?? throw new Exception("Not existed");

        project.Status = status;
        project.UpdatedDate = DateTime.Now;

        _projectRepo.Update(project);
    }
}
