using Azure.Core;
using BusinessObject.DTOs.Request;
using BusinessObject.Models;
using Repository.Implements;
using Repository.Interfaces;

namespace IDBMS_API.Services
{
    public class DecorProjectDesignService
    {
        private readonly IDecorProjectDesignRepository repository;
        public DecorProjectDesignService(IDecorProjectDesignRepository repository)
        {
            this.repository = repository;
        }
        public IEnumerable<DecorProjectDesign> GetAll()
        {
            return repository.GetAll();
        }
        public DecorProjectDesign? GetById(int id)
        {
            return repository.GetById(id);
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
            var dpdCreated = repository.Save(dpd);
            return dpdCreated;
        }
        public void UpdateDecorProjectDesign(int id, DecorProjectDesignRequest request)
        {
            var dpd = repository.GetById(id) ?? throw new Exception("This object is not existed!");
            dpd.MinBudget = request.MinBudget;
            dpd.MaxBudget = request.MaxBudget; 
            dpd.Name = request.Name;
            dpd.Description = request.Description;
            dpd.IsDeleted = request.IsDeleted;
            
            repository.Update(dpd);
        }
        public void UpdateDecorProjectDesignStatus(int id, bool isDeleted)
        {
            var dpd = repository.GetById(id) ?? throw new Exception("This object is not existed!");
            dpd.IsDeleted = isDeleted;
            repository.Update(dpd);
        }
    }
}
