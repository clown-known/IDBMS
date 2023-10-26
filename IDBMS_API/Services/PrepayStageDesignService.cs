using BusinessObject.DTOs.Request;
using BusinessObject.Models;
using Repository.Interfaces;

namespace IDBMS_API.Services
{
    public class PrepayStageDesignService
    {
        private readonly IPrepayStageDesignRepository repository;
        public PrepayStageDesignService(IPrepayStageDesignRepository repository)
        {
            this.repository = repository;
        }   
        public IEnumerable<PrepayStageDesign> GetAll()
        {
            return repository.GetAll();
        }
        public IEnumerable<PrepayStageDesign> GetByDecorProjectDesignId(int designId)
        {
            return repository.GetByDecorProjectDesignId(designId);
        }
        public PrepayStageDesign? GetById(int id)
        {
            return repository.GetById(id);
        }
        public PrepayStageDesign? CreatePrepayStageDesign(PrepayStageDesignRequest request)
        {
            var psd = new PrepayStageDesign
            {
                PricePercentage = request.PricePercentage,
                IsPrepaid = request.IsPrepaid,
                StageNo = request.StageNo,
                Name = request.Name,
                Description = request.Description,
                DecorProjectDesignId = request.DecorProjectDesignId,
            };
            var psdCreated = repository.Save(psd);
            return psdCreated;
        }
        public void UpdatePrepayStageDesign(PrepayStageDesignRequest request)
        {
            var psd = new PrepayStageDesign
            {
                PricePercentage = request.PricePercentage,
                IsPrepaid = request.IsPrepaid,
                StageNo = request.StageNo,
                Name = request.Name,
                Description = request.Description,
                DecorProjectDesignId = request.DecorProjectDesignId,
            };
            repository.Update(psd);
        }
        public void DeletePrepayStageDesign(int id)
        {
            repository.DeleteById(id);
        }
    }
}
