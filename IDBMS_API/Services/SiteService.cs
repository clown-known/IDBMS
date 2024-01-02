using IDBMS_API.DTOs.Request;
using BusinessObject.Models;
using Repository.Interfaces;
using BusinessObject.Enums;
using UnidecodeSharpFork;

namespace IDBMS_API.Services
{
    public class SiteService
    {
        private readonly ISiteRepository _siteRepo;

        public SiteService(ISiteRepository siteRepo)
        {
            _siteRepo = siteRepo;
        }

        private IEnumerable<Site> Filter(IEnumerable<Site> list,
            string? nameOrAddress)
        {
            IEnumerable<Site> filteredList = list;

            if (nameOrAddress != null)
            {
                filteredList = filteredList.Where(item =>
                            (item.Address != null && item.Address.Unidecode().IndexOf(nameOrAddress.Unidecode(), StringComparison.OrdinalIgnoreCase) >= 0) ||
                            (item.Name != null && item.Name.Unidecode().IndexOf(nameOrAddress.Unidecode(), StringComparison.OrdinalIgnoreCase) >= 0));
            }

            return filteredList;
        }

        public IEnumerable<Site> GetAll(string? nameOrAddress)
        {
            var list = _siteRepo.GetAll();

            return Filter(list, nameOrAddress);
        }
        public Site? GetById(Guid id)
        {
            return _siteRepo.GetById(id) ?? throw new Exception("This site id is not existed!");
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
                CreatedDate = DateTime.Now,
            };
            var siteCreated = _siteRepo.Save(site);
            return siteCreated;
        }

        public void UpdateSite(Guid id, SiteRequest request)
        {
            var site = _siteRepo.GetById(id) ?? throw new Exception("This site id is not existed!");

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
            var site = _siteRepo.GetById(id) ?? throw new Exception("This site id is not existed!");

            site.IsDeleted = true;

            _siteRepo.Update(site);
        }

    }
       
}

