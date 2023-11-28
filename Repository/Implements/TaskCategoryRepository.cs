using BusinessObject.Models;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implements
{
    public class TaskCategoryRepository : ITaskCategoryRepository
    {
        public void DeleteById(int id)
        {
            try
            {
                using var context = new IdtDbContext();
                var ctc = context.TaskCategories.Where(ctc => ctc.Id == id && ctc.IsDeleted == false).FirstOrDefault();
                if (ctc != null)
                {
                    ctc.IsDeleted = true;   
                    context.Entry(ctc).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<TaskCategory> GetAll()
        {
            try
            {
                using var context = new IdtDbContext();
                return context.TaskCategories.Where(ctc=> ctc.IsDeleted == false).ToList();
            }
            catch
            {
                throw;
            }
        }

        public TaskCategory? GetById(int id)
        {
            try
            {
                using var context = new IdtDbContext();
                return context.TaskCategories.Where(ctc => ctc.Id == id && ctc.IsDeleted == false).FirstOrDefault();
            }
            catch
            {
                throw;
            }
        }

        public TaskCategory? Save(TaskCategory entity)
        {
            try
            {
                using var context = new IdtDbContext();
                var ctc = context.TaskCategories.Add(entity);
                context.SaveChanges();
                return ctc.Entity;
            }
            catch
            {
                throw;
            }
        }

        public void Update(TaskCategory entity)
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
