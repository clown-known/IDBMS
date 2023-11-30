using IDBMS_API.DTOs.Request;
using IDBMS_API.DTOs.Request.BookingRequest;
using BusinessObject.Models;
using Repository.Interfaces;
using System;
using System.Collections.Generic;

namespace IDBMS_API.Services
{
    public class FloorService
    {
        private readonly IFloorRepository _floorRepo;
        private readonly IRoomRepository _roomRepo;
        private readonly IProjectTaskRepository _taskRepo;

        public FloorService(
                IFloorRepository floorRepo,
                IRoomRepository roomRepo,
                IProjectTaskRepository taskRepo)
        {
            _floorRepo = floorRepo;
            _roomRepo = roomRepo;
            _taskRepo = taskRepo;
        }

        public IEnumerable<Floor> GetAll()
        {
            return _floorRepo.GetAll();
        }

        public Floor? GetById(Guid id)
        {
            return _floorRepo.GetById(id) ?? throw new Exception("This object is not found!");
        }

        public IEnumerable<Floor?> GetByProjectId(Guid id)
        {
            return _floorRepo.GetByProjectId(id);
        }

        public Floor? CreateFloor(FloorRequest request)
        {
            var floor = new Floor
            {
                Id = Guid.NewGuid(),
                Description = request.Description,
                FloorNo = request.FloorNo,
                UsePurpose = request.UsePurpose,
                IsDeleted = false,
            };

            var floorCreated = _floorRepo.Save(floor);
            return floorCreated;
        }
        public void CreateBookFloor(Guid projectId, Guid siteId, List<BookingFloorRequest> request)
        {
            foreach (var floorRequest in request)
            {
                var floor = new Floor
                {
                    Id = Guid.NewGuid(),
                    Description = floorRequest.Description,
                    FloorNo = floorRequest.FloorNo,
                    UsePurpose = floorRequest.UsePurpose,
                    IsDeleted = false,
                };

                var floorCreated = _floorRepo.Save(floor);

                if (floorCreated != null)
                {
                    if (floorRequest.Rooms != null)
                    {
                        RoomService roomService = new RoomService(_roomRepo, _taskRepo);
                        roomService.CreateBookRoom(projectId, floorCreated.Id, floorRequest.Rooms);
                    }
                }
            }
        }

        public void UpdateFloor(Guid id, FloorRequest request)
        {
            var floor = _floorRepo.GetById(id) ?? throw new Exception("This object is not found!");

            floor.Description = request.Description;
            floor.FloorNo = request.FloorNo;
            floor.UsePurpose = request.UsePurpose;

            _floorRepo.Update(floor);
        }

        public void UpdateFloorStatus(Guid id, bool isDeleted)
        {
            var floor = _floorRepo.GetById(id) ?? throw new Exception("This object is not found!");

            floor.IsDeleted = isDeleted;

            _floorRepo.Update(floor);
        }

        public void DeleteFloor(Guid id)
        {
            var floor = _floorRepo.GetById(id) ?? throw new Exception("This object is not found!");
            _floorRepo.DeleteById(id);
        }
    }
}
