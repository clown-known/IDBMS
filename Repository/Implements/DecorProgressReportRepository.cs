using BusinessObject.Models;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository.Implements
{
    public class DecorProgressReportRepository : IDecorProgressReportRepository
    {
        public IEnumerable<DecorProgressReport> GetAll()
        {
            try
            {
                using var context = new IdtDbContext();
                return context.DecorProgressReports.ToList();
            }
            catch
            {
                throw;
            }
        }

        public DecorProgressReport? GetById(Guid id)
        {
            try
            {
                using var context = new IdtDbContext();
                return context.DecorProgressReports.FirstOrDefault(report => report.Id == id);
            }
            catch
            {
                throw;
            }
        }

        public DecorProgressReport Save(DecorProgressReport entity)
        {
            try
            {
                using var context = new IdtDbContext();
                var report = context.DecorProgressReports.Add(entity);
                context.SaveChanges();
                return report.Entity;
            }
            catch
            {
                throw;
            }
        }

        public void Update(DecorProgressReport entity)
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
                var report = context.DecorProgressReports.FirstOrDefault(report => report.Id == id);
                if (report != null)
                {
                    context.DecorProgressReports.Remove(report);
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
