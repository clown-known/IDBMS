﻿using BusinessObject.Enums;
using BusinessObject.Models;
using Repository.Interfaces;
using System.Xml.Linq;

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

    public User? GetByEmail(string email)
    {
        try
        {
            using var context = new IdtDbContext();
            return context.Users.FirstOrDefault(d => d.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
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
    }

    public User? Save(User user)
    {
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
