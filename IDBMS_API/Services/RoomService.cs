using BusinessObject.DTOs.Request;
using BusinessObject.DTOs.Request.BookingRequest;
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
        private readonly IProjectTaskRepository _taskRepo;

        public RoomService(IRoomRepository roomRepo, IProjectTaskRepository taskRepo)
        {
            _roomRepo = roomRepo;
            _taskRepo = taskRepo;
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
                PricePerArea = request.PricePerArea,
                RoomTypeId = request.RoomTypeId,
                IsHidden = false,
            };

            var roomCreated = _roomRepo.Save(room);
            return roomCreated;
        }

        public void CreateBookRoom(Guid projectId, Guid floorId, List<BookingRoomRequest> request)
        {
            foreach (var roomRequest in request)
            {
                var room = new Room
                {
                    Id = Guid.NewGuid(),
                    FloorId = floorId,
                    Description = roomRequest.Description,
                    UsePurpose = roomRequest.UsePurpose,
                    Area = roomRequest.Area,
                    PricePerArea = roomRequest.PricePerArea,
                    RoomTypeId = roomRequest.RoomTypeId,
                    IsHidden = false,
                };

                var roomCreated = _roomRepo.Save(room);

                if (roomCreated != null)
                {
                    if (roomRequest.Tasks != null)
                    {
                        ProjectTaskService taskService = new ProjectTaskService(_taskRepo);
                        taskService.CreateBookProjectTask(projectId, roomCreated.Id, roomRequest.Tasks);
                    }
                }
            }
        }

        public void UpdateRoom(Guid id, RoomRequest request)
        {
            var room = _roomRepo.GetById(id) ?? throw new Exception("This object is not found!");

            room.FloorId = request.FloorId;
            room.Description = request.Description;
            room.UsePurpose = request.UsePurpose;
            room.Area = request.Area;
            room.PricePerArea = request.PricePerArea;
            room.RoomTypeId = request.RoomTypeId;

            _roomRepo.Update(room);
        }

        public void UpdateRoomStatus(Guid id, bool isHidden)
        {
            var room = _roomRepo.GetById(id) ?? throw new Exception("This object is not found!");

            room.IsHidden = isHidden;

            _roomRepo.Update(room);
        }

        public void DeleteRoom(Guid id)
        {
            var room = _roomRepo.GetById(id) ?? throw new Exception("This object is not found!");

            _roomRepo.DeleteById(id);
        }
    }
}
