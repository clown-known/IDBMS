using BusinessObject.Models;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implements
{
    public class DecorProjectDesignRepository : IDecorProjectDesignRepository
    {
        public void DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DecorProjectDesign> GetAll()
        {
            try
            {
                using var context = new IdtDbContext();
                return context.DecorProjectDesigns.ToList();    
            }
            catch
            {
                throw;
            }
            
        }

        public DecorProjectDesign? GetById(int id)
        {
            try
            {
                using var context = new IdtDbContext();
                return context.DecorProjectDesigns.Where(dpd => dpd.Id == id).FirstOrDefault();
            }
            catch
            {
                throw;
            }
        }

        public DecorProjectDesign? Save(DecorProjectDesign entity)
        {
            try
            {
                using var context = new IdtDbContext();
                var dpd = context.DecorProjectDesigns.Add(entity);
                context.SaveChanges();
                return dpd.Entity;
            }
            catch
            {
                throw;
            }
        }

        public void Update(DecorProjectDesign entity)
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
