using IDBMS_API.DTOs.Request;
using IDBMS_API.DTOs.Request.BookingRequest;
using BusinessObject.Models;
using Repository.Interfaces;

namespace IDBMS_API.Services
{
    public class SiteService
    {
        private readonly ISiteRepository _siteRepo;
        private readonly IFloorRepository _floorRepo;
        private readonly IRoomRepository _roomRepo;
        private readonly IProjectTaskRepository _taskRepo;

        public SiteService(
                ISiteRepository siteRepo,
                IFloorRepository floorRepo,
                IRoomRepository roomRepo,
                IProjectTaskRepository taskRepo)
        {
            _siteRepo = siteRepo;
            _floorRepo = floorRepo;
            _roomRepo = roomRepo;
            _taskRepo = taskRepo;
        }
        public IEnumerable<Site> GetAll()
        {
            return _siteRepo.GetAll();
        }
        public Site? GetById(Guid id)
        {
            return _siteRepo.GetById(id) ?? throw new Exception("This object is not existed!");
        }
        public Site? CreateSite(SiteRequest request)
        {
            var site = new Site
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                Address = request.Address,
                IsDeleted = false,
            };
            var siteCreated = _siteRepo.Save(site);
            return siteCreated;
        }
        public void CreateBookSite(Guid projectId, List<BookingSiteRequest> request)
        {
            foreach (var siteRequest in request)
            {
                var site = new Site
                {
                    Id = Guid.NewGuid(),
                    Name = siteRequest.Name,
                    Description = siteRequest.Description,
                    Address = siteRequest.Address,
                    IsDeleted = false,
                };

                var siteCreated = _siteRepo.Save(site);

                if (siteCreated != null)
                {
                    if (siteRequest.Floors != null)
                    {
                        FloorService floorService = new FloorService(_floorRepo, _roomRepo, _taskRepo);
                        floorService.CreateBookFloor(projectId, siteCreated.Id, siteRequest.Floors);
                    }
                }
            }
        }
        public void UpdateSite(Guid id, SiteRequest request)
        {
            var site = _siteRepo.GetById(id) ?? throw new Exception("This object is not existed!");

            site.Name = request.Name;
            site.Description = request.Description;
            site.Address = request.Address;

            _siteRepo.Save(site);
        }
        public void DeleteSite(Guid id)
        {
            var site = _siteRepo.GetById(id) ?? throw new Exception("This object is not existed!");

            site.IsDeleted = true;

            _siteRepo.Save(site);
        }

    }
       
}

