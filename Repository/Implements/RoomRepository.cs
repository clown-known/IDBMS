using BusinessObject.Models;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository.Implements
{
    public class RoomRepository : IRoomRepository
    {
        public IEnumerable<Room> GetAll()
        {
            try
            {
                using var context = new IdtDbContext();
                return context.Rooms.ToList();
            }
            catch
            {
                throw;
            }
        }

        public Room? GetById(Guid id)
        {
            try
            {
                using var context = new IdtDbContext();
                return context.Rooms.FirstOrDefault(room => room.Id == id);
            }
            catch
            {
                throw;
            }
        }

       /* public IEnumerable<Room> GetByProjectId(Guid projectId)
        {
            try
            {
                using var context = new IdtDbContext();
                return context.Rooms
                    .Where(room => room.ProjectId == projectId && room.IsHidden == false)
                    .ToList();
            }
            catch
            {
                throw;
            }
        }*/

        public IEnumerable<Room> GetByFloorId(Guid id)
        {
            try
            {
                using var context = new IdtDbContext();
                return context.Rooms
                    .Where(room => room.FloorId == id && room.IsHidden == false)
                    .ToList();
            }
            catch
            {
                throw;
            }
        }


        public Room Save(Room entity)
        {
            try
            {
                using var context = new IdtDbContext();
                var room = context.Rooms.Add(entity);
                context.SaveChanges();
                return room.Entity;
            }
            catch
            {
                throw;
            }
        }

        public void Update(Room entity)
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
                var room = context.Rooms.FirstOrDefault(room => room.Id == id);
                if (room != null)
                {
                    context.Rooms.Remove(room);
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
