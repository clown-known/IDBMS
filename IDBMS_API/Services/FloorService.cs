using IDBMS_API.DTOs.Request;
using BusinessObject.Models;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Wordprocessing;

namespace IDBMS_API.Services
{
    public class FloorService
    {
        private readonly IFloorRepository _floorRepo;

        public FloorService(IFloorRepository floorRepo)
        {
            _floorRepo = floorRepo;
        }

        public IEnumerable<Floor> GetAll()
        {
            return _floorRepo.GetAll();
        }

        public Floor? GetById(Guid id)
        {
            return _floorRepo.GetById(id) ?? throw new Exception("This object is not found!");
        }

        public IEnumerable<Floor> GetByProjectId(Guid id)
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
                ProjectId = request.ProjectId,
                IsDeleted = false,
            };

            var floorCreated = _floorRepo.Save(floor);
            return floorCreated;
        }

        public void UpdateFloor(Guid id, FloorRequest request)
        {
            var floor = _floorRepo.GetById(id) ?? throw new Exception("This object is not found!");

            floor.Description = request.Description;
            floor.FloorNo = request.FloorNo;
            floor.UsePurpose = request.UsePurpose;
            floor.ProjectId = request.ProjectId;

            _floorRepo.Update(floor);
        }

        public void DeleteFloor(Guid id)
        {
            var floor = _floorRepo.GetById(id) ?? throw new Exception("This object is not found!");

            floor.IsDeleted = true;

            _floorRepo.Update(floor);
        }
    }
}
