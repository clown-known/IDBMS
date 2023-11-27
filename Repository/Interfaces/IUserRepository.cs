using BusinessObject.Models;
using System;

namespace Repository.Interfaces;

public interface IUserRepository : ICrudBaseRepository<User, Guid>
{
    public User? GetByEmail(string email);
    public void Lock(string email);
}
