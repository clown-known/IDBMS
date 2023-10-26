using BusinessObject.DTOs.Request;
using BusinessObject.Models;
using Repository.Interfaces;

namespace IDBMS_API.Services
{
    public class RoomService
    {
        private readonly IRoomRepository roomRepository;
        public RoomService(IRoomRepository roomRepository)
        {
            this.roomRepository = roomRepository;
        }
        public IEnumerable<Room> GetAll()
        {
            return roomRepository.GetAll();
        }
        public Room? GetById(Guid id)
        {
            return roomRepository.GetById(id);
        }
        public Room? CreateRoom(RoomRequest request)
        {
            var room = new Room
            {
                FloorId = request.FloorId,
                Description = request.Description,
                RoomNo = request.RoomNo,
                UsePurpose = request.UsePurpose,
                Area = request.Area,
                PricePerArea = request.PricePerArea,
                RoomTypeId = request.RoomTypeId,
            };
            var roomCreated = roomRepository.Save(room);
            return roomCreated;
        }
        public void UpdateRoom(RoomRequest request)
        {
            var room = new Room
            {
                FloorId = request.FloorId,
                Description = request.Description,
                RoomNo = request.RoomNo,
                UsePurpose = request.UsePurpose,
                Area = request.Area,
                PricePerArea = request.PricePerArea,
                RoomTypeId = request.RoomTypeId,
            };
            roomRepository.Update(room);
        }
        public void DeleteRoom(Guid id)
        {
            roomRepository.DeleteById(id);
        }
    }
}
