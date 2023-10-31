using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IConstructionTaskRepository : ICrudBaseRepository<ConstructionTask, Guid>
    {
        IEnumerable<ConstructionTask?> GetByProjectId(Guid id);
    }
}
