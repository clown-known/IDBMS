using BusinessObject.DTOs.Request;
using BusinessObject.Models;
using Repository.Implements;
using Repository.Interfaces;

namespace IDBMS_API.Services
{
    public class PaymentStageDesignService
    {
        private readonly IPaymentStageDesignRepository _repository;
        public PaymentStageDesignService(IPaymentStageDesignRepository repository)
        {
            _repository = repository;
        }   
        public IEnumerable<PaymentStageDesign> GetAll()
        {
            return _repository.GetAll();
        }
        public IEnumerable<PaymentStageDesign> GetByDecorProjectDesignId(int designId)
        {
            return _repository.GetByDecorProjectDesignId(designId) ?? throw new Exception("This object is not existed!");
        }
        public PaymentStageDesign? GetById(int id)
        {
            return _repository.GetById(id) ?? throw new Exception("This object is not existed!");
        }
        public PaymentStageDesign? CreatePaymentStageDesign(PaymentStageDesignRequest request)
        {
            var psd = new PaymentStageDesign
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
        public void UpdatePaymentStageDesign(int id, PaymentStageDesignRequest request)
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
        public void DeletePaymentStageDesign(int id)
        {
            var psd = _repository.GetById(id) ?? throw new Exception("This object is not existed!");

            psd.IsDeleted= true;

            _repository.Save(psd);
        }
    }
}
