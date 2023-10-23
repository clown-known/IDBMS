using BusinessObject.Models;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository.Implements
{
    public class ConstructionTaskRepository : IConstructionTaskRepository
    {
        public IEnumerable<ConstructionTask> GetAll()
        {
            try
            {
                using var context = new IdtDbContext();
                return context.ConstructionTasks.ToList();
            }
            catch
            {
                throw;
            }
        }

        public ConstructionTask? GetById(Guid id)
        {
            try
            {
                using var context = new IdtDbContext();
                return context.ConstructionTasks.FirstOrDefault(task => task.Id == id);
            }
            catch
            {
                throw;
            }
        }

        public ConstructionTask Save(ConstructionTask entity)
        {
            try
            {
                using var context = new IdtDbContext();
                var task = context.ConstructionTasks.Add(entity);
                context.SaveChanges();
                return task.Entity;
            }
            catch
            {
                throw;
            }
        }

        public void Update(ConstructionTask entity)
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
                var task = context.ConstructionTasks.FirstOrDefault(task => task.Id == id);
                if (task != null)
                {
                    context.ConstructionTasks.Remove(task);
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
