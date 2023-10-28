using BusinessObject.DTOs.Request;
using BusinessObject.Models;
using Repository.Interfaces;
using System;
using System.Collections.Generic;

namespace IDBMS_API.Services
{
    public class FloorService
    {
        private readonly IFloorRepository _repository;

        public FloorService(IFloorRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Floor> GetAll()
        {
            return _repository.GetAll();
        }

        public Floor? GetById(Guid id)
        {
            return _repository.GetById(id) ?? throw new Exception("This object is not found!");
        }

        public IEnumerable<Floor> GetByProjectId(Guid projectId)
        {
            return _repository.GetByProjectId(projectId);
        }

        public Floor? CreateFloor(FloorRequest request)
        {
            var floor = new Floor
            {
                Id = Guid.NewGuid(),
                ProjectId = request.ProjectId,
                Description = request.Description,
                FloorNo = request.FloorNo,
                Area = request.Area,
                UsePurpose = request.UsePurpose,
                IsDeleted = false,
            };

            var floorCreated = _repository.Save(floor);
            return floorCreated;
        }

        public void UpdateFloor(Guid id, FloorRequest request)
        {
            var floor = _repository.GetById(id) ?? throw new Exception("This object is not found!");

            floor.ProjectId = request.ProjectId;
            floor.Description = request.Description;
            floor.FloorNo = request.FloorNo;
            floor.Area = request.Area;
            floor.UsePurpose = request.UsePurpose;

            _repository.Update(floor);
        }

        public void UpdateFloorStatus(Guid id, bool isDeleted)
        {
            var floor = _repository.GetById(id) ?? throw new Exception("This object is not found!");

            floor.IsDeleted = isDeleted;

            _repository.Update(floor);
        }

        public void DeleteFloor(Guid id)
        {
            var floor = _repository.GetById(id) ?? throw new Exception("This object is not found!");
            _repository.DeleteById(id);
        }
    }
}
