using BusinessObject.DTOs.Request;
using BusinessObject.Models;
using Repository.Implements;
using Repository.Interfaces;

namespace IDBMS_API.Services
{
    public class PrepayStageDesignService
    {
        private readonly IPrepayStageDesignRepository _repository;
        public PrepayStageDesignService(IPrepayStageDesignRepository repository)
        {
            _repository = repository;
        }   
        public IEnumerable<PrepayStageDesign> GetAll()
        {
            return _repository.GetAll();
        }
        public IEnumerable<PrepayStageDesign> GetByDecorProjectDesignId(int designId)
        {
            return _repository.GetByDecorProjectDesignId(designId) ?? throw new Exception("This object is not existed!");
        }
        public PrepayStageDesign? GetById(int id)
        {
            return _repository.GetById(id) ?? throw new Exception("This object is not existed!");
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
                IsDeleted = false
            };

            var psdCreated = _repository.Save(psd);
            return psdCreated;
        }
        public void UpdatePrepayStageDesign(int id, PrepayStageDesignRequest request)
        {
            var psd = _repository.GetById(id) ?? throw new Exception("This object is not existed!");
            psd.PricePercentage = request.PricePercentage;
            psd.IsPrepaid = request.IsPrepaid;
            psd.StageNo = request.StageNo;
            psd.Name = request.Name;
            psd.Description = request.Description;
            psd.DecorProjectDesignId = request.DecorProjectDesignId;

            _repository.Update(psd);
        }
        public void DeletePrepayStageDesign(int id)
        {
            var psd = _repository.GetById(id) ?? throw new Exception("This object is not existed!");

            psd.IsDeleted= true;

            _repository.Save(psd);
        }
    }
}
