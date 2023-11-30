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
    public class TaskReportRepository : ITaskReportRepository
    {
        public IEnumerable<TaskReport> GetAll()
        {
            try
            {
                using var context = new IdtDbContext();
                return context.TaskReports.Where(report => report.IsDeleted == false).ToList();
            }
            catch
            {
                throw;
            }
        }

        public TaskReport? GetById(Guid id)
        {
            try
            {
                using var context = new IdtDbContext();
                return context.TaskReports.FirstOrDefault(report => report.Id == id && report.IsDeleted == false);
            }
            catch
            {
                throw;
            }
        }
        public IEnumerable<TaskReport?> GetByTaskId(Guid id)
        {
            try
            {
                using var context = new IdtDbContext();
                return context.TaskReports
                    .Include(rd => rd.TaskDocuments)
                    .Where(report => report.ProjectTaskId == id && report.IsDeleted == false).ToList();
            }
            catch
            {
                throw;
            }
        }
        
        public TaskReport Save(TaskReport entity)
        {
            try
            {
                using var context = new IdtDbContext();
                var report = context.TaskReports.Add(entity);
                context.SaveChanges();
                return report.Entity;
            }
            catch
            {
                throw;
            }
        }

        public void Update(TaskReport entity)
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
                var report = context.TaskReports.FirstOrDefault(report => report.Id == id);
                if (report != null)
                {
                    report.IsDeleted = true;
                    context.Entry(report).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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
