using IDBMS_API.DTOs.Request;
using BusinessObject.Models;
using Repository.Implements;
using Repository.Interfaces;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;
using BusinessObject.Enums;
using DocumentFormat.OpenXml.Wordprocessing;

namespace IDBMS_API.Services
{
    public class InteriorItemCategoryService
    {
        private readonly IInteriorItemCategoryRepository _repository;
        public InteriorItemCategoryService(IInteriorItemCategoryRepository repository)
        {
            _repository = repository;
        }

        private IEnumerable<InteriorItemCategory> Filter(IEnumerable<InteriorItemCategory> list,
           InteriorItemType? type, string? name)
        {
            IEnumerable<InteriorItemCategory> filteredList = list;

            if (type != null)
            {
                filteredList = filteredList.Where(item => item.InteriorItemType == type);
            }

            if (name != null)
            {
                filteredList = filteredList.Where(item => item.Name == name);
            }

            return filteredList;
        }

        public IEnumerable<InteriorItemCategory> GetAll(InteriorItemType? type, string? name)
        {
            var list = _repository.GetAll();

            return Filter(list, type, name);
        }
        public InteriorItemCategory? GetById(int id)
        {
            return _repository.GetById(id) ?? throw new Exception("This item category id is not existed!");
        }
        public async Task<InteriorItemCategory?> CreateInteriorItemCategory(InteriorItemCategoryRequest request)
        {  
            var iic = new InteriorItemCategory
            {
                Name = request.Name,
                EnglishName = request.EnglishName,
                Description = request.Description,
                EnglishDescription = request.EnglishDescription,
                InteriorItemType = request.InteriorItemType,
                ParentCategoryId = request.ParentCategoryId,
                IsDeleted = false,
            };

            if (request.BannerImage != null)
            {
                FirebaseService s = new FirebaseService();
                string link = await s.UploadImage(request.BannerImage);

                iic.BannerImageUrl = link;
            }

            if (request.IconImage != null)
            {
                FirebaseService s = new FirebaseService();
                string link = await s.UploadImage(request.IconImage);

                iic.IconImageUrl = link;
            }

            var iicCreated = _repository.Save(iic);
            return iicCreated;
        }
        public async Task UpdateInteriorItemCategory(int id, InteriorItemCategoryRequest request)
        {
            var iic = _repository.GetById(id) ?? throw new Exception("This item category id is not existed!");

            if (request.BannerImage != null)
            {
                FirebaseService s = new FirebaseService();
                string link = await s.UploadImage(request.BannerImage);

                iic.BannerImageUrl = link;
            }

            if (request.IconImage != null)
            {
                FirebaseService s = new FirebaseService();
                string link = await s.UploadImage(request.IconImage);

                iic.IconImageUrl = link;
            }

            iic.Name = request.Name;
            iic.EnglishName = request.EnglishName;
            iic.Description = request.Description;
            iic.EnglishDescription = request.EnglishDescription;

            iic.InteriorItemType = request.InteriorItemType; 
            iic.ParentCategoryId = request.ParentCategoryId;

            

            _repository.Update(iic);
        }
        public void DeleteInteriorItemCategory(int id)
        {
            var iic = _repository.GetById(id) ?? throw new Exception("This item category id is not existed!");

            iic.IsDeleted = true;
            //interior item

            _repository.Update(iic);
        }
    }
}
