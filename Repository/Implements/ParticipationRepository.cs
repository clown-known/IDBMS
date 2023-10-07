using BusinessObject.Models;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implements
{
    public class ParticipationRepository : IParticipationRepository
    {
        private readonly IdtDbContext context = new();
        public void Dispose()
        {
            context?.Dispose();
        }

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

        public Participation? Save(Participation entity)
        {
            var participationAdded = context.Participations.Add(entity);
            context.SaveChanges();
            return participationAdded.Entity;
        }

        public void Update(Participation entity)
        {
            context.Entry<Participation>(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
        }

        public IEnumerable<Guid> getAllParticipationByProjectID(Guid projectID)
        {
            var result = context.Participations.Where(u => u.ProjectId.Equals(projectID)&&u.IsDeleted!=false).Select(p => p.UserId);
            return result;
        }

        public bool isPariticipation(Guid userid, Guid projectid)
        {
            return context.Participations.Where(p => p.UserId.Equals(userid) && p.ProjectId.Equals(projectid) && p.IsDeleted != false).FirstOrDefault()!=null;
        }
    }
}
