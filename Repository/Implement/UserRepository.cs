using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implement
{
    public class UserRepository : IUserRepository
    { 
    private readonly IdtDbContext context = new();

    public void Dispose()
    {
        context?.Dispose();
    }

    public IEnumerable<User> GetAll()
    {
        return context.Users.ToList();
    }
    public User? GetById(string id)
    {
        return context.Users.FirstOrDefault(d => d.Id.ToString() == id);
    }

    public User? GetByEmailAndPassword(string email, string password)
    {
        return context.Users.FirstOrDefault(d => d.Email == email && d.Password == password);
    }

    public User? Save(User user)
    {
        var userAdded = context.Users.Add(user);
        context.SaveChanges();
        return userAdded.Entity;
    }

    public void Update(User user)
    {
        context.Entry<User>(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        context.SaveChanges();
    }

    public void DeleteById(string userId)
    {
        var user = context.Users.Where(d => d.Id.ToString() == userId).FirstOrDefault();
        if (user != null)
        {
            context.Users.Remove(user);
        }
        context.SaveChanges();
    }
}
}
