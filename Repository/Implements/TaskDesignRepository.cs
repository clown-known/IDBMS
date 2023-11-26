using BusinessObject.Models;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implements
{
    public class TaskDesignRepository : ITaskDesignRepository
    {
        public IEnumerable<TaskDesign> GetAll()
        {
            try
            {
                using var context = new IdtDbContext();
                return context.TaskDesigns.ToList();
            }
            catch
            {
                throw;
            }
        }

        public TaskDesign? GetById(int id)
        {
            try
            {
                using var context = new IdtDbContext();
                return context.TaskDesigns.Where(psd => psd.Id == id).FirstOrDefault();
            }
            catch
            {
                throw;
            }
        }

        public TaskDesign Save(TaskDesign entity)
        {
            try
            {
                using var context = new IdtDbContext();
                var ctd = context.TaskDesigns.Add(entity);
                context.SaveChanges();
                return ctd.Entity;
            }
            catch
            {
                throw;
            }
        }

        public void Update(TaskDesign entity)
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
                var ctd = context.TaskDesigns.FirstOrDefault(ctd => ctd.Id == id);
                if (ctd != null)
                {
                    context.TaskDesigns.Remove(ctd);
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
