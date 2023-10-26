using BusinessObject.DTOs.Request;
using BusinessObject.Enums;
using BusinessObject.Models;
using Repository.Interfaces;

public class ProjectService
{
    private readonly IProjectRepository projectRepo;
    private readonly IParticipationRepository participationRepo;

    public ProjectService(IProjectRepository projectRepo, IParticipationRepository participationRepo)
    {
        this.projectRepo = projectRepo;
        this.participationRepo = participationRepo;
    }

    public IEnumerable<Project> GetAllProjects()
    {
        return projectRepo.GetAll();
    }

    public Project? GetById(Guid id)
    {
        return projectRepo.GetById(id);
    }

    public IEnumerable<Participation> GetByUserId(Guid id)
    {
        return participationRepo.GetByUserId(id);
    }
    public IEnumerable<Participation> GetByProjectId(Guid id)
    {
        return participationRepo.GetByProjectId(id);
    }

    public Project? CreateProject(ProjectRequest request)
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

        var createdProject = projectRepo.Save(project);
        return createdProject;
    }

    public void UpdateProject(ProjectRequest request)
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

        projectRepo.Update(project);
    }

    public void UpdateProjectStatus(Guid id, ProjectStatus status)
    {
        var project = projectRepo.GetById(id) ?? throw new Exception("Not existed");
        project.Status = status;
        projectRepo.Update(project);
    }
}
