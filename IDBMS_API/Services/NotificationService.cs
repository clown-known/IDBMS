using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces;
using IDBMS_API.DTOs.Request;
using BusinessObject.DTOs.Request;
using Repository.Implements;
using Firebase.Auth;

namespace IDBMS_API.Services
{
    public class NotificationService 
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IUserRepository _userRepository;
        public NotificationService(INotificationRepository notificationRepository, IUserRepository userRepository)
        {
            _notificationRepository = notificationRepository;
            _userRepository = userRepository;
        }
        public Notification GetById(Guid id)
        {
            return _notificationRepository.GetById(id);
        }
        public IEnumerable<Notification> GetByUserId (Guid userId)
        {
            return _notificationRepository.GetByUserId(userId);
        }
        public IEnumerable<Notification> GetAll()
        {
            return _notificationRepository.GetAll();
        }
        public async Task<Notification?> CreateNotificatonByUserId(NotificationRequest noti, Guid userId)
        {
            var user = _userRepository.GetById(userId) ?? throw new Exception("User not existed");
            var notification = new Notification
            {
                UserId = userId,
                Category = noti.Category,
                Content = noti.Content,
                Link = noti.Link,
                IsSeen = noti.IsSeen,
            };
            var notiCreated = _notificationRepository.Save(notification);
            return notiCreated;
        }
        public async Task<Notification?> CreateNotificaton(NotificationRequest noti)
        {
            var allUser = _userRepository.GetAll();
            var allUserId = allUser.Select(n => n.Id).ToList();
            Notification notiCreated = null;
            foreach (var userId in allUserId) {
                var notification = new Notification
                {
                    UserId = userId,
                    Category = noti.Category,
                    Content = noti.Content,
                    Link = noti.Link,
                    IsSeen = noti.IsSeen,
                };
                notiCreated = _notificationRepository.Save(notification);
            }
            return notiCreated;
        }
        public async Task<Notification?> CreateNotificatonByListUserId(NotificationRequest noti, List<Guid> listUserId)
        {
            Notification notiCreated = null;
            foreach (var userId in listUserId)
            {
                var user = _userRepository.GetById(userId) ?? throw new Exception("User not existed");
                var notification = new Notification
                {
                    UserId = userId,
                    Category = noti.Category,
                    Content = noti.Content,
                    Link = noti.Link,
                    IsSeen = noti.IsSeen,
                };
                notiCreated = _notificationRepository.Save(notification);
            }
            return notiCreated;
        }
        public async Task UpdateIsSeenById(Guid id)
        {
            var noti = _notificationRepository.GetById(id) ?? throw new Exception("This notification not exist");
            var notification = new Notification
            {
                UserId = noti.UserId,
                Category = noti.Category,
                Content = noti.Content,
                Link = noti.Link,
                IsSeen = true,
            };
            _notificationRepository.Update(notification);
        }
        public async Task UpdateIsSeenByUserId(Guid userId)
        {
            var allNotiByUserId = _notificationRepository.GetByUserId(userId);
            foreach (var noti in allNotiByUserId)
            {
                var notification = new Notification
                {
                    UserId = noti.UserId,
                    Category = noti.Category,
                    Content = noti.Content,
                    Link = noti.Link,
                    IsSeen = true,
                };
                _notificationRepository.Update(notification);
            }
        }
    }
}
