using BusinessObject.Models;
using Repository.Interfaces;

namespace IDBMS_API.Services
{
    public class WarrantyClaimService
    {
        private readonly IWarrantyClaimRepository _repository;

        public WarrantyClaimService(IWarrantyClaimRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<WarrantyClaim> GetAll()
        {
            return _repository.GetAll();
        }
    }

}
