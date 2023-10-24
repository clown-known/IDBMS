using BusinessObject.DTOs.Request.CreateRequests;
using BusinessObject.Models;
using Repository.Interfaces;

namespace IDBMS_API.Services
{
    public class FloorService
    {
        private readonly IFloorRepository _floorRepository;
        public FloorService(IFloorRepository floorRepository)
        {
            _floorRepository = floorRepository;
        }
        public IEnumerable<Floor> GetAll()
        {
            return _floorRepository.GetAll();
        }
        public Floor? GetById(Guid id)
        {
            return _floorRepository.GetById(id);
        }
        public Floor? CreateFloor(FloorRequest request)
        {
            var floor = new Floor
            {
                Name = request.Name,
                Description = request.Description,
                UsePurpose = request.UsePurpose,
                FloorNo = request.FloorNo,
                Area = request.Area,
                ProjectId = request.ProjectId,
                IsDeleted = request.IsDeleted,
            };
            var floorCreated = _floorRepository.Save(floor);
            return floorCreated;
        }
        public void UpdateFloor(FloorRequest request)
        {
            var floor = new Floor
            {
                Name = request.Name,
                Description = request.Description,
                UsePurpose = request.UsePurpose,
                FloorNo = request.FloorNo,
                Area = request.Area,
                ProjectId = request.ProjectId,
                IsDeleted = request.IsDeleted,
            };
            _floorRepository.Update(floor);
        }

    }
}
