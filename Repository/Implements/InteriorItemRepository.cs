using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
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
                return context.InteriorItems
                    .Include(u => u.InteriorItemCategory)
                    .Where(ii => ii.IsDeleted == false)
                    .OrderByDescending(ii => ii.CreatedDate)
                    .ToList();
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
                return context.InteriorItems
                    .Include(u => u.InteriorItemCategory)
                    .FirstOrDefault(ii => ii.Id == id && ii.IsDeleted == false);
            }
            catch
            {
                throw;
            }
        }
        public IEnumerable<InteriorItem> GetByCategory(int id)
        {
            try
            {
                using var context = new IdtDbContext();
                return context.InteriorItems
                    .Where(ii => ii.InteriorItemCategoryId == id && ii.IsDeleted == false)
                    .Include(u => u.InteriorItemCategory)
                    .OrderByDescending(ii => ii.CreatedDate)
                    .ToList();
            }
            catch
            {
                throw;
            }
        }

        public bool CheckCodeExisted(string code)
        {
            try
            {
                using var context = new IdtDbContext();

                bool exists = context.InteriorItems.Any(item => item.Code.ToLower() == code.ToLower());

                return exists;
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
                var ii = context.InteriorItems.FirstOrDefault(ii => ii.Id == id && ii.IsDeleted == false);
                if (ii != null)
                {
                    ii.IsDeleted = true;
                    context.Entry(ii).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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
