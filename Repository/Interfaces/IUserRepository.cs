using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
<<<<<<< HEAD
    public interface IUserRepository : ICrudBaseRepository<User, string>
    {
        User? GetByEmailAndPassword(string email, string password);
    }
=======
    public User? GetByEmail(string email);
    public void Lock(string email);
>>>>>>> dev
}
