using Azure.Core;
using BusinessObject.DTOs.Request;
using BusinessObject.Models;
using Repository.Implements;
using Repository.Interfaces;

namespace IDBMS_API.Services
{
    public class DecorProjectDesignService
    {
        private readonly IDecorProjectDesignRepository _repository;
        public DecorProjectDesignService(IDecorProjectDesignRepository repository)
        {
            _repository = repository;
        }
        public IEnumerable<DecorProjectDesign> GetAll()
        {
            return _repository.GetAll();
        }
        public DecorProjectDesign? GetById(int id)
        {
            return _repository.GetById(id) ?? throw new Exception("This object is not existed!");
        }
        public DecorProjectDesign? CreateDecorProjectDesign(DecorProjectDesignRequest request)
        {
            var dpd = new DecorProjectDesign
            {
                MinBudget = request.MinBudget,
                MaxBudget = request.MaxBudget,
                Name = request.Name,    
                Description = request.Description,
                IsDeleted = request.IsDeleted,
            };
            var dpdCreated = _repository.Save(dpd);
            return dpdCreated;
        }
        public void UpdateDecorProjectDesign(int id, DecorProjectDesignRequest request)
        {
            var dpd = _repository.GetById(id) ?? throw new Exception("This object is not existed!");
            dpd.MinBudget = request.MinBudget;
            dpd.MaxBudget = request.MaxBudget; 
            dpd.Name = request.Name;
            dpd.Description = request.Description;
            dpd.IsDeleted = request.IsDeleted;
            
            _repository.Update(dpd);
        }
        public void UpdateDecorProjectDesignStatus(int id, bool isDeleted)
        {
            var dpd = _repository.GetById(id) ?? throw new Exception("This object is not existed!");
            dpd.IsDeleted = isDeleted;
            _repository.Update(dpd);
        }
    }
}
