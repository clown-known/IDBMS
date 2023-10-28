using BusinessObject.DTOs.Request;
using BusinessObject.Enums;
using BusinessObject.Models;
using Repository.Interfaces;

public class ProjectService
{
    private readonly IProjectRepository _repository;

    public ProjectService(IProjectRepository _repository)
    {
        this._repository = _repository;
    }

    public IEnumerable<Project> GetAll()
    {
        return _repository.GetAll();
    }

    public Project? GetById(Guid id)
    {
        return _repository.GetById(id) ?? throw new Exception("This object is not existed!");
    }

    public Project? CreateProject(ProjectRequest request)
    {
        var project = new Project
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            CompanyName = request.CompanyName,
            Location = request.Location,
            Description = request.Description,
            Type = request.Type,
            ProjectCategoryId = request.ProjectCategoryId,
            CreatedDate = DateTime.Now,
            NoStage = request.NoStage,
            EstimatedPrice = request.EstimatedPrice,
            FinalPrice = request.FinalPrice,
            CurrentStageId = request.CurrentStageId,
            Language = request.Language,
            Status = request.Status,
            IsAdvertisement = false,
            AdminNote = request.AdminNote,
            BasedOnDecorProjectId = request.BasedOnDecorProjectId,
            DecorProjectDesignId = request.DecorProjectDesignId
        };

        var createdProject = _repository.Save(project);
        return createdProject;
    }

    public void UpdateProject(Guid id, ProjectRequest request)
    {
        var p = _repository.GetById(id) ?? throw new Exception("This object is not existed!");

        p.Name = request.Name;
        p.CompanyName = request.CompanyName;
        p.Location = request.Location;
        p.Description = request.Description;
        p.Type = request.Type;
        p.ProjectCategoryId = request.ProjectCategoryId;
        p.UpdatedDate = DateTime.Now;
        p.NoStage = request.NoStage;
        p.EstimatedPrice = request.EstimatedPrice;
        p.FinalPrice = request.FinalPrice;
        p.CurrentStageId = request.CurrentStageId;
        p.Language = request.Language;
        p.Status = request.Status;
        p.AdminNote = request.AdminNote;
        p.BasedOnDecorProjectId = request.BasedOnDecorProjectId;
        p.DecorProjectDesignId = request.DecorProjectDesignId;

        _repository.Update(p);
    }

    public void UpdateProjectStatus(Guid id, ProjectStatus status)
    {
        var project = _repository.GetById(id) ?? throw new Exception("Not existed");

        project.Status = status;

        _repository.Update(project);
    }

    public void UpdateProjectAdvertisementStatus(Guid id, bool isAdvertisement)
    {
        var project = _repository.GetById(id) ?? throw new Exception("Not existed");

        project.IsAdvertisement = isAdvertisement;

        _repository.Update(project);
    }
}
