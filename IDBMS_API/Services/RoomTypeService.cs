using BusinessObject.DTOs.Request;
using BusinessObject.Models;
using Repository.Interfaces;

namespace IDBMS_API.Services
{
    public class RoomTypeService
    {
        private readonly IRoomTypeRepository roomTypeRepository;
        public RoomTypeService(IRoomTypeRepository roomTypeRepository)
        {
            this.roomTypeRepository = roomTypeRepository;
        }   
        public RoomType? GetById(int id)
        {
            return roomTypeRepository.GetById(id);
        }
        public IEnumerable<RoomType> GetAll()
        {
            return roomTypeRepository.GetAll();
        }
        public RoomType? CreateRoomType(RoomTypeRequest roomType)
        {
            var rt = new RoomType
            {
                Name = roomType.Name,
                ImageUrl = roomType.ImageUrl,
                Description = roomType.Description,
                PricePerArea = roomType.PricePerArea,
                IsHidden = roomType.IsHidden,
                IconImageUrl = roomType.IconImageUrl,
            };
            var roomTypeCreated = roomTypeRepository.Save(rt);
            return roomTypeCreated;
        }
        public void UpdateRoomType(int id, RoomTypeRequest roomType)
        {
            var rt = roomTypeRepository.GetById(id) ?? throw new Exception("This Object not existed");
            rt.Name = roomType.Name;
            rt.ImageUrl = roomType.ImageUrl;
            rt.Description = roomType.Description;
            rt.PricePerArea = roomType.PricePerArea;
            rt.IsHidden = roomType.IsHidden;
            rt.IconImageUrl = roomType.IconImageUrl;

            roomTypeRepository.Update(rt);
        }
        public void UpdateRoomTypeStatus(int id, bool isHidden)
        {
            var rt = roomTypeRepository.GetById(id) ?? throw new Exception("This Object not existed");
            rt.IsHidden = isHidden;

            roomTypeRepository.Update(rt);
        }
    }
}
