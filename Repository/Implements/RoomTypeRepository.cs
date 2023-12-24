using BusinessObject.Models;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implements
{
    public class RoomTypeRepository : IRoomTypeRepository
    {
        public void DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RoomType> GetAll()
        {
            try
            {
                using var context = new IdtDbContext();
                return context.RoomTypes
                    .OrderBy(r => r.PricePerArea)
                        .ThenByDescending(r => r.Id)
                    .ToList();
            }
            catch
            {
                throw;
            }
        }

        public RoomType? GetById(int id)
        {
            try
            {
                using var context = new IdtDbContext();
                return context.RoomTypes.Where(rt => rt.Id == id).FirstOrDefault();
            }
            catch
            {
                throw;
            }
        }

        public RoomType? Save(RoomType entity)
        {
            try
            {
                using var context = new IdtDbContext();
                var roomType = context.RoomTypes.Add(entity);
                context.SaveChanges();
                return roomType.Entity;
            }
            catch
            {
                throw;
            }
        }

        public void Update(RoomType entity)
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
