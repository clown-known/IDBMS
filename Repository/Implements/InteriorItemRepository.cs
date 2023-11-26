using BusinessObject.Models;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository.Implements
{
    public class InteriorItemRepository : IInteriorItemRepository
    {
        public IEnumerable<InteriorItem> GetAll()
        {
            try
            {
                using var context = new IdtDbContext();
                return context.InteriorItems.ToList();
            }
            catch
            {
                throw;
            }
        }

        public InteriorItem? GetById(Guid id)
        {
            try
            {
                using var context = new IdtDbContext();
                return context.InteriorItems.FirstOrDefault(ii => ii.Id == id);
            }
            catch
            {
                throw;
            }
        }
        public IEnumerable<InteriorItem?> GetByCategory(int id)
        {
            try
            {
                using var context = new IdtDbContext();
                return context.InteriorItems.Where(ii => ii.InteriorItemCategoryId == id).ToList();
            }
            catch
            {
                throw;
            }
        }
        public InteriorItem Save(InteriorItem entity)
        {
            try
            {
                using var context = new IdtDbContext();
                var ii = context.InteriorItems.Add(entity);
                context.SaveChanges();
                return ii.Entity;
            }
            catch
            {
                throw;
            }
        }

        public void Update(InteriorItem entity)
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
                var ii = context.InteriorItems.FirstOrDefault(ii => ii.Id == id);
                if (ii != null)
                {
                    context.InteriorItems.Remove(ii);
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
