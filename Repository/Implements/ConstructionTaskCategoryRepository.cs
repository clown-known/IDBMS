using BusinessObject.Models;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implements
{
    public class ConstructionTaskCategoryRepository : IConstructionTaskCategoryRepository
    {
        public void DeleteById(int id)
        {
            try
            {
                using var context = new IdtDbContext();
                var ctc = context.ConstructionTaskCategories.Where(ctc => ctc.Id == id).FirstOrDefault();
                if (ctc != null)
                {
                    context.ConstructionTaskCategories.Remove(ctc);
                    context.SaveChanges();
                }
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<ConstructionTaskCategory> GetAll()
        {
            try
            {
                using var context = new IdtDbContext();
                return context.ConstructionTaskCategories.ToList();
            }
            catch
            {
                throw;
            }
        }

        public ConstructionTaskCategory? GetById(int id)
        {
            try
            {
                using var context = new IdtDbContext();
                return context.ConstructionTaskCategories.Where(ctc => ctc.Id == id).FirstOrDefault();
            }
            catch
            {
                throw;
            }
        }

        public ConstructionTaskCategory? Save(ConstructionTaskCategory entity)
        {
            try
            {
                using var context = new IdtDbContext();
                var ctc = context.ConstructionTaskCategories.Add(entity);
                context.SaveChanges();
                return ctc.Entity;
            }
            catch
            {
                throw;
            }
        }

        public void Update(ConstructionTaskCategory entity)
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
