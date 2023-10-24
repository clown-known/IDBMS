using BusinessObject.DTOs.Request.CreateRequests;
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
        public void UpdateRoomType(RoomTypeRequest roomType, int id)
        {
            var rtCheck = roomTypeRepository.GetById(id) ?? throw new Exception("This Room Type not existed");
            var rt = new RoomType
            {
                Name = roomType.Name,
                ImageUrl = roomType.ImageUrl,
                Description = roomType.Description,
                PricePerArea = roomType.PricePerArea,
                IsHidden = roomType.IsHidden,
                IconImageUrl = roomType.IconImageUrl,
            };
            roomTypeRepository.Update(rt);
        }
    }
}
