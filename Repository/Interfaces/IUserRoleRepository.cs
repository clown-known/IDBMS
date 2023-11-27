using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IUserRoleRepository : ICrudBaseRepository<BusinessObject.Models.UserRole, int>
    {
        public IEnumerable<UserRole?> GetByUserId(Guid id);
    }
}
