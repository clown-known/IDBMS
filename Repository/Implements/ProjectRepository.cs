using BusinessObject.Models;
using Repository.Interfaces;

namespace Repository.Implements;

public class ProjectRepository : IProjectRepository
{
    public void DeleteById(string id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Project> GetAll()
    {
        try
        {
            using var context = new IdtDbContext();
            return context.Projects.ToList();
        }
        catch
        {
            throw;
        }
    }

    public Project? GetById(string id)
    {
        throw new NotImplementedException();
    }

    public void Save(Project entity)
    {
        throw new NotImplementedException();
    }

    public void Update(Project entity)
    {
        throw new NotImplementedException();
    }
}
