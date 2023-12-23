using IDBMS_API.DTOs.Request;
using BusinessObject.Models;
using Repository.Implements;
using Repository.Interfaces;
using BusinessObject.Enums;
using UnidecodeSharpFork;

namespace IDBMS_API.Services
{
    public class PaymentStageDesignService
    {
        private readonly IPaymentStageDesignRepository _repository;
        public PaymentStageDesignService(IPaymentStageDesignRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<PaymentStageDesign> Filter(IEnumerable<PaymentStageDesign> list,
           string? name)
        {
            IEnumerable<PaymentStageDesign> filteredList = list;

            if (name != null)
            {
                filteredList = filteredList.Where(item => (item.Name != null && item.Name.Unidecode().IndexOf(name.Unidecode(), StringComparison.OrdinalIgnoreCase) >= 0));
            }

            return filteredList;
        }

        public IEnumerable<PaymentStageDesign> GetAll(string? name)
        {
            var list = _repository.GetAll();

            return Filter(list, name);
        }
        public IEnumerable<PaymentStageDesign> GetByProjectDesignId(int id, string? name)
        {
            var list =_repository.GetByProjectDesignId(id) ?? throw new Exception("This object is not existed!");

            return Filter(list, name);
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
                EnglishName= request.EnglishName,
                Description = request.Description,
                EnglishDescription= request.EnglishDescription,
                ProjectDesignId= request.ProjectDesignId,
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
            psd.EnglishName = request.EnglishName;
            psd.Description = request.Description;
            psd.EnglishDescription = request.EnglishDescription;
            psd.ProjectDesignId = request.ProjectDesignId;

            _repository.Update(psd);
        }
        public void DeletePaymentStageDesign(int id)
        {
            var psd = _repository.GetById(id) ?? throw new Exception("This object is not existed!");

            psd.IsDeleted= true;

            _repository.Update(psd);
        }
    }
}
