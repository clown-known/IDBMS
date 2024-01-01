using BusinessObject.Enums;
using BusinessObject.Models;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implements
{
    public class AuthenticationCodeRepository : IAuthenticationCodeRepository
    {
        public int CheckNumOfSend(Guid userId)
        {
            try
            {
                using var context = new IdtDbContext();

                return context.AuthenticationCodes.Where(c => c.UserId.Equals(userId)
                    && c.CreatedTime.AddMinutes((int)ConstantValues.TimeCount) > DateTime.UtcNow).Count();
            }
            catch
            {
                throw;
            }
        }

        public void DeleteById(Guid id)
        {
            throw new NotImplementedException();
        }

        public void EnableCodeOfUser(Guid userId)
        {
            using var context = new IdtDbContext();
            context.AuthenticationCodes.Where(c => c.UserId.Equals(userId)).ToList()
                .ForEach(c => c.Status = BusinessObject.Enums.AuthenticationCodeStatus.Expired);
            context.SaveChanges();
        }

        public IEnumerable<AuthenticationCode> GetAll()
        {
            throw new NotImplementedException();
        }

        public AuthenticationCode? GetById(Guid id)
        {
            try
            {
                using var context = new IdtDbContext();
                return context.AuthenticationCodes.FirstOrDefault(u => u.Id == id);
            }
            catch
            {
                throw;
            }
        }

        public AuthenticationCode GetByUserId(Guid userId)
        {
            try
            {
                using var context = new IdtDbContext();
                return context.AuthenticationCodes.FirstOrDefault(u => u.UserId == userId && u.Status == AuthenticationCodeStatus.Active);
                
            }
            catch
            {
                throw;
            }
        }

        public AuthenticationCode? Save(AuthenticationCode entity)
        {
            try
            {
                using var context = new IdtDbContext();

                var codeAdded = context.AuthenticationCodes.Add(entity);
                context.SaveChanges();
                return codeAdded.Entity;
            }   
            catch
            {
                throw;
            }
        }

        public void Update(AuthenticationCode entity)
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
    }
}
