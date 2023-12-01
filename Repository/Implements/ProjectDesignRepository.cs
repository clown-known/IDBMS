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
    public class ProjectDesignRepository : IProjectDesignRepository
    {
        public void DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProjectDesign> GetAll()
        {
            try
            {
                using var context = new IdtDbContext();
                return context.ProjectDesigns.ToList();    
            }
            catch
            {
                throw;
            }
            
        }

        public IEnumerable<ProjectDesign> GetByType(ProjectType type)
        {
            try
            {
                using var context = new IdtDbContext();
                return context.ProjectDesigns
                    .Where(pd => pd.ProjectType == type)
                    .ToList();
            }
            catch
            {
                throw;
            }

        }

        public ProjectDesign? GetById(int id)
        {
            try
            {
                using var context = new IdtDbContext();
                return context.ProjectDesigns.Where(dpd => dpd.Id == id).FirstOrDefault();
            }
            catch
            {
                throw;
            }
        }

        public ProjectDesign? Save(ProjectDesign entity)
        {
            try
            {
                using var context = new IdtDbContext();
                var dpd = context.ProjectDesigns.Add(entity);
                context.SaveChanges();
                return dpd.Entity;
            }
            catch
            {
                throw;
            }
        }

        public void Update(ProjectDesign entity)
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
