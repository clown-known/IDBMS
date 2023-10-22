using BusinessObject.Models;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implements
{
    public class DocumentTemplateRepository : IProjectDocumentTemplateRepository
    {
        public void DeleteById(int id)
        {
            try
            {
                using var context = new IdtDbContext();
                var dt = context.ProjectDocumentTemplates.Where(dt => dt.Id == id).FirstOrDefault();
                context.ProjectDocumentTemplates.Remove(dt);
                context.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<ProjectDocumentTemplate> GetAll()
        {
            try
            {
                using var context = new IdtDbContext();
                return context.ProjectDocumentTemplates.ToList();
            }
            catch
            {
                throw;
            }
        }

        public ProjectDocumentTemplate? GetById(int id)
        {
            try
            {
                using var context = new IdtDbContext();
                return context.ProjectDocumentTemplates.Where(dt => dt.Id == id).FirstOrDefault();
            }
            catch
            {
                throw;
            }
        }

        public ProjectDocumentTemplate? Save(ProjectDocumentTemplate entity)
        {
            try
            {
                using var context = new IdtDbContext();
                var dt = context.ProjectDocumentTemplates.Add(entity);
                context.SaveChanges();
                return dt.Entity;
            }
            catch
            {
                throw;
            }
        }

        public void Update(ProjectDocumentTemplate entity)
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
