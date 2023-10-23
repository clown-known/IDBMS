using BusinessObject.Models;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository.Implements
{
    public class ConstructionTaskReportRepository : IConstructionTaskReportRepository
    {
        public IEnumerable<ConstructionTaskReport> GetAll()
        {
            try
            {
                using var context = new IdtDbContext();
                return context.ConstructionTaskReports.ToList();
            }
            catch
            {
                throw;
            }
        }

        public ConstructionTaskReport? GetById(Guid id)
        {
            try
            {
                using var context = new IdtDbContext();
                return context.ConstructionTaskReports.FirstOrDefault(report => report.Id == id);
            }
            catch
            {
                throw;
            }
        }

        public ConstructionTaskReport Save(ConstructionTaskReport entity)
        {
            try
            {
                using var context = new IdtDbContext();
                var report = context.ConstructionTaskReports.Add(entity);
                context.SaveChanges();
                return report.Entity;
            }
            catch
            {
                throw;
            }
        }

        public void Update(ConstructionTaskReport entity)
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
                var report = context.ConstructionTaskReports.FirstOrDefault(report => report.Id == id);
                if (report != null)
                {
                    context.ConstructionTaskReports.Remove(report);
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
