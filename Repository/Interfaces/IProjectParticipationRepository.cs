using BusinessObject.Models;
using System;

namespace Repository.Interfaces;

public interface IProjectParticipationRepository : ICrudBaseRepository<ProjectParticipation, Guid>
{
    IEnumerable<ProjectParticipation> GetByProjectId(Guid projectId);
    IEnumerable<ProjectParticipation> GetByUserId(Guid userId);
}
