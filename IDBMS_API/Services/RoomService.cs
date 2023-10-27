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
<<<<<<< HEAD
=======

>>>>>>> a1244e10b9284a0873e7e9033552e66e43c15959
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
<<<<<<< HEAD
            return _repository.GetById(id);
=======
            return _repository.GetById(id) ?? throw new Exception("This object is not found!");
>>>>>>> a1244e10b9284a0873e7e9033552e66e43c15959
        }

        public IEnumerable<Room> GetByProjectId(Guid projectId)
        {
            return _repository.GetByProjectId(projectId);
        }

        public IEnumerable<Room> GetByFloorId(Guid floorId)
        {
            return _repository.GetByFloorId(floorId);
        }

        public Room? CreateRoom(RoomRequest request)
        {
            var room = new Room
            {
                Id = Guid.NewGuid(),
                FloorId = request.FloorId,
                Description = request.Description,
                RoomNo = request.RoomNo,
                UsePurpose = request.UsePurpose,
                Area = request.Area,
                PricePerArea = request.PricePerArea,
                RoomTypeId = request.RoomTypeId,
                ProjectId = request.ProjectId,
                IsHidden = false,
            };
            var roomCreated = _repository.Save(room);
            return roomCreated;
        }

        public void UpdateRoom(Guid id, RoomRequest request)
        {
<<<<<<< HEAD
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
=======
            var room = _repository.GetById(id) ?? throw new Exception("This object is not found!");

            room.FloorId = request.FloorId;
            room.Description = request.Description;
            room.RoomNo = request.RoomNo;
            room.UsePurpose = request.UsePurpose;
            room.Area = request.Area;
            room.PricePerArea = request.PricePerArea;
            room.RoomTypeId = request.RoomTypeId;
            room.ProjectId = request.ProjectId;

>>>>>>> a1244e10b9284a0873e7e9033552e66e43c15959
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
<<<<<<< HEAD
=======
            var room = _repository.GetById(id) ?? throw new Exception("This object is not found!");

>>>>>>> a1244e10b9284a0873e7e9033552e66e43c15959
            _repository.DeleteById(id);
        }
    }
}
