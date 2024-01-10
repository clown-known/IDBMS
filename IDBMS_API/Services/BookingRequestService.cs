using BLL.Services;
using BusinessObject.Models;
using BusinessObject.Enums;
using IDBMS_API.DTOs.Request;
using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using UnidecodeSharpFork;
using Azure.Core;
using API.Services;
using API.Supporters.JwtAuthSupport;
using IDBMS_API.DTOs.Request.AccountRequest;
using IDBMS_API.Supporters.JwtAuthSupport;
using IDBMS_API.Supporters.TimeHelper;

namespace IDBMS_API.Services
{
    public class BookingRequestService
    {
        private readonly IBookingRequestRepository _bookingRequestRepo;
        private readonly IUserRepository _userRepo;
        private readonly JwtTokenSupporter jwtTokenSupporter;
        private readonly GoogleTokenVerify googleTokenVerify;

        public BookingRequestService(IBookingRequestRepository bookingRequestRepo, IUserRepository userRepo, JwtTokenSupporter jwtTokenSupporter, GoogleTokenVerify googleTokenVerify)
        {
            _bookingRequestRepo = bookingRequestRepo;
            _userRepo = userRepo;
            this.jwtTokenSupporter= jwtTokenSupporter;
            this.googleTokenVerify= googleTokenVerify;
        }

        public IEnumerable<BookingRequest> Filter(IEnumerable<BookingRequest> list,
           BookingRequestStatus? status, string? contactName)
        {
            IEnumerable<BookingRequest> filteredList = list;

            if (status != null)
            {
                filteredList = filteredList.Where(item => item.Status == status);
            }

            if (contactName != null)
            {
                filteredList = filteredList.Where(item => (item.ContactName != null && item.ContactName.Unidecode().IndexOf(contactName.Unidecode(), StringComparison.OrdinalIgnoreCase) >= 0));
            }

            return filteredList;
        }

        public BookingRequest? GetById(Guid id)
        {
            return _bookingRequestRepo.GetById(id) ?? throw new Exception("This booking request id is not existed!");
        }
        public IEnumerable<BookingRequest> GetAll(BookingRequestStatus? status, string? contactName)
        {
            var list = _bookingRequestRepo.GetAll();

            return Filter(list, status, contactName);
        }
        public BookingRequest? CreateBookingRequest(BookingRequestRequest request)
        {
            UserService userService = new (_userRepo, jwtTokenSupporter, googleTokenVerify);
            var user = userService.GetByEmail(request.ContactEmail);

            var br = new BookingRequest
            {
                Id = Guid.NewGuid(),
                ContactName= request.ContactName,
                ContactEmail = request.ContactEmail,
                ContactPhone = request.ContactPhone,
                ContactLocation = request.ContactLocation,
                Note = request.Note,
                Status = BookingRequestStatus.Pending,
                IsDeleted = false,
                CreatedDate= TimeHelper.GetTime(DateTime.Now),
            };

            if (user == null)
            {
                var newUser = new CreateUserRequest
                {
                    Name = request.ContactName,
                    Address = request.ContactLocation,
                    Email= request.ContactEmail,
                    Phone= request.ContactPhone,
                    Language = request.Language,
                    Role = CompanyRole.Customer,
                };

                var createdUser = userService.GenerateUser(newUser);

                br.UserId = createdUser.Id;

                var bookingRequestCreated = _bookingRequestRepo.Save(br);
                return bookingRequestCreated;
            }
            else
            {
                var pendingRequest = user.BookingRequests.Any(request => request.Status == BookingRequestStatus.Pending);

                if (pendingRequest == false && user.Status == UserStatus.Active)
                {
                    br.UserId = user.Id;
                    var bookingRequestCreated = _bookingRequestRepo.Save(br);
                    return bookingRequestCreated;
                }
                else
                {
                    throw new Exception("This user cannot create booking request at the moment!");
                }
            }

            return null;
        }
        public void UpdateBookingRequest(Guid id, BookingRequestRequest request)
        {
            var br = _bookingRequestRepo.GetById(id) ?? throw new Exception("This booking request id is not existed!");

            br.ContactName = request.ContactName;
            br.ContactEmail = request.ContactEmail;
            br.ContactPhone = request.ContactPhone;
            br.ContactLocation = request.ContactLocation;
            br.Note = request.Note;
            br.UpdatedDate= TimeHelper.GetTime(DateTime.Now);

            _bookingRequestRepo.Update(br);
        }

        public void UpdateBookingRequestStatus(Guid id, BookingRequestStatus status)
        {
            var br = _bookingRequestRepo.GetById(id) ?? throw new Exception("This booking request id is not existed!");

            br.Status = status;
            br.UpdatedDate = TimeHelper.GetTime(DateTime.Now);

            _bookingRequestRepo.Update(br);
        }

        public void ProcessBookingRequest(Guid id, BookingRequestStatus status, string adminReply)
        {
            var br = _bookingRequestRepo.GetById(id) ?? throw new Exception("This booking request id is not existed!");

            br.UpdatedDate = TimeHelper.GetTime(DateTime.Now);
            br.AdminReply = adminReply;
            br.Status = status;

            _bookingRequestRepo.Update(br);
        }

        public void DeleteBookingRequest(Guid id)
        {
            var br = _bookingRequestRepo.GetById(id) ?? throw new Exception("This booking request id is not existed!");

            br.IsDeleted = true;
            br.UpdatedDate = TimeHelper.GetTime(DateTime.Now);

            _bookingRequestRepo.Update(br);
        }
    }
}
