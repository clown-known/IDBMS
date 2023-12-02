using BusinessObject.Models;
using System;

namespace Repository.Interfaces;

public interface IProjectRepository : ICrudBaseRepository<Project, Guid>
{
    IEnumerable<Project> GetBySiteId(Guid id);
    public Project? GetByIdWithSite(Guid id);
}
