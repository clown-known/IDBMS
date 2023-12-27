using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IPaymentStageRepository : ICrudBaseRepository<PaymentStage, Guid>
    {
        IEnumerable<PaymentStage> GetByProjectId(Guid id);
        PaymentStage? GetByStageNoByProjectId(int no, Guid projectId);
    }
}
