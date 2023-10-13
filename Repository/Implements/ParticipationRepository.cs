using BusinessObject.Models;
using Repository.Interfaces;

namespace Repository.Implements;

public class ParticipationRepository : IParticipationRepository
{
    public void DeleteById(string id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Participation> GetAll()
    {
        throw new NotImplementedException();
    }

    public Participation? GetById(string id)
    {
        throw new NotImplementedException();
    }

    public void Save(Participation participationEntity)
    {
        try
        {
            using var context = new IdtDbContext();
            context.Participations.Add(participationEntity);
            context.SaveChanges();
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

    public IEnumerable<Participation> GetAllParticipationByProjectID(Guid projectID)
    {
        try
        {
            using var context = new IdtDbContext();
            return context.Participations
                .Where(u => u.ProjectId.Equals(projectID) && u.IsDeleted != false)
                .ToList();
        }
        catch
        {
            throw;
        }
    }
}
