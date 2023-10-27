using BusinessObject.DTOs.Request;
using BusinessObject.Models;
using Repository.Interfaces;

namespace IDBMS_API.Services
{
    public class RoomService
    {
        private readonly IRoomRepository _repository;
        public RoomService(IRoomRepository repository)
        {
            _repository = repository;
        }
        public IEnumerable<Room> GetAll()
        {
            return _repository.GetAll();
        }
        public Room? GetById(Guid id)
        {
            return _repository.GetById(id);
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
            var roomCreated = _repository.Save(room);
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
            _repository.Update(room);
        }
        public void DeleteRoom(Guid id)
        {
            _repository.DeleteById(id);
        }
    }
}
