using BusinessObject.Models;
using System;

namespace Repository.Interfaces;

public interface IProjectParticipationRepository : ICrudBaseRepository<ProjectParticipation, Guid>
{
    IEnumerable<ProjectParticipation> GetByProjectId(Guid id);
    IEnumerable<ProjectParticipation> GetByUserId(Guid id);
}
