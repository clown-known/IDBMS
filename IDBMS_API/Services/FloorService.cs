using IDBMS_API.DTOs.Request;
using BusinessObject.Models;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Wordprocessing;
using BusinessObject.Enums;
using UnidecodeSharpFork;

namespace IDBMS_API.Services
{
    public class FloorService
    {
        private readonly IFloorRepository _floorRepo;
        private readonly IRoomRepository _roomRepo;
        private readonly IRoomTypeRepository _roomTypeRepo;
        private readonly IProjectTaskRepository _projectTaskRepo;
        private readonly IPaymentStageRepository _stageRepo;
        private readonly IProjectDesignRepository _projectDesignRepo;
        private readonly IPaymentStageDesignRepository _stageDesignRepo;
        private readonly IProjectRepository _projectRepo;
        private readonly ITransactionRepository _transactionRepo;
        private readonly ITaskDesignRepository _taskDesignRepo;
        private readonly ITaskCategoryRepository _taskCategoryRepo;

        public FloorService(
            IProjectRepository projectRepo,
            IRoomRepository roomRepo,
            IRoomTypeRepository roomTypeRepo,
            IProjectTaskRepository projectTaskRepo,
            IPaymentStageRepository stageRepo,
            IProjectDesignRepository projectDesignRepo,
            IPaymentStageDesignRepository stageDesignRepo,
            IFloorRepository floorRepo,
            ITransactionRepository transactionRepo,
            ITaskDesignRepository taskDesignRepo,
            ITaskCategoryRepository taskCategoryRepo)
        {
            _projectRepo = projectRepo;
            _roomRepo = roomRepo;
            _roomTypeRepo = roomTypeRepo;
            _projectTaskRepo = projectTaskRepo;
            _stageRepo = stageRepo;
            _projectDesignRepo = projectDesignRepo;
            _stageDesignRepo = stageDesignRepo;
            _floorRepo = floorRepo;
            _transactionRepo = transactionRepo;
            _taskDesignRepo = taskDesignRepo;
            _taskCategoryRepo = taskCategoryRepo;
        }

        private IEnumerable<Floor> Filter(IEnumerable<Floor> list,
            int? noOfFloor, string? usePurpose)
        {
            IEnumerable<Floor> filteredList = list;

            if (noOfFloor != null)
            {
                filteredList = filteredList.Where(item => item.FloorNo == noOfFloor);
               
            }

            if (usePurpose != null)
            {
                filteredList = filteredList.Where(item =>
                           (item.UsePurpose != null && item.UsePurpose.Unidecode().IndexOf(usePurpose.Unidecode(), StringComparison.OrdinalIgnoreCase) >= 0));
            }

            return filteredList;
        }

        public IEnumerable<Floor> GetAll(int? noOfFloor, string? usePurpose)
        {
            var list = _floorRepo.GetAll();

            return Filter(list, noOfFloor, usePurpose);
        }

        public Floor? GetById(Guid id)
        {
            return _floorRepo.GetById(id) ?? throw new Exception("This floor id is not found!");
        }

        public IEnumerable<Floor> GetByProjectId(Guid id, int? noOfFloor, string? usePurpose)
        {
            var list = _floorRepo.GetByProjectId(id) ?? throw new Exception("This floor id is not found!");

            return Filter(list, noOfFloor, usePurpose);
        }

        public Floor? CreateFloor(FloorRequest request)
        {
            var floor = new Floor
            {
                Id = Guid.NewGuid(),
                Description = request.Description,
                FloorNo = request.FloorNo,
                UsePurpose = request.UsePurpose,
                ProjectId = request.ProjectId,
                IsDeleted = false,
            };

            var floorCreated = _floorRepo.Save(floor);
            return floorCreated;
        }

        public void DuplicateFloorsByProjectId(Guid createdId, Guid basedOnId)
        {
            var floors = _floorRepo.GetByProjectId(basedOnId);

            foreach (var floor in floors)
            {
                var floorRequest = new FloorRequest
                {
                    Description= floor.Description,
                    FloorNo = floor.FloorNo,
                    ProjectId = createdId,
                    UsePurpose = floor.UsePurpose,
                };
                var createdFloor = CreateFloor(floorRequest);

                RoomService roomService = new RoomService(_roomRepo, _roomTypeRepo, _projectRepo, _projectTaskRepo, _stageRepo, _projectDesignRepo, _stageDesignRepo, _floorRepo, _transactionRepo, _taskDesignRepo, _taskCategoryRepo);
                roomService.DuplicateRoomsByFloorId(createdFloor.Id, floor.Id, createdId);
            }
        }

        public void UpdateFloor(Guid id, FloorRequest request)
        {
            var floor = _floorRepo.GetById(id) ?? throw new Exception("This floor id is not found!");

            floor.Description = request.Description;
            floor.FloorNo = request.FloorNo;
            floor.UsePurpose = request.UsePurpose;
            floor.ProjectId = request.ProjectId;

            _floorRepo.Update(floor);
        }

        public void DeleteFloor(Guid id)
        {
            var floor = _floorRepo.GetById(id) ?? throw new Exception("This floor id is not found!");

            floor.IsDeleted = true;

            _floorRepo.Update(floor);
        }
    }
}
