using BusinessObject.DTOs.Request;
using BusinessObject.Models;
using Repository.Interfaces;

namespace IDBMS_API.Services
{
    public class ApplianceSuggestionService
    {
        private readonly IApplianceSuggestionRepository _repository;
        public ApplianceSuggestionService(IApplianceSuggestionRepository repository)
        {
            _repository = repository;
        }
        public IEnumerable<ApplianceSuggestion> GetAll()
        {
            return _repository.GetAll();
        }
        public ApplianceSuggestion? GetById(Guid id)
        {
            return _repository.GetById(id);
        }
        public ApplianceSuggestion? CreateApplianceSuggestion(ApplianceSuggestionRequest request)
        {
            var applianceSuggestion = new ApplianceSuggestion
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                ImageUrl = request.ImageUrl,
                InteriorItemId = request.InteriorItemId,
                RoomId = request.RoomId,
            };
            var asCreated = _repository.Save(applianceSuggestion);
            return asCreated;
        }
        public void UpdateApplianceSuggestion(Guid id, ApplianceSuggestionRequest request)
        {
            var applianceSuggestion = _repository.GetById(id) ?? throw new Exception("This object is not existed!");
            applianceSuggestion.Name = request.Name;
            applianceSuggestion.Description = request.Description;
            applianceSuggestion.ImageUrl = request.ImageUrl;
            applianceSuggestion.InteriorItemId = request.InteriorItemId;
            applianceSuggestion.RoomId = request.RoomId;


            _repository.Update(applianceSuggestion);
        }
        public void DeleteApplianceSuggestion(Guid id)
        {
            var applianceSuggestion = _repository.GetById(id) ?? throw new Exception("This object is not existed!");
            _repository.DeleteById(id);
        }
    }
}
