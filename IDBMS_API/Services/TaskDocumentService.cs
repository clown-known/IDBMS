using BusinessObject.Models;
using Repository.Interfaces;

namespace IDBMS_API.Services
{
    public class TaskDocumentService
    {
        private readonly ITaskDocumentRepository _repository;

        public TaskDocumentService(ITaskDocumentRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<TaskDocument> GetAll()
        {
            return _repository.GetAll();
        }
    }
}
