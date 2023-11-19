using BusinessObject.DTOs.Request;
using BusinessObject.Models;
using IDBMS_API.Constants;
using Repository.Interfaces;
using System;
using System.Collections.Generic;

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
            return _repository.GetById(id) ?? throw new Exception("This object is not found!");
        }

       /* public IEnumerable<Room> GetByProjectId(Guid projectId)
        {
            return _repository.GetByProjectId(projectId);
        }*/

        public IEnumerable<Room> GetByFloorId(Guid id)
        {
            return _repository.GetByFloorId(id);
        }

        public Room? CreateRoom(RoomRequest request)
        {
            var room = new Room
            {
                Id = Guid.NewGuid(),
                FloorId = request.FloorId,
                Description = request.Description,
                UsePurpose = request.UsePurpose,
                Area = request.Area,
                PricePerArea = request.PricePerArea,
                RoomTypeId = request.RoomTypeId,
                IsHidden = false,
            };

            var roomCreated = _repository.Save(room);
            return roomCreated;
        }

        public void UpdateRoom(Guid id, RoomRequest request)
        {
            var room = _repository.GetById(id) ?? throw new Exception("This object is not found!");

            room.FloorId = request.FloorId;
            room.Description = request.Description;
            room.UsePurpose = request.UsePurpose;
            room.Area = request.Area;
            room.PricePerArea = request.PricePerArea;
            room.RoomTypeId = request.RoomTypeId;

            _repository.Update(room);
        }

        public void UpdateRoomStatus(Guid id, bool isHidden)
        {
            var room = _repository.GetById(id) ?? throw new Exception("This object is not found!");

            room.IsHidden = isHidden;

            _repository.Update(room);
        }

        public void DeleteRoom(Guid id)
        {
            var room = _repository.GetById(id) ?? throw new Exception("This object is not found!");

            _repository.DeleteById(id);
        }
    }
}
