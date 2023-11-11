using BusinessObject.Models;
using Repository.Interfaces;

namespace Repository.Implements;

public class ProjectParticipationRepository : IProjectParticipationRepository
{

    public IEnumerable<ProjectParticipation> GetAll()
    {
        try
        {
            using var context = new IdtDbContext();
            return context.Participations.ToList();
        }
        catch
        {
            throw;
        }
    }

    public ProjectParticipation? GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public ProjectParticipation? Save(ProjectParticipation entity)
    {
        try
        {
            using var context = new IdtDbContext();
            var partiAdded = context.Participations.Add(entity);
            context.SaveChanges();
            return partiAdded.Entity;
        }
        catch
        {
            throw;
        }
    }

    public void Update(ProjectParticipation entity)
    {
        try
        {
            using var context = new IdtDbContext();
            context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
        }
        catch
        {
            throw;
        }
    }

    public IEnumerable<ProjectParticipation> GetByProjectId(Guid id)
    {
        try
        {
            using var context = new IdtDbContext();
            return context.Participations
                .Where(u => u.ProjectId.Equals(id) && u.IsDeleted == false)
                .ToList();
        }
        catch
        {
            throw;
        }
    }
    public IEnumerable<ProjectParticipation> GetByUserId(Guid id)
    {
        try
        {
            using var context = new IdtDbContext();
            return context.Participations
                .Where(u => u.UserId.Equals(id) && u.IsDeleted == false)
                .ToList();
        }
        catch
        {
            throw;
        }
    }

    public void DeleteById(Guid id)
    {
        throw new NotImplementedException();
    }
}
