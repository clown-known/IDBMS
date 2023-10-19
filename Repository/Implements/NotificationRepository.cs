using BusinessObject.Models;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implements
{
    public class NotificationRepository : INotificationRepository
    {
        public void DeleteById(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Notification> GetAll()
        {
            throw new NotImplementedException();
        }

        public Notification? GetById(Guid id)
        {
            try
            {
                using var context = new IdtDbContext();
                return context.Notifications.Where(n => n.Id == id).FirstOrDefault();
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<Notification> GetByUserId(Guid id)
        {
            try
            {
                using var context = new IdtDbContext();
                return context.Notifications
                    .Where(n => n.UserId == id).ToList();
            }
            catch
            {
                throw;
            }
        }

        public Notification? Save(Notification entity)
        {
            try
            {
                using var context = new IdtDbContext();
                var noti = context.Notifications.Add(entity);
                context.SaveChanges();
                return noti.Entity;
            }
            catch
            {
                throw;
            }
        }

        public void Update(Notification entity)
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
