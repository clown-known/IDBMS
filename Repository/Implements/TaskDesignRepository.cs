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
    public class TaskDesignRepository : ITaskDesignRepository
    {
        public IEnumerable<TaskDesign> GetAll()
        {
            try
            {
                using var context = new IdtDbContext();
                return context.TaskDesigns
                    .Include(p => p.TaskCategory)
                    .Where(psd => psd.IsDeleted == false)
                    .OrderBy(td => td.TaskCategoryId)
                    .ToList();
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
                return context.TaskDesigns.Where(psd => psd.Id == id && psd.IsDeleted == false).FirstOrDefault();
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

                bool exists = context.TaskDesigns.Any(task => task.Code.ToLower() == code.ToLower());

                return exists;
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
                var ctd = context.TaskDesigns.FirstOrDefault(ctd => ctd.Id == id && ctd.IsDeleted == false);
                if (ctd != null)
                {
                    ctd.IsDeleted = true;
                    context.Entry(ctd).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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
