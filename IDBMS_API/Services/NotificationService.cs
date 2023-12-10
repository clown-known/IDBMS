using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces;
using Repository.Implements;

using IDBMS_API.DTOs.Request;
using BusinessObject.Enums;
using UnidecodeSharpFork;
using DocumentFormat.OpenXml.Spreadsheet;

namespace IDBMS_API.Services
{
    public class NotificationService 
    {
        private readonly INotificationRepository _repository;
        private readonly IUserRepository _userRepository;
        public NotificationService(INotificationRepository notificationRepository, IUserRepository userRepository)
        {
            _repository = notificationRepository;
            _userRepository = userRepository;
        }

        public IEnumerable<Notification> Filter(IEnumerable<Notification> list,
           NotificationCategory? category)
        {
            IEnumerable<Notification> filteredList = list;
 
            if (category != null)
            {
                filteredList = filteredList.Where(item => item.Category == category);
            }

            return filteredList;
        }

        public Notification? GetById(Guid id)
        {
            return _repository.GetById(id) ?? throw new Exception("This object is not existed!");
        }
        public IEnumerable<Notification> GetByUserId (Guid userId, NotificationCategory? category)
        {
            var list = _repository.GetByUserId(userId) ?? throw new Exception("This object is not existed!");

            return Filter(list, category);
        }
        public IEnumerable<Notification> GetAll(NotificationCategory? category)
        {
            var list = _repository.GetAll();

            return Filter(list, category);
        }
        public Notification? CreateNotificatonByUserId(NotificationRequest noti, Guid userId)
        {
            var user = _userRepository.GetById(userId) ?? throw new Exception("This object is not existed!");
            var notification = new Notification
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                Category = noti.Category,
                Content = noti.Content,
                Link = noti.Link,
                IsSeen = false,
            };
            var notiCreated = _repository.Save(notification);
            return notiCreated;
        }
        public void CreateNotificatonsForAllUsers(NotificationRequest noti)
        {
            var users = _userRepository.GetAll();
            var idList = users.Select(n => n.Id).ToList();

            foreach (var userId in idList) 
            {
                var notification = new Notification
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    Category = noti.Category,
                    Content = noti.Content,
                    Link = noti.Link,
                    IsSeen = false,
                };

                _repository.Save(notification);
            }

        }
        public void CreateNotificatonForUsers(NotificationRequest noti, List<Guid> listUserId)
        {
            if (listUserId == null) throw new Exception("List user is null!");

            foreach (var userId in listUserId)
            {
                var user = _userRepository.GetById(userId) ?? throw new Exception($"User with user id {userId} is not existed!");

                var notification = new Notification
                {
                    Id = Guid.NewGuid(),
                    UserId = user.Id,
                    Category = noti.Category,
                    Content = noti.Content,
                    Link = noti.Link,
                    IsSeen = false,
                };

                _repository.Save(notification);
            }
        }

        public void UpdateIsSeenById(Guid id)
        {
            var noti = _repository.GetById(id) ?? throw new Exception("This object is not existed!");

            noti.IsSeen = true;

            _repository.Update(noti);
        }

        public void UpdateIsSeenByUserId(Guid userId)
        {
            var listNotiByUserId = _repository.GetByUserId(userId);

            foreach (var noti in listNotiByUserId)
            {
                noti.IsSeen = true;

                _repository.Update(noti);
            }
        }
    }
}
