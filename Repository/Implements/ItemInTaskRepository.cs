using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implements
{
    public class ItemInTaskRepository : IItemInTaskRepository
    {
        public IEnumerable<ItemInTask> GetAll()
        {
            try
            {
                using var context = new IdtDbContext();
                return context.ItemInTasks
                    .Include(ItemInTask => ItemInTask.ProjectTask)
                    .Include(ItemInTask => ItemInTask.InteriorItem)
                    .ToList()
                    .Reverse<ItemInTask>();
            }
            catch
            {
                throw;
            }
        }

        public ItemInTask? GetById(Guid id)
        {
            try
            {
                using var context = new IdtDbContext();
                return context.ItemInTasks
                    .Include(ItemInTask => ItemInTask.ProjectTask)
                    .Include(ItemInTask => ItemInTask.InteriorItem)
                    .FirstOrDefault(ItemInTask => ItemInTask.Id == id);
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<ItemInTask> GetByProjectId(Guid id)
        {
            try
            {
                using var context = new IdtDbContext();
                return context.ItemInTasks
                    .Include(ItemInTask => ItemInTask.ProjectTask)
                    .Include(ItemInTask => ItemInTask.InteriorItem)
                        .ThenInclude(category => category.InteriorItemCategory)
                    .Where(ItemInTask => ItemInTask.ProjectId == id)
                    .ToList()
                    .Reverse<ItemInTask>();
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<ItemInTask> GetByRoomId(Guid id)
        {
            try
            {
                using var context = new IdtDbContext();
                return context.ItemInTasks
                    .Include(ItemInTask => ItemInTask.ProjectTask)
                    .Include(ItemInTask => ItemInTask.InteriorItem)
                    .Where(item => item.ProjectTask != null && item.ProjectTask.RoomId == id)
                    .ToList()
                    .Reverse<ItemInTask>();
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<ItemInTask> GetByTaskId(Guid id)
        {
            try
            {
                using var context = new IdtDbContext();
                return context.ItemInTasks
                    .Include(ItemInTask => ItemInTask.InteriorItem)
                    .Where(ItemInTask => ItemInTask.ProjectTaskId == id)
                    .ToList()
                    .Reverse<ItemInTask>();
            }
            catch
            {
                throw;
            }
        }


        public ItemInTask Save(ItemInTask entity)
        {
            try
            {
                using var context = new IdtDbContext();
                var ItemInTask = context.ItemInTasks.Add(entity);
                context.SaveChanges();
                return ItemInTask.Entity;
            }
            catch
            {
                throw;
            }
        }

        public void Update(ItemInTask entity)
        {
            try
            {
                using var context = new IdtDbContext();
                context.Entry(entity).State = EntityState.Modified;
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
                var ItemInTask = context.ItemInTasks.FirstOrDefault(ItemInTask => ItemInTask.Id == id);
                if (ItemInTask != null)
                {
                    context.ItemInTasks.Remove(ItemInTask);
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
