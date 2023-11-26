using BusinessObject.Models;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implements
{
    public class WarrantyClaimRepository : IWarrantyClaimRepository
    {
        public IEnumerable<WarrantyClaim> GetAll()
        {
            try
            {
                using var context = new IdtDbContext();
                return context.WarrantyClaims.ToList();
            }
            catch
            {
                throw;
            }
        }

        public WarrantyClaim? GetById(Guid id)
        {
            try
            {
                using var context = new IdtDbContext();
                return context.WarrantyClaims.Where(wc => wc.Id == id).FirstOrDefault();
            }
            catch
            {
                throw;
            }
        }

        public WarrantyClaim? Save(WarrantyClaim entity)
        {
            try
            {
                using var context = new IdtDbContext();
                var wc = context.WarrantyClaims.Add(entity);
                context.SaveChanges();
                return wc.Entity;
            }
            catch
            {
                throw;
            }
        }

        public void Update(WarrantyClaim entity)
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
            throw new NotImplementedException();
        }
    }
}
