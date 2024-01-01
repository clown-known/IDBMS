using BusinessObject.Enums;
using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
<<<<<<< HEAD
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
=======
using System.Linq;
using System.Xml.Linq;
>>>>>>> dev

namespace Repository.Implements
{
    public class UserRepository : IUserRepository
    { 
    private readonly IdtDbContext context = new();

    public void Dispose()
    {
<<<<<<< HEAD
        context?.Dispose();
=======
        try
        {
            using var context = new IdtDbContext();
            return context.Users
                .Include(u => u.Comments.Where(cmt => cmt.IsDeleted ==false))
                .Include(u => u.Transactions.Where(trans => trans.IsDeleted == false))
                .Include(u => u.Participations.Where(p => p.IsDeleted == false))
                .Include(u => u.BookingRequests.Where(br => br.IsDeleted == false))
                .OrderByDescending(u => u.CreatedDate)
                .ToList();
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
            return context.Users
                .Include(u => u.Comments.Where(cmt => cmt.IsDeleted == false))
                .Include(u => u.Transactions.Where(trans => trans.IsDeleted == false))
                .Include(u => u.Participations.Where(p => p.IsDeleted == false))
                .Include(u => u.BookingRequests.Where(br => br.IsDeleted == false))
                .FirstOrDefault(u => u.Id == id);
        }
        catch
        {
            throw;
        }
>>>>>>> dev
    }

    public IEnumerable<User> GetAll()
    {
<<<<<<< HEAD
        return context.Users.ToList();
    }
    public User? GetById(string id)
    {
        return context.Users.FirstOrDefault(d => d.Id.ToString() == id);
    }

    public User? GetByEmailAndPassword(string email, string password)
    {
        byte[] hashPass = new byte[2];
        return context.Users.FirstOrDefault(d => d.Email == email && d.Password == hashPass);
=======
        try
        {
            using var context = new IdtDbContext();
            return context.Users
                .Include(u => u.Comments.Where(cmt => cmt.IsDeleted == false))
                .Include(u => u.Transactions.Where(trans => trans.IsDeleted == false))
                .Include(u => u.Participations.Where(p => p.IsDeleted == false))
                .Include(u => u.BookingRequests.Where(br => br.IsDeleted == false))
                .FirstOrDefault(d => d.Email.ToLower().Equals(email.ToLower()));
        }
        catch
        {
            throw;
        }
    }
    public void Lock(string email)
    {
        try
        {
            using var context = new IdtDbContext();
            var user = context.Users.FirstOrDefault(d => d.Email == email);
            user.LockedUntil = DateTime.Now.AddMinutes((int)ConstantValues.LockedTime);
            context.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
        }
        catch
        {
            throw;
        }
>>>>>>> dev
    }

    public User? Save(User user)
    {
<<<<<<< HEAD
        var userAdded = context.Users.Add(user);
        context.SaveChanges();
        return userAdded.Entity;
=======
        try
        {
            using var context = new IdtDbContext();
            user.Email = user.Email.ToLower();
            var userAdded = context.Users.Add(user);
            context.SaveChanges();
            return userAdded.Entity;
        }
        catch
        {
            throw;
        }
>>>>>>> dev
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
