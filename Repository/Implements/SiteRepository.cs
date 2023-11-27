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
    public class SiteRepository : ISiteRepository
    {
        public IEnumerable<Site> GetAll()
        {
            try
            {
                using var context = new IdtDbContext();
                return context.Sites.ToList();
            }
            catch
            {
                throw;
            }
        }

        public Site? GetById(Guid id)
        {

            try
            {
                using var context = new IdtDbContext();
                return context.Sites
                    .Include(f => f.Floors)
                    .Where(s => s.Id == id).FirstOrDefault();
            }
            catch
            {
                throw;
            }
        }
        public IEnumerable<Site?> GetByProjectId(Guid id)
        {
            try
            {
                using var context = new IdtDbContext();
                return context.Sites.Where(s => s.ProjectId == id).ToList();
            }
            catch
            {
                throw;
            }
        }

        public Site? Save(Site entity)
        {
            try
            {
                using var context = new IdtDbContext();
                var site = context.Sites.Add(entity);
                context.SaveChanges();
                return site.Entity;
            }
            catch
            {
                throw;
            }
        }

        public void Update(Site entity)
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
