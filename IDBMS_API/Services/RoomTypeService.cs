using IDBMS_API.DTOs.Request;
using BusinessObject.Models;
using Repository.Interfaces;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;
using BusinessObject.Enums;
using UnidecodeSharpFork;

namespace IDBMS_API.Services
{
    public class RoomTypeService
    {
        private readonly IRoomTypeRepository _repository;
        public RoomTypeService(IRoomTypeRepository _repository)
        {
            this._repository = _repository;
        }

        public IEnumerable<RoomType> Filter(IEnumerable<RoomType> list,
            bool? isHidden, string? name)
        {
            IEnumerable<RoomType> filteredList = list;

            if (isHidden != null)
            {
                filteredList = filteredList.Where(item => item.IsHidden == isHidden);
            }

            if (name != null)
            {
                filteredList = filteredList.Where(item => (item.Name != null && item.Name.Unidecode().IndexOf(name.Unidecode(), StringComparison.OrdinalIgnoreCase) >= 0));
            }

            return filteredList;
        }

        public RoomType? GetById(int id)
        {
            return _repository.GetById(id) ?? throw new Exception("This object is not existed!");
        }
        public IEnumerable<RoomType> GetAll(bool? isHidden, string? name)
        {
            var list = _repository.GetAll();

            return Filter(list, isHidden, name);
        }
        public async Task<RoomType?> CreateRoomType([FromForm] RoomTypeRequest roomType)
        {
            var rt = new RoomType
            {
                Name = roomType.Name,
                EnglishName = roomType.EnglishName,
                Description = roomType.Description,
                EnglishDescription = roomType.EnglishDescription,
                PricePerArea = roomType.PricePerArea,
                IsHidden = false,
            };

            if (roomType.Image != null)
            {
                FirebaseService s = new FirebaseService();
                string link = await s.UploadImage(roomType.Image);

                rt.ImageUrl = link;
            }

            if (roomType.IconImage != null)
            {
                FirebaseService s = new FirebaseService();
                string link = await s.UploadImage(roomType.IconImage);

                rt.IconImageUrl = link;
            }

            var roomTypeCreated = _repository.Save(rt);
            return roomTypeCreated;
        }
        public async void UpdateRoomType(int id, [FromForm] RoomTypeRequest roomType)
        {
            var rt = _repository.GetById(id) ?? throw new Exception("This object is not existed!");

            if (roomType.IconImage!= null)
            {
                FirebaseService s = new FirebaseService();
                string link = await s.UploadImage(roomType.IconImage);

                rt.IconImageUrl = link;
            }

            if (roomType.Image != null)
            {
                FirebaseService s = new FirebaseService();
                string link = await s.UploadImage(roomType.Image);

                rt.ImageUrl = link;
            }

            rt.Name = roomType.Name;
            rt.EnglishName = roomType.EnglishName;
            rt.Description = roomType.Description;
            rt.EnglishDescription = roomType.EnglishDescription;
            rt.PricePerArea = roomType.PricePerArea;

            _repository.Update(rt);
        }
        public void UpdateRoomTypeStatus(int id, bool isHidden)
        {
            var rt = _repository.GetById(id) ?? throw new Exception("This object is not existed!");

            rt.IsHidden = isHidden;

            _repository.Update(rt);
        }
    }
}
