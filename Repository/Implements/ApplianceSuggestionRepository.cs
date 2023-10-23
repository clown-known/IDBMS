using BusinessObject.Models;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository.Implements
{
    public class ApplianceSuggestionRepository : IApplianceSuggestionRepository
    {
        public IEnumerable<ApplianceSuggestion> GetAll()
        {
            try
            {
                using var context = new IdtDbContext();
                return context.ApplianceSuggestions.ToList();
            }
            catch
            {
                throw;
            }
        }

        public ApplianceSuggestion? GetById(Guid id)
        {
            try
            {
                using var context = new IdtDbContext();
                return context.ApplianceSuggestions.FirstOrDefault(asg => asg.Id == id);
            }
            catch
            {
                throw;
            }
        }

        public ApplianceSuggestion Save(ApplianceSuggestion entity)
        {
            try
            {
                using var context = new IdtDbContext();
                var asg = context.ApplianceSuggestions.Add(entity);
                context.SaveChanges();
                return asg.Entity;
            }
            catch
            {
                throw;
            }
        }

        public void Update(ApplianceSuggestion entity)
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
                var asg = context.ApplianceSuggestions.FirstOrDefault(asg => asg.Id == id);
                if (asg != null)
                {
                    context.ApplianceSuggestions.Remove(asg);
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
