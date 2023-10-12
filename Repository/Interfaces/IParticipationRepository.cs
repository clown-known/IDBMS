using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IParticipationRepository : ICrudBaseRepository<Participation, string>
    {
        IEnumerable<Guid> getAllParticipationByProjectID(Guid projectID);
        bool isPariticipation(Guid userid,Guid projectid);
    }
}
