using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface ICommentRepository : ICrudBaseRepository<Comment, Guid>
    {
        IEnumerable<Comment?> GetByConstructionTaskId(Guid ctId);
        IEnumerable<Comment?> GetByDecorProgressReportId(Guid dprId);
    }
}
