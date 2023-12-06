using IDBMS_API.DTOs.Request;
using BusinessObject.Models;
using IDBMS_API.Constants;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using BusinessObject.Enums;
using DocumentFormat.OpenXml.Wordprocessing;
using Azure.Core;

namespace IDBMS_API.Services
{
    public class RoomService
    {
        private readonly IRoomRepository _roomRepo;
        private readonly IRoomTypeRepository _roomTypeRepo;
        private readonly IProjectRepository _projectRepo;
        private readonly IProjectTaskRepository _projectTaskRepo;
        private readonly IPaymentStageRepository _stageRepo;
        private readonly IProjectDesignRepository _projectDesignRepo;
        private readonly IPaymentStageDesignRepository _stageDesignRepo;

        public RoomService(
            IRoomRepository roomRepo,
            IRoomTypeRepository roomTypeRepo,
            IProjectRepository projectRepo,
            IProjectTaskRepository projectTaskRepo,
            IPaymentStageRepository stageRepo,
            IProjectDesignRepository projectDesignRepo,
            IPaymentStageDesignRepository stageDesignRepo)
        {
            _roomRepo = roomRepo;
            _roomTypeRepo = roomTypeRepo;
            _projectRepo = projectRepo;
            _projectTaskRepo = projectTaskRepo;
            _stageRepo = stageRepo;
            _projectDesignRepo = projectDesignRepo;
            _stageDesignRepo = stageDesignRepo;
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
        
        public IEnumerable<Room> GetByProjectId(Guid id)
        {
            return _roomRepo.GetByProjectId(id);
        }

        public void UpdateProjectArea(Guid projectId)
        {
            var roomsInProject = _roomRepo.GetByProjectId(projectId);

            double area = 0;

            if (roomsInProject != null && roomsInProject.Any())
            {
                area = roomsInProject.Sum(room =>
                {
                    if (room != null && room.IsHidden != true)
                    {
                        return room.Area;
                    }
                    return 0;
                });
            }

            ProjectService projectService = new(_projectRepo);
            projectService.UpdateProjectDataByRoom(projectId, area);

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
                IsHidden = false,
            };

            var roomCreated = new Room();

            if (request.RoomTypeId != null)
            {
                RoomTypeService rtService = new(_roomTypeRepo);
                var roomType = rtService.GetById((int)request.RoomTypeId);
                room.PricePerArea = roomType.PricePerArea;
                roomCreated = _roomRepo.Save(room);

                ProjectTaskService taskService = new ProjectTaskService(_projectTaskRepo, _projectRepo, _stageRepo, _projectDesignRepo, _stageDesignRepo);
                var task = new ProjectTaskRequest
                {
                    Percentage = 0,
                    CalculationUnit = BusinessObject.Enums.CalculationUnit.Meter,
                    PricePerUnit = roomType.PricePerArea,
                    UnitInContract = request.Area,
                    UnitUsed = 0,
                    IsIncurred = false,
                    ProjectId = request.ProjectId,
                    RoomId = roomCreated.Id,
                    Status = ProjectTaskStatus.Pending,
                    EstimateBusinessDay = (int)(roomType.EstimateDayPerArea * roomCreated.Area),
                };

                if (request.Language == Language.English)
                {
                    task.Name = "Decor design for " + request.UsePurpose;
                }
                else
                {
                    task.Name = "Thiết kế cho " + request.UsePurpose;
                }

                taskService.CreateProjectTask(task);
            }
            else
            {
                roomCreated = _roomRepo.Save(room);
            }

            UpdateProjectArea(request.ProjectId);

            return roomCreated;
        }

        public void UpdateRoom(Guid id, RoomRequest request)
        {
            var room = _roomRepo.GetById(id) ?? throw new Exception("This object is not found!");

            room.FloorId = request.FloorId;
            room.Description = request.Description;
            room.UsePurpose = request.UsePurpose;
            room.Area = request.Area;

            if (request.RoomTypeId != null)
            {
                room.RoomTypeId = request.RoomTypeId;

                RoomTypeService rtService = new RoomTypeService(_roomTypeRepo);
                var roomType = rtService.GetById((int)request.RoomTypeId);
                room.PricePerArea = roomType.PricePerArea;
            }

            _roomRepo.Update(room);

            UpdateProjectArea(request.ProjectId);
        }

        public void UpdateRoomStatus(Guid id, bool isHidden, Guid projectId)
        {
            var room = _roomRepo.GetById(id) ?? throw new Exception("This object is not found!");

            room.IsHidden = isHidden;

            _roomRepo.Update(room);

            UpdateProjectArea(projectId);
        }

    }
}
