using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IItemInTaskRepository : ICrudBaseRepository<ItemInTask, Guid>
    {
        IEnumerable<ItemInTask> GetByProjectId(Guid id);
        IEnumerable<ItemInTask> GetByRoomId(Guid id);
        IEnumerable<ItemInTask> GetByTaskId(Guid id);
    }
}
