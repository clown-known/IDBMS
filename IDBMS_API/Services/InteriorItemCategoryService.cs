using BusinessObject.DTOs.Request.CreateRequests;
using BusinessObject.Models;
using Repository.Interfaces;

namespace IDBMS_API.Services
{
    public class InteriorItemCategoryService
    {
        private readonly IInteriorItemCategoryRepository _repository;
        public InteriorItemCategoryService(IInteriorItemCategoryRepository repository)
        {
            _repository = repository;
        }
        public IEnumerable<InteriorItemCategory> GetAll()
        {
            return _repository.GetAll();
        }
        public InteriorItemCategory? GetById(int id)
        {
            return _repository.GetById(id);
        }
        public InteriorItemCategory? CreateInteriorItemCategory(InteriorItemCategoryRequest request)
        {
            var iic = new InteriorItemCategory
            {
                Name = request.Name,
                Description = request.Description,
                BannerImageUrl = request.BannerImageUrl,
                IconImageUrl = request.IconImageUrl,
                InteriorItemType = request.InteriorItemType,
                ParentCategoryId = request.ParentCategoryId,
                IsDeleted = request.IsDeleted,
            };
            var iicCreated = _repository.Save(iic);
            return iicCreated;
        }
        public void UdpateInteriorItemCategory(InteriorItemCategoryRequest request)
        {
            var iic = new InteriorItemCategory
            {
                Name = request.Name,
                Description = request.Description,
                BannerImageUrl = request.BannerImageUrl,
                IconImageUrl = request.IconImageUrl,
                InteriorItemType = request.InteriorItemType,
                ParentCategoryId = request.ParentCategoryId,
                IsDeleted = request.IsDeleted,
            };
            _repository.Update(iic);
        }
    }
}
