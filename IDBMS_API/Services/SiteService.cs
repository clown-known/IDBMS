using IDBMS_API.DTOs.Request;
using BusinessObject.Models;
using Repository.Interfaces;
using BusinessObject.Enums;

namespace IDBMS_API.Services
{
    public class SiteService
    {
        private readonly ISiteRepository _siteRepo;

        public SiteService(ISiteRepository siteRepo)
        {
            _siteRepo = siteRepo;
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
                ContactName= request.ContactName,
                ContactEmail = request.ContactEmail,
                ContactLocation = request.ContactLocation,
                ContactPhone = request.ContactPhone,
                CompanyCode = request.CompanyCode,
                IsDeleted = false,
            };
            var siteCreated = _siteRepo.Save(site);
            return siteCreated;
        }

        public void UpdateSite(Guid id, SiteRequest request)
        {
            var site = _siteRepo.GetById(id) ?? throw new Exception("This object is not existed!");

            site.Name = request.Name;
            site.Description = request.Description;
            site.Address = request.Address;
            site.ContactName = request.ContactName;
            site.ContactEmail = request.ContactEmail;
            site.ContactLocation = request.ContactLocation;
            site.ContactPhone = request.ContactPhone;
            site.CompanyCode = request.CompanyCode;

            _siteRepo.Update(site);
        }
        public void DeleteSite(Guid id)
        {
            var site = _siteRepo.GetById(id) ?? throw new Exception("This object is not existed!");

            site.IsDeleted = true;

            _siteRepo.Update(site);
        }

    }
       
}

