using BusinessObject.Models;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implements
{
    public class PrepayStageDesignRepository : IPrepayStageDesignRepository
    {
        public void DeleteById(int id)
        {
            try
            {
                using var context = new IdtDbContext();
                var psd = context.PrepayStageDesigns.Where(psd => psd.Id == id).FirstOrDefault();
                context.PrepayStageDesigns.Remove(psd);
                context.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<PrepayStageDesign> GetAll()
        {
            try
            {
                using var context = new IdtDbContext();
                return context.PrepayStageDesigns.ToList();
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<PrepayStageDesign> GetByDecorProjectDesignId(int designId)
        {
            try
            {
                using var context = new IdtDbContext();
                return context.PrepayStageDesigns
                    .Where(psd => psd.DecorProjectDesignId == designId).ToList();
            }
            catch
            {
                throw;
            }
        }

        public PrepayStageDesign? GetById(int id)
        {
            try
            {
                using var context = new IdtDbContext();
                return context.PrepayStageDesigns.Where(psd => psd.Id == id).FirstOrDefault();
            }
            catch
            {
                throw;
            }
        }

        public PrepayStageDesign? Save(PrepayStageDesign entity)
        {
            try
            {
                using var context = new IdtDbContext();
                var psd = context.PrepayStageDesigns.Add(entity);
                context.SaveChanges();
                return psd.Entity;
            }
            catch
            {
                throw;
            }
        }

        public void Update(PrepayStageDesign entity)
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
