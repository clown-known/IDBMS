using BusinessObject.DTOs.Request.CreateRequests;
using BusinessObject.DTOs.Request.UpdateRequests;
using BusinessObject.Enums;
using BusinessObject.Models;
using Repository.Interfaces;

public class ProjectService
{
    private readonly IProjectRepository projectRepository;

    public ProjectService(IProjectRepository projectRepository)
    {
        this.projectRepository = projectRepository;
    }

    public IEnumerable<Project> GetAllProjects()
    {
        return projectRepository.GetAll();
    }

    public Project? GetById(Guid id)
    {
        return projectRepository.GetById(id);
    }

    public IEnumerable<Project> GetByUserId(Guid id)
    {

        return null;
    }

    public Project? CreateProject(CreateProjectRequest request)
    {
        var project = new Project
        {
            Name = request.Name,
            CompanyName = request.CompanyName,
            Location = request.Location,
            Description = request.Description,
            Type = request.Type,
            ProjectCategoryId = request.ProjectCategoryId,
            CreatedDate = request.CreatedDate,
            UpdatedDate = request.UpdatedDate,
            NoStage = request.NoStage,
            EstimatedPrice = request.EstimatedPrice,
            FinalPrice = request.FinalPrice,
            CurrentStageId = request.CurrentStageId,
            Language = request.Language,
            Status = request.Status,
            IsAdvertisement = request.IsAdvertisement,
            AdminNote = request.AdminNote,
            BasedOnDecorProjectId = request.BasedOnDecorProjectId,
            DecorProjectDesignId = request.DecorProjectDesignId
        };

        var createdProject = projectRepository.Save(project);
        return createdProject;
    }

    public void UpdateProject(UpdateProjectRequest request)
    {
        var project = new Project
        {
            Id = request.Id,
            Name = request.Name,
            CompanyName = request.CompanyName,
            Location = request.Location,
            Description = request.Description,
            Type = request.Type,
            ProjectCategoryId = request.ProjectCategoryId,
            CreatedDate = request.CreatedDate,
            UpdatedDate = request.UpdatedDate,
            NoStage = request.NoStage,
            EstimatedPrice = request.EstimatedPrice,
            FinalPrice = request.FinalPrice,
            CurrentStageId = request.CurrentStageId,
            Language = request.Language,
            Status = request.Status,
            IsAdvertisement = request.IsAdvertisement,
            AdminNote = request.AdminNote,
            BasedOnDecorProjectId = request.BasedOnDecorProjectId,
            DecorProjectDesignId = request.DecorProjectDesignId
        };

        projectRepository.Update(project);
    }

    public void UpdateProjectStatus(Guid id, ProjectStatus status)
    {
        
    }
}
