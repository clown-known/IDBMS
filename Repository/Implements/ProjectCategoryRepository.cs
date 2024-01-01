using BusinessObject.Models;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implements
{
    public class ProjectCategoryRepository : IProjectCategoryRepository
    {
        public void DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProjectCategory> GetAll()
        {
            try
            {
                using var context = new IdtDbContext();
                return context.ProjectCategories
                    .ToList()
                    .Reverse<ProjectCategory>();
            }
            catch
            {
                throw;
            }
        }

        public ProjectCategory? GetById(int id)
        {
            try
            {
                using var context = new IdtDbContext();
                return context.ProjectCategories.Where(pc => pc.Id == id).FirstOrDefault();
            }
            catch
            {
                throw;
            }
        }

        public ProjectCategory? Save(ProjectCategory entity)
        {
            try
            {
                using var context = new IdtDbContext();
                var projectCategory = context.ProjectCategories.Add(entity);
                context.SaveChanges();
                return projectCategory.Entity;
            }
            catch
            {
                throw;
            }
        }

        public void Update(ProjectCategory entity)
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
