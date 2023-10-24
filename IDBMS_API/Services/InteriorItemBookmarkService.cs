using BusinessObject.DTOs.Request.CreateRequests;
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
        public InteriorItemBookmark? GetById(Guid id)
        {
            return _repository.GetById(id);
        }
        public InteriorItemBookmark? CreateInteriorItemBookmark(InteriorItemBookmarkRequest request)
        {
            var iib = new InteriorItemBookmark
            {
                UserId = request.UserId,
                InteriorItemId = request.InteriorItemId,
            };
            var iibCreated = _repository.Save(iib);
            return iibCreated;
        }
        public void UpdateInteriorItemBookmark(InteriorItemBookmarkRequest request)
        {
            var iib = new InteriorItemBookmark
            {
                UserId = request.UserId,
                InteriorItemId = request.InteriorItemId,
            };
            _repository.Update(iib);
        }
        public void DeleteInteriorItemBookmark(Guid id)
        {
            _repository.DeleteById(id);
        }
    }
}
