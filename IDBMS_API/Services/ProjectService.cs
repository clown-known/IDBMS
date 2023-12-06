using BusinessObject.Enums;
using BusinessObject.Models;
using IDBMS_API.DTOs.Request;
using IDBMS_API.Services;
using Repository.Interfaces;
using Microsoft.OData.Edm;

public class ProjectService
{
    private readonly IProjectRepository _projectRepo;
    private readonly IRoomRepository _roomRepo;
    private readonly IRoomTypeRepository _roomTypeRepo;
    private readonly IProjectTaskRepository _taskRepo;
    private readonly IInteriorItemRepository _itemRepo;

    public ProjectService(IProjectRepository projectRepo, IRoomRepository roomRepo, 
                    IRoomTypeRepository roomTypeRepo, IProjectTaskRepository taskRepo,
                    IInteriorItemRepository itemRepo)
    {
        _projectRepo = projectRepo;
        _roomRepo = roomRepo;
        _roomTypeRepo = roomTypeRepo;
        _taskRepo = taskRepo;
        _itemRepo = itemRepo;
    }

    public IEnumerable<Project> GetAll()
    {
        return _projectRepo.GetAll();
    }

    public Project? GetById(Guid id)
    {
        return _projectRepo.GetById(id) ?? throw new Exception("This object is not existed!");
    }    
    public IEnumerable<Project> GetBySiteId(Guid id)
    {
        return _projectRepo.GetBySiteId(id) ?? throw new Exception("This object is not existed!");
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
            EstimatedPrice = request.EstimatedPrice,
            FinalPrice = request.FinalPrice,
            TotalWarrantyPaid = request.TotalWarrantyPaid,
            Area = request.Area,
            EstimateBusinessDay = request.EstimateBusinessDay,
            Language = request.Language,
            Status = request.Status,
            AdvertisementStatus = request.AdvertisementStatus,
            SiteId = request.SiteId,
            CreatedDate = DateTime.Now 
        };

        if (newProject.Type == ProjectType.Construction)
        {
            newProject.BasedOnDecorProjectId = request.BasedOnDecorProjectId;
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
        p.EstimatedPrice = request.EstimatedPrice;
        p.FinalPrice = request.FinalPrice;
        p.TotalWarrantyPaid = request.TotalWarrantyPaid;
        p.Area = request.Area;
        p.EstimateBusinessDay = request.EstimateBusinessDay;
        p.Language = request.Language;
        p.Status = request.Status;
        p.AdvertisementStatus = request.AdvertisementStatus;
        p.SiteId= request.SiteId;
        p.UpdatedDate = DateTime.Now;

        if (p.Type == ProjectType.Construction)
        {
            p.BasedOnDecorProjectId = request.BasedOnDecorProjectId;
        }

        _projectRepo.Update(p);
    }

    /*public void UpdateProjectEstimatePrice(Guid id)
    {
        var project = _projectRepo.GetById(id) ?? throw new Exception("This object is not existed!");
        decimal totalSumRooms = 0;

        RoomService roomService = new RoomService(_roomRepo, _roomTypeRepo);
        var roomsInProject = roomService.GetByProjectId(project.Id);

        if (roomsInProject != null)
        {
            totalSumRooms = roomsInProject.Sum(room => ((decimal?)room.PricePerArea ?? 0) * ((decimal?)room.Area ?? 0));
        }

        project.EstimatedPrice = totalSumRooms;
        project.UpdatedDate = DateTime.Now;

        _projectRepo.Update(project);
    }*/

    public void UpdateProjectEstimatePrice(Guid id)
    {
        var project = _projectRepo.GetById(id) ?? throw new Exception("This object is not existed!");
        decimal totalSum = 0;

        ProjectTaskService taskService = new ProjectTaskService(_taskRepo);
        var tasksInProject = taskService.GetByProjectId(project.Id);

        if (tasksInProject != null && tasksInProject.Any())
        {
            totalSum = tasksInProject.Sum(task =>
                ((decimal?)task.PricePerUnit ?? 0) *
                (decimal)(task.UnitUsed > task.UnitInContract ? task.UnitUsed : task.UnitInContract)
            );
        }

        project.EstimatedPrice = totalSum;
        project.UpdatedDate = DateTime.Now;

        _projectRepo.Update(project);
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
