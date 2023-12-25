using BusinessObject.Models;
using System;

namespace Repository.Interfaces;

public interface IProjectRepository : ICrudBaseRepository<Project, Guid>
{
    IEnumerable<Project> GetBySiteId(Guid id);
    IEnumerable<Project> GetAdvertisementAllowedProjects();
    IEnumerable<Project> GetRecentProjects();
    Project? GetByIdWithSite(Guid id);
}
