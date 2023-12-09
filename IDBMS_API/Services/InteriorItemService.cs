using IDBMS_API.DTOs.Request;
using BusinessObject.Enums;
using BusinessObject.Models;
using Newtonsoft.Json.Linq;
using Repository.Interfaces;
using BLL.Services;
using Microsoft.AspNetCore.OData.Formatter.Wrapper;
using Microsoft.AspNetCore.Mvc;
using UnidecodeSharpFork;
using DocumentFormat.OpenXml.Wordprocessing;

namespace IDBMS_API.Services
{
    public class InteriorItemService
    {
        private readonly IInteriorItemRepository _repository;
        public InteriorItemService(IInteriorItemRepository repository)
        {
            _repository = repository;
        }

        private IEnumerable<InteriorItem> Filter(IEnumerable<InteriorItem?> list,
            int? itemCategoryId, InteriorItemStatus? status, string? codeOrName, InteriorItemType? cateType)
        {
            IEnumerable<InteriorItem> filteredList = list;
            
            if (itemCategoryId != null)
            {
                filteredList = filteredList.Where(item => item.InteriorItemCategoryId == itemCategoryId);
            }

            if (status != null)
            {
                filteredList = filteredList.Where(item => item.Status == status);
            }

            if (codeOrName != null)
            {
                filteredList = filteredList.Where(item =>
                           (item.Code != null && item.Code.IndexOf(codeOrName, StringComparison.OrdinalIgnoreCase) >= 0) ||
                           (item.Name != null && item.Name.Unidecode().IndexOf(codeOrName.Unidecode(), StringComparison.OrdinalIgnoreCase) >= 0));
            }

            if (cateType != null)
            {
                filteredList = filteredList.Where(item => item.InteriorItemCategory.InteriorItemType == cateType);
            }

            return filteredList;
        }

        public IEnumerable<InteriorItem> GetAll(int? itemCategoryId, InteriorItemStatus? status, string? codeOrName, InteriorItemType? cateType)
        {
            var list = _repository.GetAll();

            return Filter(list, itemCategoryId, status, codeOrName, cateType);
        }
        public InteriorItem? GetById(Guid id)
        {
            return _repository.GetById(id) ?? throw new Exception("This object is not existed!");
        }
        public IEnumerable<InteriorItem?> GetByCategory(int id, int? itemCategoryId, InteriorItemStatus? status, string? codeOrName, InteriorItemType? cateType)
        {
            var list = _repository.GetByCategory(id) ?? throw new Exception("This object is not existed!");

            return Filter(list, itemCategoryId, status, codeOrName, cateType);
        }
        public async Task<InteriorItem?> CreateInteriorItem([FromForm] InteriorItemRequest request)
        {
            FirebaseService s = new FirebaseService();
            string link = await s.UploadInteriorItemImage(request.Image);
            var ii = new InteriorItem
            {
                Id = Guid.NewGuid(),
                Code = request.Code,
                Name = request.Name,
                EnglishName = request.EnglishName,
                ImageUrl = link,
                Length = request.Length,
                Width = request.Width,
                Height = request.Height,
                CalculationUnit = request.CalculationUnit,
                Material = request.Material,
                Description = request.Description,
                EnglishDescription = request.EnglishDescription,
                Origin = request.Origin,
                EstimatePrice = request.EstimatePrice,
                InteriorItemColorId = request.InteriorItemColorId,
                InteriorItemCategoryId = request.InteriorItemCategoryId,
                Status = request.Status,
                ParentItemId = request.ParentItemId,
                IsDeleted = false,
                CreatedDate = DateTime.Now,
            };

            var iiCreated = _repository.Save(ii);
            return iiCreated;
        }
        public async void UpdateInteriorItem(Guid id, [FromForm] InteriorItemRequest request)
        {
            var ii = _repository.GetById(id) ?? throw new Exception("This object is not existed!");
            FirebaseService s = new FirebaseService();
            string link = await s.UploadInteriorItemImage(request.Image);
            ii.Code = request.Code;
            ii.Name = request.Name;
            ii.EnglishName = request.EnglishName;
            ii.ImageUrl = link;
            ii.Length = request.Length;
            ii.Width = request.Width;
            ii.Height = request.Height;
            ii.CalculationUnit = request.CalculationUnit;
            ii.Material = request.Material;
            ii.Description = request.Description;
            ii.EnglishDescription = request.EnglishDescription;
            ii.Origin = request.Origin;
            ii.EstimatePrice = request.EstimatePrice;
            ii.InteriorItemColorId = request.InteriorItemColorId;
            ii.InteriorItemCategoryId = request.InteriorItemCategoryId;
            ii.Status = request.Status;
            ii.ParentItemId = request.ParentItemId;

            _repository.Update(ii);
        }

        public void UpdateInteriorItemStatus(Guid id, InteriorItemStatus status)
        {
            var ii = _repository.GetById(id) ?? throw new Exception("This object is not existed!");

            ii.Status = status;

            _repository.Update(ii);
        }

        public void DeleteInteriorItem(Guid id)
        {
            var ii = _repository.GetById(id) ?? throw new Exception("This object is not existed!");

            ii.IsDeleted = true;

            _repository.Update(ii);
        }
    }
}
