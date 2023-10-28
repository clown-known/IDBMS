using BusinessObject.DTOs.Request;
using BusinessObject.Models;
using Repository.Interfaces;

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
        public RoomType? CreateRoomType(RoomTypeRequest roomType)
        {
            var rt = new RoomType
            {
                Name = roomType.Name,
                ImageUrl = roomType.ImageUrl,
                Description = roomType.Description,
                PricePerArea = roomType.PricePerArea,
                IsHidden = false,
                IconImageUrl = roomType.IconImageUrl,
            };

            var roomTypeCreated = _repository.Save(rt);
            return roomTypeCreated;
        }
        public void UpdateRoomType(int id, RoomTypeRequest roomType)
        {
            var rt = _repository.GetById(id) ?? throw new Exception("This object is not existed!");

            rt.Name = roomType.Name;
            rt.ImageUrl = roomType.ImageUrl;
            rt.Description = roomType.Description;
            rt.PricePerArea = roomType.PricePerArea;
            rt.IconImageUrl = roomType.IconImageUrl;

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
