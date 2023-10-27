using BusinessObject.Models;
using Repository.Interfaces;

namespace Repository.Implements;

public class ParticipationRepository : IParticipationRepository
{

    public IEnumerable<Participation> GetAll()
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

    public Participation? GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Participation? Save(Participation participationEntity)
    {
        try
        {
            using var context = new IdtDbContext();
            var partiAdded = context.Participations.Add(participationEntity);
            context.SaveChanges();
            return partiAdded.Entity;
        }
        catch
        {
            throw;
        }
    }

    public void Update(Participation entity)
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

    public IEnumerable<Participation> GetByProjectId(Guid projectID)
    {
        try
        {
            using var context = new IdtDbContext();
            return context.Participations
                .Where(u => u.ProjectId.Equals(projectID) && u.IsDeleted == false)
                .ToList();
        }
        catch
        {
            throw;
        }
    }
    public IEnumerable<Participation> GetByUserId(Guid userId)
    {
        try
        {
            using var context = new IdtDbContext();
            return context.Participations
                .Where(u => u.UserId.Equals(userId) && u.IsDeleted == false)
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
