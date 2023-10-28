using BusinessObject.DTOs.Request;
using BusinessObject.Models;
using Repository.Implements;
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
            return _repository.GetById(id) ?? throw new Exception("This object is not existed!");
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
                IsDeleted = false,
            };

            var iicCreated = _repository.Save(iic);
            return iicCreated;
        }
        public void UdpateInteriorItemCategory(int id, InteriorItemCategoryRequest request)
        {
            var iic = _repository.GetById(id) ?? throw new Exception("This object is not existed!");
            iic.Name = request.Name;
            iic.Description = request.Description;
            iic.BannerImageUrl = request.BannerImageUrl;
            iic.IconImageUrl = request.IconImageUrl; 
            iic.InteriorItemType = request.InteriorItemType; 
            iic.ParentCategoryId = request.ParentCategoryId;

            _repository.Update(iic);
        }
        public void DeleteInteriorItemCategory(int id)
        {
            var iic = _repository.GetById(id) ?? throw new Exception("This object is not existed!");

            iic.IsDeleted = true;
            //interior item

            _repository.Update(iic);
        }
    }
}
