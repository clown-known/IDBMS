using BusinessObject.Models;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implements
{
    public class TaskDocumentRepository : ITaskDocumentRepository
    {
        
        public IEnumerable<TaskDocument> GetAll()
        {
            try
            {
                using var context = new IdtDbContext();
                return context.TaskDocuments.ToList();
            }
            catch
            {
                throw;
            }
        }

        public TaskDocument? GetById(Guid id)
        {
            try
            {
                using var context = new IdtDbContext();
                return context.TaskDocuments.Where(td => td.Id == id).FirstOrDefault();
            }
            catch
            {
                throw;
            }
        }

        public TaskDocument? Save(TaskDocument entity)
        {
            try
            {
                using var context = new IdtDbContext();
                var td = context.TaskDocuments.Add(entity);
                context.SaveChanges();
                return td.Entity;
            }
            catch
            {
                throw;
            }
        }

        public void Update(TaskDocument entity)
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
                var td = context.TaskDocuments.FirstOrDefault(td => td.Id == id);
                if (td != null)
                {
                    context.TaskDocuments.Remove(td);
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
