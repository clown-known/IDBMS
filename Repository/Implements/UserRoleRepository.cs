using BusinessObject.Models;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implements
{
    public class UserRoleRepository : IUserRoleRepository
    {
        public IEnumerable<UserRole> GetAll()
        {
            try
            {
                using var context = new IdtDbContext();
                return context.UserRoles
                    .ToList()
                    .Reverse<UserRole>();
            }
            catch
            {
                throw;
            }
        }

        public UserRole? GetById(int id)
        {
            try
            {
                using var context = new IdtDbContext();
                return context.UserRoles.Where(td => td.Id == id).FirstOrDefault();
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<UserRole?> GetByUserId(Guid id)
        {
            try
            {
                using var context = new IdtDbContext();
                return context.UserRoles.Where(td => td.UserId == id)
                    .ToList()
                    .Reverse<UserRole>();
            }
            catch
            {
                throw;
            }
        }

        public UserRole? Save(UserRole entity)
        {
            try
            {
                using var context = new IdtDbContext();
                var td = context.UserRoles.Add(entity);
                context.SaveChanges();
                return td.Entity;
            }
            catch
            {
                throw;
            }
        }

        public void Update(UserRole entity)
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

        public void DeleteById(int id)
        {
            try
            {
                using var context = new IdtDbContext();
                var td = context.UserRoles.FirstOrDefault(td => td.Id == id);
                if (td != null)
                {
                    context.UserRoles.Remove(td);
                    context.SaveChanges();
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
