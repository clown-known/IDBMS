using IDBMS_API.DTOs.Request;
using BusinessObject.Models;
using Repository.Implements;
using Repository.Interfaces;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<InteriorItemCategory?> CreateInteriorItemCategory([FromForm] InteriorItemCategoryRequest request)
        {
            FirebaseService s = new FirebaseService();
            string BannerImageUrl = await s.UploadImage(request.IconImage);
            string IconImageUrl = await s.UploadImage(request.IconImage);
            var iic = new InteriorItemCategory
            {
                Name = request.Name,
                EnglishName = request.EnglishName,
                Description = request.Description,
                EnglishDescription = request.EnglishDescription,
                BannerImageUrl = BannerImageUrl,
                IconImageUrl = IconImageUrl,
                InteriorItemType = request.InteriorItemType,
                ParentCategoryId = request.ParentCategoryId,
                IsDeleted = false,
            };

            var iicCreated = _repository.Save(iic);
            return iicCreated;
        }
        public async void UpdateInteriorItemCategory(int id, [FromForm] InteriorItemCategoryRequest request)
        {
            FirebaseService s = new FirebaseService();
            string BannerImageUrl = await s.UploadImage(request.IconImage);
            string IconImageUrl = await s.UploadImage(request.IconImage);
            var iic = _repository.GetById(id) ?? throw new Exception("This object is not existed!");
            iic.Name = request.Name;
            iic.EnglishName = request.EnglishName;
            iic.Description = request.Description;
            iic.EnglishDescription = request.EnglishDescription;
            iic.BannerImageUrl = BannerImageUrl;
            iic.IconImageUrl = IconImageUrl; 
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
