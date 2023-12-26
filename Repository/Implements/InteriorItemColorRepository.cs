using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
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
                return context.InteriorItemColors
                    .Include(color => color.InteriorItems.Where(ii => ii.IsDeleted == false))
                    .ToList()
                    .Reverse<InteriorItemColor>();
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
                return context.InteriorItemColors
                    .Include(color => color.InteriorItems.Where(ii => ii.IsDeleted == false))
                    .FirstOrDefault(iic => iic.Id == id);
            }
            catch
            {
                throw;
            }
        }
        public IEnumerable<InteriorItemColor> GetByCategoryId(int id)
        {
            try
            {
                using var context = new IdtDbContext();
                List<InteriorItemColor?> result = new List<InteriorItemColor?>();
                
                var iiList = context.InteriorItems.Where(ii => ii.InteriorItemCategoryId == id).ToList();
      
                if (iiList != null)
                {
                    foreach (InteriorItem item in iiList)
                    {
                        InteriorItemColor? color = new InteriorItemColor();

                        if (item != null && item.InteriorItemColorId != null) color = context.InteriorItemColors.Include(color => color.InteriorItems.Where(ii => ii.IsDeleted == false)).FirstOrDefault(iic => iic.Id == item.InteriorItemColorId);

                        if (color.Id != 0 && color != null) result.Add(color);
                    }
                }
                return result.Reverse<InteriorItemColor>();
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