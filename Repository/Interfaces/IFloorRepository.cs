using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IFloorRepository : ICrudBaseRepository<Floor, Guid>
    {
        IEnumerable<Floor?> GetBySiteId(Guid id); 
    }
}
