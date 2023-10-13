using BusinessObject.Models;
using Repository.Interfaces;

namespace Repository.Implements;

public class UserRepository : IUserRepository
{
    public IEnumerable<User> GetAll()
    {
        try
        {
            using var context = new IdtDbContext();
            return context.Users.ToList();
        }
        catch
        {
            throw;
        }
}
    public User? GetById(Guid id)
    {
        try
        {
            using var context = new IdtDbContext();
            return context.Users.FirstOrDefault(u => u.Id == id);
        }
        catch
        {
            throw;
        }
    }

    public User? GetByEmailAndPassword(string email, string password)
    {
        try
        {
            using var context = new IdtDbContext();
            byte[] hashPass = new byte[2];
            return context.Users.FirstOrDefault(d => d.Email == email && d.Password == hashPass);
        }
        catch
        {
            throw;
        }
    }

    public void Save(User user)
    {
        try
        {
            using var context = new IdtDbContext();
            context.Users.Add(user);
            context.SaveChanges();
        }
        catch
        {
            throw;
        }
    }

    public void Update(User user)
    {
        try
        {
            using var context = new IdtDbContext();
            context.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
        }
        catch
        {
            throw;
        }
    }

    public void DeleteById(Guid userId)
    {
        try
        {
            using var context = new IdtDbContext();
            User user = new() { Id = userId };
            context.Users.Attach(user);
            context.Users.Remove(user);
            context.SaveChanges();
        }
        catch
        {
            throw;
        }
    }
}
