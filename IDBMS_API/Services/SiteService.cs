using BusinessObject.DTOs.Request;
using BusinessObject.Models;
using Repository.Interfaces;

namespace IDBMS_API.Services
{
    public class SiteService
    {
        private readonly ISiteRepository _repository;

        public SiteService(ISiteRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Site> GetAll()
        {
            return _repository.GetAll();
        }
        public Site? GetById(Guid id)
        {
            return _repository.GetById(id) ?? throw new Exception("This object is not existed!");
        }
        public IEnumerable<Site?> GetByProjectId(Guid id)
        {
            return _repository.GetByProjectId(id) ?? throw new Exception("This object is not existed!");
        }
        public Site? CreateSite(SiteRequest request)
        {
            var site = new Site
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                Address = request.Address,
                UsePurpose = request.UsePurpose,
                Area = request.Area,
                ProjectId = request.ProjectId,
                IsDeleted = false,
            };
            var siteCreated = _repository.Save(site);
            return siteCreated;
        }
        public void UpdateSite(Guid id, SiteRequest request)
        {
            var site = _repository.GetById(id) ?? throw new Exception("This object is not existed!");

            site.Name = request.Name;
            site.Description = request.Description;
            site.Address = request.Address;
            site.UsePurpose = request.UsePurpose;
            site.Area = request.Area;
            site.ProjectId = request.ProjectId;

            _repository.Save(site);
        }
        public void DeleteSite(Guid id)
        {
            var site = _repository.GetById(id) ?? throw new Exception("This object is not existed!");

            site.IsDeleted = true;

            _repository.Save(site);
        }

    }
       
}

