using IDBMS_API.DTOs.Request;
using BusinessObject.Models;
using IDBMS_API.Constants;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using BusinessObject.Enums;
using DocumentFormat.OpenXml.Wordprocessing;
using Azure.Core;
using UnidecodeSharpFork;
using DocumentFormat.OpenXml.Office2016.Excel;

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
        private readonly IFloorRepository _floorRepo;
        private readonly ITransactionRepository _transactionRepo;
        private readonly ITaskDesignRepository _taskDesignRepo;
        private readonly ITaskCategoryRepository _taskCategoryRepo;

        public RoomService(
            IRoomRepository roomRepo,
            IRoomTypeRepository roomTypeRepo,
            IProjectRepository projectRepo,
            IProjectTaskRepository projectTaskRepo,
            IPaymentStageRepository stageRepo,
            IProjectDesignRepository projectDesignRepo,
            IPaymentStageDesignRepository stageDesignRepo,
            IFloorRepository floorRepo,
            ITransactionRepository transactionRepo, 
            ITaskDesignRepository taskDesignRepo, 
            ITaskCategoryRepository taskCategoryRepo)
        {
            _roomRepo = roomRepo;
            _roomTypeRepo = roomTypeRepo;
            _projectRepo = projectRepo;
            _projectTaskRepo = projectTaskRepo;
            _stageRepo = stageRepo;
            _projectDesignRepo = projectDesignRepo;
            _stageDesignRepo = stageDesignRepo;
            _floorRepo = floorRepo;
            _transactionRepo = transactionRepo;
            _taskDesignRepo = taskDesignRepo;
            _taskCategoryRepo = taskCategoryRepo;
        }

        private IEnumerable<Room> Filter(IEnumerable<Room> list,
            string? usePurpose, bool? isHidden)
        {
            IEnumerable<Room> filteredList = list;

            if (usePurpose != null)
            {
                filteredList = filteredList.Where(item => item.UsePurpose.Unidecode().IndexOf(usePurpose.Unidecode(), StringComparison.OrdinalIgnoreCase) >= 0);
            }

            if (isHidden != null)
            {
                filteredList = filteredList.Where(item => item.IsHidden == isHidden);
            }

            return filteredList;
        }

        public IEnumerable<Room> GetAll(string? usePurpose, bool? isHidden)
        {
            var list = _roomRepo.GetAll();

            return Filter(list, usePurpose, isHidden);
        }

        public Room? GetById(Guid id)
        {
            return _roomRepo.GetById(id) ?? throw new Exception("This room id is not found!");
        }

        public IEnumerable<Room> GetByFloorId(Guid id, string? usePurpose, bool? isHidden)
        {
            var list = _roomRepo.GetByFloorId(id) ?? throw new Exception("This room id is not found!");

            return Filter(list, usePurpose, isHidden);
        }
        
        public IEnumerable<Room> GetByProjectId(Guid id, string? usePurpose, bool? isHidden)
        {
            var list = _roomRepo.GetByProjectId(id);

            return Filter(list, usePurpose, isHidden);
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

            ProjectService projectService = new(_projectRepo, _roomRepo, _roomTypeRepo, _projectTaskRepo, _stageRepo, _projectDesignRepo, _stageDesignRepo, _floorRepo, _transactionRepo, _taskDesignRepo, _taskCategoryRepo);
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
                room.RoomTypeId = request.RoomTypeId;

                roomCreated = _roomRepo.Save(room);

                ProjectTaskService taskService = new (_projectTaskRepo, _projectRepo, _stageRepo, _projectDesignRepo, _stageDesignRepo, _floorRepo, _roomRepo, _roomTypeRepo, _transactionRepo, _taskCategoryRepo, _taskDesignRepo);
                var task = new ProjectTaskRequest
                {
                    CalculationUnit = BusinessObject.Enums.CalculationUnit.Meter,
                    PricePerUnit = roomType.PricePerArea,
                    UnitInContract = request.Area,
                    IsIncurred = false,
                    ProjectId = request.ProjectId,
                    RoomId = roomCreated.Id,
                    Status = ProjectTaskStatus.Pending,
                    EstimateBusinessDay = (int)Math.Ceiling(roomType.EstimateDayPerArea * roomCreated.Area),
                };

                if (request.Language == Language.English)
                {
                    task.Name = "Decor design for " + request.UsePurpose;
                }
                else
                {
                    task.Name = "Thiết kế cho " + request.UsePurpose;
                }

                taskService.CreateTasksDecor(task);
            }
            else
            {
                roomCreated = _roomRepo.Save(room);
            }

            UpdateProjectArea(request.ProjectId);

            return roomCreated;
        }

        public void DuplicateRoomsByFloorId(Guid createdId, Guid basedOnId, Guid projectId)
        {
            var rooms = _roomRepo.GetByFloorId(basedOnId).Where(r=> r.IsHidden == false);

            foreach (var room in rooms)
            {
                var roomRequest = new RoomRequest
                {
                    Area= room.Area,
                    Description= room.Description,
                    FloorId= createdId,
                    IsHidden= room.IsHidden,
                    UsePurpose = room.UsePurpose,
                    ProjectId= projectId
                };

                CreateRoom(roomRequest);
            }
        }

        public void UpdateRoom(Guid id, RoomRequest request)
        {
            var room = _roomRepo.GetById(id) ?? throw new Exception("This room id is not found!");

            room.FloorId = request.FloorId;
            room.Description = request.Description;
            room.UsePurpose = request.UsePurpose;
            room.Area = request.Area;

            if (request.RoomTypeId != null && request.RoomTypeId != room.RoomTypeId)
            {
                room.RoomTypeId = request.RoomTypeId;

                RoomTypeService rtService = new (_roomTypeRepo);
                var roomType = rtService.GetById(request.RoomTypeId.Value);
                room.PricePerArea = roomType.PricePerArea;

                var estimateBusinessDay = (int)Math.Ceiling(roomType.EstimateDayPerArea * room.Area);

                ProjectTaskService taskService = new(_projectTaskRepo, _projectRepo, _stageRepo, _projectDesignRepo, _stageDesignRepo, _floorRepo, _roomRepo, _roomTypeRepo, _transactionRepo, _taskCategoryRepo, _taskDesignRepo);
                taskService.UpdateDecorTask(id, room.PricePerArea.Value, estimateBusinessDay);
            }

            _roomRepo.Update(room);

            UpdateProjectArea(request.ProjectId);
        }

        public void UpdateRoomStatus(Guid id, bool isHidden, Guid projectId)
        {
            var room = _roomRepo.GetById(id) ?? throw new Exception("This room id is not found!");

            room.IsHidden = isHidden;

            _roomRepo.Update(room);

            UpdateProjectArea(projectId);
        }

    }
}
