using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
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
                return context.Floors
                    .Where(floor => floor.IsDeleted == false)
                    .ToList()
                    .Reverse<Floor>();
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
                return context.Floors
                    .Include(r => r.Rooms)
                        .ThenInclude(rt => rt.RoomType)
                    .FirstOrDefault(floor => floor.Id == id);
            }
            catch
            {
                throw;
            }
        }
        public IEnumerable<Floor> GetByProjectId(Guid id)
        {
            try
            {
                using var context = new IdtDbContext();
                return context.Floors
                    .Include(r => r.Rooms)
                    .Where(floor => floor.ProjectId == id && floor.IsDeleted == false)
                    .OrderBy(floor => floor.FloorNo)
                    .ToList();
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
            throw new NotImplementedException();
        }

    }
}
