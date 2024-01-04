using IDBMS_API.DTOs.Request;
using BusinessObject.Models;
using Repository.Implements;
using Repository.Interfaces;
using BusinessObject.Enums;
using UnidecodeSharpFork;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace IDBMS_API.Services
{
    public class InteriorItemColorService
    {
        private readonly IInteriorItemColorRepository _colorRepo;
        private readonly IInteriorItemRepository _itemRepo;
        private readonly IInteriorItemCategoryRepository _itemCategoryRepo;
        public InteriorItemColorService(IInteriorItemColorRepository colorRepo, IInteriorItemRepository itemRepo, IInteriorItemCategoryRepository itemCategoryRepo)
        {
            _colorRepo = colorRepo;
            _itemRepo = itemRepo;
            _itemCategoryRepo = itemCategoryRepo;
        }

        private IEnumerable<InteriorItemColor> Filter(IEnumerable<InteriorItemColor> list,
            ColorType? type, string? name)
        {
            IEnumerable<InteriorItemColor> filteredList = list;

            if (type != null)
            {
                filteredList = filteredList.Where(item => item.Type == type);
            }

            if (name != null)
            {
                filteredList = filteredList.Where(item => item.Name == name);
            }

            return filteredList;
        }

        public IEnumerable<InteriorItemColor> GetAll(ColorType? type, string? name)
        {
            var list = _colorRepo.GetAll();

            return Filter(list, type, name);
        }
        public InteriorItemColor? GetById(int id)
        {
            return _colorRepo.GetById(id) ?? throw new Exception("This item color id is not existed!");
        }
        public IEnumerable<InteriorItemColor> GetByCategoryId(int id, ColorType? type, string? name)
        {
            var list = _colorRepo.GetByCategoryId(id) ?? throw new Exception("This item color id is not existed!");

            return Filter(list, type, name);
        }
        public async Task<InteriorItemColor?> CreateInteriorItemColor(InteriorItemColorRequest request)
        {
            var iic = new InteriorItemColor
            {
                Name = request.Name,
                EnglishName = request.EnglishName,
                Type = request.Type,
            };

            FirebaseService s = new FirebaseService();

            if (request.PrimaryColorFile != null)
            {
                string primaryColorUrl = await s.UploadImage(request.PrimaryColorFile);
                iic.PrimaryColor = primaryColorUrl;
            }

            if (request.SecondaryColorFile != null)
            {
                string secondaryColorUrl = await s.UploadImage(request.SecondaryColorFile);
                iic.SecondaryColor = secondaryColorUrl;
            }

            var iicCreated = _colorRepo.Save(iic);
            return iicCreated;
        }
        public async Task UpdateInteriorItemColor(int id, InteriorItemColorRequest request)
        {
            var iic = _colorRepo.GetById(id) ?? throw new Exception("This item color id is not existed!");

            iic.Name = request.Name;
            iic.EnglishName = request.EnglishName;
            iic.Type = request.Type;

            FirebaseService s = new FirebaseService();

            if (request.PrimaryColorFile != null)
            {
                string primaryColorUrl = await s.UploadImage(request.PrimaryColorFile);
                iic.PrimaryColor = primaryColorUrl;
            }

            if (request.SecondaryColorFile != null)
            {
                string secondaryColorUrl = await s.UploadImage(request.SecondaryColorFile);
                iic.SecondaryColor = secondaryColorUrl;
            }

            _colorRepo.Update(iic);
        }
        public void DeleteInteriorItemColor(int id)
        {
            var iic = _colorRepo.GetById(id) ?? throw new Exception("This item color id is not existed!");

            InteriorItemService itemService = new InteriorItemService(_itemRepo, _itemCategoryRepo);
            itemService.DeleteItemColor(id);

            _colorRepo.DeleteById(id);
        }
    }
}
