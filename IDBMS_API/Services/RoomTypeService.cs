using IDBMS_API.DTOs.Request;
using BusinessObject.Models;
using Repository.Interfaces;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace IDBMS_API.Services
{
    public class RoomTypeService
    {
        private readonly IRoomTypeRepository _repository;
        public RoomTypeService(IRoomTypeRepository _repository)
        {
            this._repository = _repository;
        }   
        public RoomType? GetById(int id)
        {
            return _repository.GetById(id) ?? throw new Exception("This object is not existed!");
        }
        public IEnumerable<RoomType> GetAll()
        {
            return _repository.GetAll();
        }
        public async Task<RoomType?> CreateRoomType([FromForm] RoomTypeRequest roomType)
        {
            FirebaseService s = new FirebaseService();
            string link = await s.UploadImage(roomType.IconImage);
            string link2 = await s.UploadImage(roomType.Image);
            var rt = new RoomType
            {
                Name = roomType.Name,
                EnglishName = roomType.EnglishName,
                ImageUrl = link2,
                Description = roomType.Description,
                EnglishDescription = roomType.EnglishDescription,
                PricePerArea = roomType.PricePerArea,
                IsHidden = false,
                IconImageUrl = link,
            };

            var roomTypeCreated = _repository.Save(rt);
            return roomTypeCreated;
        }
        public async void UpdateRoomType(int id, [FromForm] RoomTypeRequest roomType)
        {
            var rt = _repository.GetById(id) ?? throw new Exception("This object is not existed!");
            FirebaseService s = new FirebaseService();
            string link = await s.UploadImage(roomType.IconImage);
            string link2 = await s.UploadImage(roomType.Image);
            rt.Name = roomType.Name;
            rt.EnglishName = roomType.EnglishName;
            rt.ImageUrl = link2;
            rt.Description = roomType.Description;
            rt.EnglishDescription = roomType.EnglishDescription;
            rt.PricePerArea = roomType.PricePerArea;
            rt.IconImageUrl = link;

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
