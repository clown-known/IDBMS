using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IWarrantyClaimRepository : ICrudBaseRepository<WarrantyClaim, Guid>
    {
        IEnumerable<WarrantyClaim> GetByUserId(Guid id);
        IEnumerable<WarrantyClaim> GetByProjectId(Guid id);
    }
}
