using IDBMS_API.DTOs.Request;
using BusinessObject.Models;
using IDBMS_API.Constants;
using Repository.Interfaces;
using System;
using System.Collections.Generic;

namespace IDBMS_API.Services
{
    public class RoomService
    {
        private readonly IRoomRepository _roomRepo;
        private readonly IRoomTypeRepository _roomTypeRepo;

        public RoomService(IRoomRepository roomRepo, IRoomTypeRepository roomTypeRepo)
        {
            _roomRepo = roomRepo;
            _roomTypeRepo = roomTypeRepo;
        }

        public IEnumerable<Room> GetAll()
        {
            return _roomRepo.GetAll();
        }

        public Room? GetById(Guid id)
        {
            return _roomRepo.GetById(id) ?? throw new Exception("This object is not found!");
        }

        public IEnumerable<Room> GetByFloorId(Guid id)
        {
            return _roomRepo.GetByFloorId(id);
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
                RoomTypeId = request.RoomTypeId,
                IsHidden = false,
            };

            RoomTypeService rtService = new RoomTypeService(_roomTypeRepo);
            var roomType = rtService.GetById(request.RoomTypeId);

            if (roomType != null)
            {
                room.PricePerArea = roomType.PricePerArea;

            }

            var roomCreated = _roomRepo.Save(room);
            return roomCreated;
        }

        public void UpdateRoom(Guid id, RoomRequest request)
        {
            var room = _roomRepo.GetById(id) ?? throw new Exception("This object is not found!");

            room.FloorId = request.FloorId;
            room.Description = request.Description;
            room.UsePurpose = request.UsePurpose;
            room.Area = request.Area;
            room.RoomTypeId = request.RoomTypeId;

            RoomTypeService rtService = new RoomTypeService(_roomTypeRepo);
            var roomType = rtService.GetById(request.RoomTypeId);

            if (roomType != null)
            {
                room.PricePerArea = roomType.PricePerArea;

            }

            _roomRepo.Update(room);
        }

        public void UpdateRoomStatus(Guid id, bool isHidden)
        {
            var room = _roomRepo.GetById(id) ?? throw new Exception("This object is not found!");

            room.IsHidden = isHidden;

            _roomRepo.Update(room);
        }

    }
}
