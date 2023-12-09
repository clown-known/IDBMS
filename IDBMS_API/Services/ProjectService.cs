using BusinessObject.Enums;
using BusinessObject.Models;
using IDBMS_API.DTOs.Request;
using IDBMS_API.Services;
using Repository.Interfaces;
using Microsoft.OData.Edm;
using UnidecodeSharpFork;

public class ProjectService
{
    private readonly IProjectRepository _repository;

    public ProjectService(IProjectRepository repository)
    {
        _repository = repository;
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
        var list = _repository.GetAll();

        return Filter(list, type, status, name);
    }

    public Project? GetById(Guid id)
    {
        return _repository.GetById(id) ?? throw new Exception("This object is not existed!");
    }    
    public IEnumerable<Project> GetBySiteId(Guid id, ProjectType? type, ProjectStatus? status, string? name)
    {
        var list = _repository.GetBySiteId(id) ?? throw new Exception("This object is not found!");

        return Filter(list, type, status, name);
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

        if (newProject.Type == ProjectType.Construction)
        {
            newProject.BasedOnDecorProjectId = request.BasedOnDecorProjectId;
        }

        return _repository.Save(newProject);
    }

    public void UpdateProject(Guid id, ProjectRequest request)
    {
        var p = _repository.GetById(id) ?? throw new Exception("This object is not existed!");

        p.Name = request.Name;
        p.Description = request.Description;
        p.Type = request.Type;
        p.ProjectCategoryId = request.ProjectCategoryId;
        p.Language = request.Language;
        p.Status = request.Status;
        p.AdvertisementStatus = request.AdvertisementStatus;
        p.SiteId= request.SiteId;
        p.UpdatedDate = DateTime.Now;

        if (p.Type == ProjectType.Construction)
        {
            p.BasedOnDecorProjectId = request.BasedOnDecorProjectId;
        }

        _repository.Update(p);
    }

    public void UpdateProjectDataByTask(Guid id, decimal estimatePrice, decimal finalPrice, int estimateBusinessDay)
    {
        var project = _repository.GetById(id) ?? throw new Exception("This object is not existed!");

        project.EstimatedPrice = (decimal?)estimatePrice;
        project.FinalPrice = (decimal?)finalPrice;
        project.UpdatedDate = DateTime.Now;
        project.EstimateBusinessDay = estimateBusinessDay;

        _repository.Update(project);
    }

    public void UpdateProjectDataByRoom(Guid id, double totalArea)
    {
        var project = _repository.GetById(id) ?? throw new Exception("This object is not existed!");

        project.Area = (double)totalArea;
        project.UpdatedDate = DateTime.Now;

        _repository.Update(project);
    }

    public void UpdateProjectDataByWarrantyClaim(Guid id, decimal totalWarrantyPaid)
    {
        var project = _repository.GetById(id) ?? throw new Exception("This object is not existed!");

        project.TotalWarrantyPaid = totalWarrantyPaid;
        project.UpdatedDate = DateTime.Now;

        _repository.Update(project);
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
