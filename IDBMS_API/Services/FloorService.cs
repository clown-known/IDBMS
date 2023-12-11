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

        public FloorService(IFloorRepository floorRepo)
        {
            _floorRepo = floorRepo;
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
            return _floorRepo.GetById(id) ?? throw new Exception("This object is not found!");
        }

        public IEnumerable<Floor> GetByProjectId(Guid id, int? noOfFloor, string? usePurpose)
        {
            var list = _floorRepo.GetByProjectId(id) ?? throw new Exception("This object is not found!");

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
