using BusinessObject.Models;
using System;

namespace Repository.Interfaces;

public interface IProjectParticipationRepository : ICrudBaseRepository<ProjectParticipation, Guid>
{
    IEnumerable<ProjectParticipation> GetByProjectId(Guid id);
    IEnumerable<ProjectParticipation> GetProjectMemberByProjectId(Guid id);
    IEnumerable<ProjectParticipation> GetUsersByParticipationInProject(Guid id);
    IEnumerable<ProjectParticipation> GetByUserId(Guid id);
    ProjectParticipation? GetProjectOwnerByProjectId(Guid id);
    ProjectParticipation? GetProjectManagerByProjectId(Guid id);
}
