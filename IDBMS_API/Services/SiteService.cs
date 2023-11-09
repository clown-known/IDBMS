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
    }

}
