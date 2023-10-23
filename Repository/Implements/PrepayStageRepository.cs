using BusinessObject.Models;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository.Implements
{
    public class PrepayStageRepository : IPrepayStageRepository
    {
        public IEnumerable<PrepayStage> GetAll()
        {
            try
            {
                using var context = new IdtDbContext();
                return context.PrepayStages.ToList();
            }
            catch
            {
                throw;
            }
        }

        public PrepayStage? GetById(Guid id)
        {
            try
            {
                using var context = new IdtDbContext();
                return context.PrepayStages.FirstOrDefault(stage => stage.Id == id);
            }
            catch
            {
                throw;
            }
        }

        public PrepayStage Save(PrepayStage entity)
        {
            try
            {
                using var context = new IdtDbContext();
                var stage = context.PrepayStages.Add(entity);
                context.SaveChanges();
                return stage.Entity;
            }
            catch
            {
                throw;
            }
        }

        public void Update(PrepayStage entity)
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
                var stage = context.PrepayStages.FirstOrDefault(stage => stage.Id == id);
                if (stage != null)
                {
                    context.PrepayStages.Remove(stage);
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
