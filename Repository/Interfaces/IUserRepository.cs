using BusinessObject.Models;
using System;

namespace Repository.Interfaces;

public interface IUserRepository : ICrudBaseRepository<User, Guid>
{
    User? GetByEmailAndPassword(string email, string password);
}
