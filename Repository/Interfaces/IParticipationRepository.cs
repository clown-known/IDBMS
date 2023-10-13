using BusinessObject.Models;

namespace Repository.Interfaces;

public interface IParticipationRepository : ICrudBaseRepository<Participation, string>
{
    IEnumerable<Participation> GetAllParticipationByProjectID(Guid projectId);
}
