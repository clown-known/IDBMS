using BusinessObject.Models;

namespace Repository.Interfaces;

public interface IUserRepository : ICrudBaseRepository<User, Guid>
{
    User? GetByEmailAndPassword(string email, string password);
}
