using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IAuthenticationCodeRepository : ICrudBaseRepository<AuthenticationCode, Guid>
    {
        public void EnableCodeOfUser(Guid userId);
        public int CheckNumOfSend(Guid userId);
        public AuthenticationCode? GetByUserId(Guid userId);
    }
}
