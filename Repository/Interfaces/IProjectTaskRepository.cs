using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IProjectTaskRepository : ICrudBaseRepository<ProjectTask, Guid>
    {
        IEnumerable<ProjectTask> GetByProjectId(Guid id);
        IEnumerable<ProjectTask> GetByRoomId(Guid id);
        IEnumerable<ProjectTask> GetByPaymentStageId(Guid id);
        IEnumerable<ProjectTask> GetOngoingTasksByUserId(Guid id);
        IEnumerable<ProjectTask> GetOngoingTasks();
    }
}
