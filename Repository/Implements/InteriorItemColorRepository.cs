using BusinessObject.Models;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository.Implements
{
    public class InteriorItemColorRepository : IInteriorItemColorRepository
    {
        public IEnumerable<InteriorItemColor> GetAll()
        {
            try
            {
                using var context = new IdtDbContext();
                return context.InteriorItemColors.ToList();
            }
            catch
            {
                throw;
            }
        }

        public InteriorItemColor? GetById(int id)
        {
            try
            {
                using var context = new IdtDbContext();
                return context.InteriorItemColors.FirstOrDefault(iic => iic.Id == id);
            }
            catch
            {
                throw;
            }
        }

        public InteriorItemColor Save(InteriorItemColor entity)
        {
            try
            {
                using var context = new IdtDbContext();
                var iic = context.InteriorItemColors.Add(entity);
                context.SaveChanges();
                return iic.Entity;
            }
            catch
            {
                throw;
            }
        }

        public void Update(InteriorItemColor entity)
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
                var iic = context.InteriorItemColors.FirstOrDefault(iic => iic.Id == id);
                if (iic != null)
                {
                    context.InteriorItemColors.Remove(iic);
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