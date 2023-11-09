using BusinessObject.Models;
using Repository.Interfaces;

namespace IDBMS_API.Services
{
    public class TaskDesignService
    {
        private readonly ITaskDesignRepository _repository;

        public TaskDesignService(ITaskDesignRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<TaskDesign> GetAll()
        {
            return _repository.GetAll();
        }
    }

}
