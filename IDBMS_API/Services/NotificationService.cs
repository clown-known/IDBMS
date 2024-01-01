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
        private readonly IConfiguration _configuration;

        public NotificationService(INotificationRepository notificationRepository, IUserRepository userRepository, IConfiguration configuration)
        {
            _repository = notificationRepository;
            _userRepository = userRepository;
            _configuration = configuration;
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
            return _repository.GetById(id) ?? throw new Exception("This notification id is not existed!");
        }
        public IEnumerable<Notification> GetByUserId (Guid userId, NotificationCategory? category)
        {
            var list = _repository.GetByUserId(userId) ?? throw new Exception("This notification id is not existed!");

            return Filter(list, category);
        }
        public IEnumerable<Notification> GetAll(NotificationCategory? category)
        {
            var list = _repository.GetAll();

            return Filter(list, category);
        }

        public void CreateNotificatonsForAllCustomers(NotificationRequest request)
        {
            var users = _userRepository.GetAll();
            var idList = users.Select(n => n.Id).ToList();

            foreach (var userId in idList) 
            {
                var notification = new Notification
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    Category = request.Category,
                    Content = request.Content,
                    CreatedDate= DateTime.Now,
                    IsSeen = false,
                };

                _repository.Save(notification);
            }

        }
        public void CreateNotificatonForProject(Guid projectId, NotificationRequest request)
        {
            var managementWebClientURL = _configuration["ManagementWebClientURL"];
            var projectLink = $"{managementWebClientURL}/projects/{projectId}";

            foreach (var userId in request.ListUserId)
            {
                var user = _userRepository.GetById(userId) ?? throw new Exception($"User with user id {userId} is not existed!");

                var notification = new Notification
                {
                    Id = Guid.NewGuid(),
                    UserId = user.Id,
                    Category = request.Category,
                    Content = request.Content,
                    Link= projectLink,
                    CreatedDate = DateTime.Now,
                    IsSeen = false,
                };

                _repository.Save(notification);
            }
        }

        public void UpdateIsSeenById(Guid id)
        {
            var request = _repository.GetById(id) ?? throw new Exception("This notification id is not existed!");

            request.IsSeen = true;

            _repository.Update(request);
        }

        public void UpdateIsSeenByUserId(Guid userId)
        {
            var listNotiByUserId = _repository.GetByUserId(userId);

            foreach (var request in listNotiByUserId)
            {
                request.IsSeen = true;

                _repository.Update(request);
            }
        }
    }
}
