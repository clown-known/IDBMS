using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
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
                return context.Rooms
                    .Include(rt => rt.RoomType)
                    .ToList()
                    .Reverse<Room>();
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
                return context.Rooms
                    .Include(rt => rt.RoomType)
                    .Include(t => t.Tasks)
                    .FirstOrDefault(room => room.Id == id);
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<Room> GetByProjectId(Guid id)
        {
            try
            {
                using var context = new IdtDbContext();
                return context.Rooms
                    .Include(f => f.Floor)
                    .Where(room => room.Floor != null && room.Floor.ProjectId == id && room.IsHidden == false)
                    .ToList()
                    .Reverse<Room>();
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<Room> GetByFloorId(Guid id)
        {
            try
            {
                using var context = new IdtDbContext();
                return context.Rooms
                    .Include(rt => rt.RoomType)
                    .Where(room => room.FloorId == id)
                    .ToList()
                    .Reverse<Room>();
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
