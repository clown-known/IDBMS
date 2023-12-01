using BusinessObject.Models;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implements
{
    public class AdminRepository : IAdminRepository
    {
        public IEnumerable<Admin> GetAll()
        {
            try
            {
                using var context = new IdtDbContext();
                return context.Admins.ToList();
            }
            catch
            {
                throw;
            }
        }

        public Admin? GetById(Guid id)
        {

            try
            {
                using var context = new IdtDbContext();
                return context.Admins.Where(a => a.Id == id).FirstOrDefault();
            }
            catch
            {
                throw;
            }
        }

        public Admin? Save(Admin entity)
        {

            try
            {
                using var context = new IdtDbContext();
                var adminCreated = context.Admins.Add(entity);
                context.SaveChanges();
                return adminCreated.Entity;
            }
            catch
            {
                throw;
            }
        }

        public void Update(Admin entity)
        {
            try
            {
                using var context = new IdtDbContext();
                context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
        public void DeleteById(Guid id)
        {
            try
            {
                using var context = new IdtDbContext();
                var admin = context.Admins.FirstOrDefault(a => a.Id == id);
                if (admin != null)
                {
                    context.Admins.Remove(admin);
                    context.SaveChanges();
                }
            }
            catch
            {
                throw;
            }
        }

        public Admin? GetByEmail(string email)
        {
            try
            {
                using var context = new IdtDbContext();
                return context.Admins.Where(a => a.Email.ToLower() == email.ToLower()).FirstOrDefault();
            }
            catch
            {
                throw;
            }
        }

        public Admin? GetByUsername(string username)
        {
            try
            {
                using var context = new IdtDbContext();
                return context.Admins.Where(a => a.Username.ToLower() == username.ToLower()).FirstOrDefault();
            }
            catch
            {
                throw;
            }
        }
    }
}
