using IDBMS_API.DTOs.Request;
using BusinessObject.Models;
using Repository.Interfaces;
using BLL.Services;

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
        public async Task<RoomType?> CreateRoomType(RoomTypeRequest roomType)
        {
            FirebaseService s = new FirebaseService();
            string link = await s.UploadImage(roomType.IconImage);
            var rt = new RoomType
            {
                Name = roomType.Name,
                EnglishName = roomType.EnglishName,
                ImageUrl = roomType.ImageUrl,
                Description = roomType.Description,
                EnglishDescription = roomType.EnglishDescription,
                PricePerArea = roomType.PricePerArea,
                IsHidden = false,
                IconImageUrl = link,
            };

            var roomTypeCreated = _repository.Save(rt);
            return roomTypeCreated;
        }
        public async void UpdateRoomType(int id, RoomTypeRequest roomType)
        {
            var rt = _repository.GetById(id) ?? throw new Exception("This object is not existed!");
            FirebaseService s = new FirebaseService();
            string link = await s.UploadImage(roomType.IconImage);
            rt.Name = roomType.Name;
            rt.EnglishName = roomType.EnglishName;
            rt.ImageUrl = roomType.ImageUrl;
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
