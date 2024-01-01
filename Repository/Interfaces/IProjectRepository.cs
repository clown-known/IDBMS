using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
<<<<<<< HEAD
    public interface IProjectRepository : ICrudBaseRepository<Project, string>
    {
    }
=======
    IEnumerable<Project> GetBySiteId(Guid id);
    IEnumerable<Project> GetAdvertisementAllowedProjects();
    IEnumerable<Project> GetRecentProjects();
    IEnumerable<Project> GetRecentProjectsByUserId(Guid id);
    IEnumerable<Project> GetOngoingProjects();
    IEnumerable<Project> GetOngoingProjectsByUserId(Guid id);
    Project? GetByIdWithSite(Guid id);
>>>>>>> dev
}
