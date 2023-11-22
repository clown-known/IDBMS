using BusinessObject.Models;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository.Implements
{
    public class FloorRepository : IFloorRepository
    {
        public IEnumerable<Floor> GetAll()
        {
            try
            {
                using var context = new IdtDbContext();
                return context.Floors.ToList();
            }
            catch
            {
                throw;
            }
        }

        public Floor? GetById(Guid id)
        {
            try
            {
                using var context = new IdtDbContext();
                return context.Floors.FirstOrDefault(floor => floor.Id == id);
            }
            catch
            {
                throw;
            }
        }
        public IEnumerable<Floor?> GetBySiteId(Guid id)
        {
            try
            {
                using var context = new IdtDbContext();
                return context.Floors.Where(floor => floor.SiteId == id);
            }
            catch
            {
                throw;
            }
        }
        public Floor Save(Floor entity)
        {
            try
            {
                using var context = new IdtDbContext();
                var floor = context.Floors.Add(entity);
                context.SaveChanges();
                return floor.Entity;
            }
            catch
            {
                throw;
            }
        }

        public void Update(Floor entity)
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
                var floor = context.Floors.FirstOrDefault(floor => floor.Id == id);
                if (floor != null)
                {
                    context.Floors.Remove(floor);
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
