using IDBMS_API.DTOs.Request;
using BusinessObject.Models;
using Repository.Interfaces;
using UnidecodeSharpFork;

namespace IDBMS_API.Services
{
    public class InteriorItemBookmarkService
    {
        private readonly IInteriorItemBookmarkRepository _repository;
        public InteriorItemBookmarkService(IInteriorItemBookmarkRepository repository)
        {
            _repository = repository;
        }

        private IEnumerable<InteriorItemBookmark> Filter(IEnumerable<InteriorItemBookmark> list, string? name)
        {
            IEnumerable<InteriorItemBookmark> filteredList = list;

            if (name != null)
            {
                filteredList = filteredList.Where(item =>
                           (item.InteriorItem.Name != null && item.InteriorItem.Name.Unidecode().IndexOf(name.Unidecode(), StringComparison.OrdinalIgnoreCase) >= 0)
                           || (item.InteriorItem.EnglishName != null && item.InteriorItem.EnglishName.Unidecode().IndexOf(name.Unidecode(), StringComparison.OrdinalIgnoreCase) >= 0));
            }

            return filteredList;
        }

        public IEnumerable<InteriorItemBookmark> GetAll()
        {
            return _repository.GetAll();
        }

        public IEnumerable<InteriorItemBookmark> GetByUserId(Guid userId, string? name)
        {
            var list = _repository.GetByUserId(userId);

            return Filter(list, name);
        }

        public InteriorItemBookmark GetById(Guid id)
        {
            return _repository.GetById(id) ?? throw new Exception("This bookmark id is not existed!");
        }

        public InteriorItemBookmark? CreateInteriorItemBookmark(InteriorItemBookmarkRequest request)
        {
            bool exist = _repository.GetByUserId(request.UserId).Any(bm => bm.InteriorItemId == request.InteriorItemId);
            if (exist)
            {
                return null;
            }
            
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
