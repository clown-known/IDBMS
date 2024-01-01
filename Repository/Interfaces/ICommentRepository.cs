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
        IEnumerable<Comment> GetByTaskId(Guid id);
        IEnumerable<Comment> GetByProjectId(Guid id);
    }
}
