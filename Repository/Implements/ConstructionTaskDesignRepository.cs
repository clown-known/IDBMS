using BusinessObject.Models;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository.Implements
{
    public class ConstructionTaskDesignRepository : IConstructionTaskDesignRepository
    {
        public IEnumerable<ConstructionTaskDesign> GetAll()
        {
            try
            {
                using var context = new IdtDbContext();
                return context.ConstructionTaskDesigns.ToList();
            }
            catch
            {
                throw;
            }
        }

        public ConstructionTaskDesign? GetById(int id)
        {
            try
            {
                using var context = new IdtDbContext();
                return context.ConstructionTaskDesigns.Where(psd => psd.Id == id).FirstOrDefault();
            }
            catch
            {
                throw;
            }
        }

        public ConstructionTaskDesign Save(ConstructionTaskDesign entity)
        {
            try
            {
                using var context = new IdtDbContext();
                var ctd = context.ConstructionTaskDesigns.Add(entity);
                context.SaveChanges();
                return ctd.Entity;
            }
            catch
            {
                throw;
            }
        }

        public void Update(ConstructionTaskDesign entity)
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
                var ctd = context.ConstructionTaskDesigns.FirstOrDefault(ctd => ctd.Id == id);
                if (ctd != null)
                {
                    context.ConstructionTaskDesigns.Remove(ctd);
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
