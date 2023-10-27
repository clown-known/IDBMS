using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface ITransactionRepository : ICrudBaseRepository<Transaction, Guid>
    {
        IEnumerable<Transaction?> GetByPrepayStageId(Guid psId);
        IEnumerable<Transaction?> GetByUserId(Guid userId);
    }
}
