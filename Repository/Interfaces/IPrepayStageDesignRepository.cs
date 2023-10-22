using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IPrepayStageDesignRepository : ICrudBaseRepository<BusinessObject.Models.PrepayStageDesign, int>
    {
        IEnumerable<PrepayStageDesign> GetByDecorProjectDesignId(int designId);
    }
}
