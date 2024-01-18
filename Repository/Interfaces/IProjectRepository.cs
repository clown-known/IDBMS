using BusinessObject.Models;
using System;

namespace Repository.Interfaces;

public interface IProjectRepository : ICrudBaseRepository<Project, Guid>
{
    IEnumerable<Project> GetBySiteId(Guid id);
    IEnumerable<Project> GetAdvertisementAllowedProjects();
    IEnumerable<Project> GetPublicAdvertisementProjects();
    IEnumerable<Project> GetRecentProjects();
    IEnumerable<Project> GetRecentProjectsByUserId(Guid id);
    IEnumerable<Project> GetOngoingProjects();
    IEnumerable<Project> GetOngoingProjectsByUserId(Guid id);
}
