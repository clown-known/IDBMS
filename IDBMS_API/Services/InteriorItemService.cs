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
        private readonly IInteriorItemRepository _itemRepo;
        private readonly IInteriorItemCategoryRepository _itemCategoryRepo;
        public InteriorItemService(IInteriorItemRepository itemRepo, IInteriorItemCategoryRepository itemCategoryRepo)
        {
            _itemRepo = itemRepo;
            _itemCategoryRepo = itemCategoryRepo;
        }

        private IEnumerable<InteriorItem> Filter(IEnumerable<InteriorItem> list,
            int? itemCategoryId, InteriorItemStatus? status, string? codeOrName, InteriorItemType? itemType)
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
                           (item.Code != null && item.Code.Unidecode().IndexOf(codeOrName.Unidecode(), StringComparison.OrdinalIgnoreCase) >= 0) ||
                           (item.Name != null && item.Name.Unidecode().IndexOf(codeOrName.Unidecode(), StringComparison.OrdinalIgnoreCase) >= 0));
            }

            if (itemType != null)
            {
                filteredList = filteredList.Where(item => item.InteriorItemCategory.InteriorItemType == itemType);
            }

            return filteredList;
        }
        public bool CheckCodeExisted(string code)
        {
            return _itemRepo.CheckCodeExisted(code);
        }

        public string GenerateCode(int? categoryId)
        {
            string code = String.Empty;
            Random random = new ();

            for (int attempt = 0; attempt < 10; attempt++)
            {
                // Generate the code
                code = GenerateSingleCode(categoryId, random);


                bool codeExists = CheckCodeExisted(code);

                if (codeExists == false)
                {
                    return code;
                }
            }

            throw new Exception("Failed to generate a unique code after 10 attempts.");
        }

        public string GenerateSingleCode(int? categoryId, Random random)
        {
            string code = String.Empty;

            if (categoryId == null)
            {
                code += "KPL_KPL_";
            }
            else
            {
                InteriorItemCategoryService itemCategoryService = new(_itemCategoryRepo);
                var category = itemCategoryService.GetById(categoryId.Value) ?? throw new Exception("This item category id is not existed!");
                var type = category.InteriorItemType;

                if (type == InteriorItemType.Furniture)
                {
                    code += "NT_";
                }
                if (type == InteriorItemType.Material)
                {
                    code += "VL_";
                }
                if (type == InteriorItemType.CustomFurniture)
                {
                    code += "NB_";
                }

                var valid = category.Name.Contains(' ');
                if (valid)
                {
                    category.Name.Split(' ').ToList().ForEach(i => code += i[0].ToString().Unidecode().ToUpper());
                    code += "_";
                }
                else
                {
                    code += category.Name.Substring(0, 2).Unidecode().ToUpper() + "_";
                }
            }
            
            code += random.Next(100000, 999999);

            return code;
        }

        public IEnumerable<InteriorItem> GetAll(int? itemCategoryId, InteriorItemStatus? status, string? codeOrName, InteriorItemType? itemType)
        {
            var list = _itemRepo.GetAll();

            return Filter(list, itemCategoryId, status, codeOrName, itemType);
        }
        public InteriorItem? GetById(Guid id)
        {
            return _itemRepo.GetById(id) ?? throw new Exception("This item id is not existed!");
        }
        public IEnumerable<InteriorItem> GetByCategory(int id, int? itemCategoryId, InteriorItemStatus? status, string? codeOrName, InteriorItemType? itemType)
        {
            var list = _itemRepo.GetByCategory(id) ?? throw new Exception("This item id is not existed!");

            return Filter(list, itemCategoryId, status, codeOrName, itemType);
        }
        public async Task<InteriorItem?> CreateInteriorItem(InteriorItemRequest request)
        {
            var generateCode = GenerateCode(request.InteriorItemCategoryId);
            var ii = new InteriorItem
            {
                Id = Guid.NewGuid(),
                Code = generateCode,
                Name = request.Name,
                EnglishName = request.EnglishName,
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

            if (request.InteriorItemCategoryId == null)
            {
                ii.InteriorItemCategoryId = 12;
            }

            if (request.Image != null)
            {
                FirebaseService s = new FirebaseService();
                string link = await s.UploadInteriorItemImage(request.Image);

                ii.ImageUrl = link;
            }

            var iiCreated = _itemRepo.Save(ii);
            return iiCreated;
        }
        public async void UpdateInteriorItem(Guid id, InteriorItemRequest request)
        {
            var ii = _itemRepo.GetById(id) ?? throw new Exception("This item id is not existed!");

            ii.Name = request.Name;
            ii.EnglishName = request.EnglishName;
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

            if (request.Image != null)
            {
                FirebaseService s = new FirebaseService();
                string link = await s.UploadInteriorItemImage(request.Image);

                ii.ImageUrl = link;
            }

            _itemRepo.Update(ii);
        }

        public void UpdateInteriorItemStatus(Guid id, InteriorItemStatus status)
        {
            var ii = _itemRepo.GetById(id) ?? throw new Exception("This item id is not existed!");

            ii.Status = status;

            _itemRepo.Update(ii);
        }

        public void DeleteInteriorItem(Guid id)
        {
            var ii = _itemRepo.GetById(id) ?? throw new Exception("This item id is not existed!");

            ii.IsDeleted = true;

            _itemRepo.Update(ii);
        }
    }
}
