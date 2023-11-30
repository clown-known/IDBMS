using BusinessObject.Enums;
using BusinessObject.Models;
using IDBMS_API.DTOs.Request;
using IDBMS_API.Services;
using Repository.Interfaces;
using Microsoft.OData.Edm;

public class ProjectService
{
    private readonly IProjectRepository _repository;

    public ProjectService(IProjectRepository repository)
    {
        _repository = repository;
    }

    public IEnumerable<Project> GetAll()
    {
        return _repository.GetAll();
    }

    public Project? GetById(Guid id)
    {
        return _repository.GetById(id) ?? throw new Exception("This object is not existed!");
    }    
    public IEnumerable<Project> GetBySiteId(Guid id)
    {
        return _repository.GetBySiteId(id) ?? throw new Exception("This object is not existed!");
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
            NoStage = request.NoStage,
            EstimatedPrice = request.EstimatedPrice,
            FinalPrice = request.FinalPrice,
            TotalWarrantyPaid = request.TotalWarrantyPaid,
            Area = request.Area,
            EstimateBusinessDay = request.EstimateBusinessDay,
            CurrentStageId = request.CurrentStageId,
            Language = request.Language,
            Status = request.Status,
            AdvertisementStatus = request.AdvertisementStatus,
            BasedOnDecorProjectId = request.BasedOnDecorProjectId,
            SiteId = request.SiteId,
            CreatedDate = DateTime.Now 
        };

        return _repository.Save(newProject);
    }

    public void UpdateProject(Guid id, ProjectRequest request)
    {
        var p = _repository.GetById(id) ?? throw new Exception("This object is not existed!");

        p.Name = request.Name;
        p.Description = request.Description;
        p.Type = request.Type;
        p.ProjectCategoryId = request.ProjectCategoryId;
        p.CreatedAdminUsername = request.CreatedAdminUsername;
        p.CreatedByAdminId = request.CreatedByAdminId;
        p.NoStage = request.NoStage;
        p.EstimatedPrice = request.EstimatedPrice;
        p.FinalPrice = request.FinalPrice;
        p.TotalWarrantyPaid = request.TotalWarrantyPaid;
        p.Area = request.Area;
        p.EstimateBusinessDay = request.EstimateBusinessDay;
        p.CurrentStageId = request.CurrentStageId;
        p.Language = request.Language;
        p.Status = request.Status;
        p.AdvertisementStatus = request.AdvertisementStatus;
        p.BasedOnDecorProjectId = request.BasedOnDecorProjectId;
        p.SiteId= request.SiteId;
        p.UpdatedDate = DateTime.Now;

        _repository.Update(p);
    }

    public void UpdateProjectStatus(Guid id, ProjectStatus status)
    {
        var project = _repository.GetById(id) ?? throw new Exception("Not existed");

        project.Status = status;

        _repository.Update(project);
    }

    public void UpdateProjectAdvertisementStatus(Guid id, AdvertisementStatus status)
    {
        var project = _repository.GetById(id) ?? throw new Exception("Not existed");

        project.AdvertisementStatus = status;

        _repository.Update(project);
    }
}
