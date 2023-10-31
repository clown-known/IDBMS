using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IDecorProgressReportRepository : ICrudBaseRepository<DecorProgressReport, Guid>
    {
        IEnumerable<DecorProgressReport?> GetByPaymentStageId(Guid id);
    }
}
