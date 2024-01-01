using IDBMS_API.DTOs.Request;
using BusinessObject.Models;
using Repository.Interfaces;

namespace IDBMS_API.Services
{
    public class InteriorItemBookmarkService
    {
        private readonly IInteriorItemBookmarkRepository _repository;
        public InteriorItemBookmarkService(IInteriorItemBookmarkRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<InteriorItemBookmark> GetAll()
        {
            return _repository.GetAll();
        }

        public IEnumerable<InteriorItemBookmark> GetByUserId(Guid userId)
        {
            return _repository.GetByUserId(userId) ?? throw new Exception("This bookmark id is not existed!");
        }

        public InteriorItemBookmark GetById(Guid id)
        {
            return _repository.GetById(id) ?? throw new Exception("This bookmark id is not existed!");
        }

        public InteriorItemBookmark? CreateInteriorItemBookmark(InteriorItemBookmarkRequest request)
        {
            var iib = new InteriorItemBookmark
            {
                Id = Guid.NewGuid(),
                UserId = request.UserId,
                InteriorItemId = request.InteriorItemId,
            };

            var iibCreated = _repository.Save(iib);
            return iibCreated;
        }

        public void UpdateInteriorItemBookmark(Guid id, InteriorItemBookmarkRequest request)
        {
            var iib = _repository.GetById(id) ?? throw new Exception("This bookmark id is not existed!");

            iib.UserId = request.UserId;
            iib.InteriorItemId = request.InteriorItemId;

            _repository.Update(iib);
        }

        public void DeleteInteriorItemBookmark(Guid id)
        {
            _repository.DeleteById(id);
        }
    }
}
