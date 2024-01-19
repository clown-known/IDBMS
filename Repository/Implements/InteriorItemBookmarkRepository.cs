using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implements
{
    public class InteriorItemBookmarkRepository : IInteriorItemBookmarkRepository
    {
        public IEnumerable<InteriorItemBookmark> GetAll()
        {
            try
            {
                using var context = new IdtDbContext();
                return context.InteriorItemBookmarks
                    .ToList()
                    .Reverse<InteriorItemBookmark>();
            }
            catch
            {
                throw;
            }
        }

        public InteriorItemBookmark? GetById(Guid id)
        {
            try
            {
                using var context = new IdtDbContext();
                return context.InteriorItemBookmarks.Where(iib => iib.Id == id).FirstOrDefault();
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<InteriorItemBookmark> GetByUserId(Guid userId)
        {
            try
            {
                using var context = new IdtDbContext();
                return context.InteriorItemBookmarks
                    .Include(bm => bm.InteriorItem)
                    .Where(iib => iib.UserId.Equals(userId))
                    .ToList()
                    .Reverse<InteriorItemBookmark>();
            }
            catch
            {
                throw;
            }
        }

        public InteriorItemBookmark? Save(InteriorItemBookmark entity)
        {
            try
            {
                using var context = new IdtDbContext();
                var iibCreated = context.InteriorItemBookmarks.Add(entity);
                context.SaveChanges();
                return iibCreated.Entity;
            }
            catch
            {
                throw;
            }
        }

        public void Update(InteriorItemBookmark entity)
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
                var iib = context.InteriorItemBookmarks.FirstOrDefault(iib => iib.Id == id);
                if (iib != null)
                {
                    context.InteriorItemBookmarks.Remove(iib);
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
