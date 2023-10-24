using Azure.Core;
using BusinessObject.DTOs.Request.CreateRequests;
using BusinessObject.Models;
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
        public void UpdateDecorProjectDesign(DecorProjectDesignRequest request)
        {
            var dpd = new DecorProjectDesign
            {
                MinBudget = request.MinBudget,
                MaxBudget = request.MaxBudget,
                Name = request.Name,
                Description = request.Description,
                IsDeleted = request.IsDeleted,
            };
            repository.Update(dpd);
        }
        public void UpdateIsDeleted(int id)
        {
            var dpd = repository.GetById(id);
            var dpdDeleted = new DecorProjectDesign
            {
                MinBudget = dpd.MinBudget,
                MaxBudget = dpd.MaxBudget,
                Name = dpd.Name,
                Description = dpd.Description,
                IsDeleted = true,
            };
            repository.Update(dpdDeleted);
        }
    }
}
