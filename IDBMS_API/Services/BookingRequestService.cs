using BLL.Services;
using BusinessObject.Models;
using BusinessObject.Enums;
using IDBMS_API.DTOs.Request;
using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;

namespace IDBMS_API.Services
{
    public class BookingRequestService
    {
        private readonly IBookingRequestRepository _repository;
        public BookingRequestService(IBookingRequestRepository _repository)
        {
            this._repository = _repository;
        }
        public BookingRequest? GetById(Guid id)
        {
            return _repository.GetById(id) ?? throw new Exception("This object is not existed!");
        }
        public IEnumerable<BookingRequest> GetAll()
        {
            return _repository.GetAll();
        }
        public BookingRequest? CreateBookingRequest([FromForm] BookingRequestRequest BookingRequest)
        {
            var br = new BookingRequest
            {
                Id = Guid.NewGuid(),
                ProjectType = BookingRequest.ProjectType,
                ContactName= BookingRequest.ContactName,
                ContactEmail = BookingRequest.ContactEmail,
                ContactPhone = BookingRequest.ContactPhone,
                ContactLocation = BookingRequest.ContactLocation,
                Note = BookingRequest.Note,
                UserId = BookingRequest.UserId,
                Status = BookingRequestStatus.Pending,
                IsDeleted = false,
                CreatedDate= DateTime.Now,
                AdminReply = BookingRequest.AdminReply,
            };

            var BookingRequestCreated = _repository.Save(br);
            return BookingRequestCreated;
        }
        public void UpdateBookingRequest(Guid id, [FromForm] BookingRequestRequest BookingRequest)
        {
            var br = _repository.GetById(id) ?? throw new Exception("This object is not existed!");

            br.ProjectType = BookingRequest.ProjectType;
            br.ContactName = BookingRequest.ContactName;
            br.ContactEmail = BookingRequest.ContactEmail;
            br.ContactPhone = BookingRequest.ContactPhone;
            br.ContactLocation = BookingRequest.ContactLocation;
            br.Note = BookingRequest.Note;
            br.UpdatedDate= DateTime.Now;
            br.AdminReply = BookingRequest.AdminReply;

            _repository.Update(br);
        }

        public void UpdateBookingRequestStatus(Guid id, BookingRequestStatus status)
        {
            var br = _repository.GetById(id) ?? throw new Exception("This object is not existed!");

            br.Status = status;
            br.UpdatedDate = DateTime.Now;

            _repository.Update(br);
        }

        public void ProcessBookingRequest(Guid id, string adminReply)
        {
            var br = _repository.GetById(id) ?? throw new Exception("This object is not existed!");

            br.UpdatedDate = DateTime.Now;
            br.AdminReply = adminReply;

            _repository.Update(br);
        }

        public void DeleteBookingRequest(Guid id)
        {
            var br = _repository.GetById(id) ?? throw new Exception("This object is not existed!");

            br.IsDeleted = true;
            br.UpdatedDate = DateTime.Now;

            _repository.Update(br);
        }
    }
}
