using BusinessObject.Models;
using System;

namespace Repository.Interfaces;

public interface IParticipationRepository : ICrudBaseRepository<Participation, Guid>
{
    IEnumerable<Participation> GetAllParticipationByProjectID(Guid projectId);
}
