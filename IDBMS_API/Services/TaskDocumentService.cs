using IDBMS_API.DTOs.Request;
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
        public TaskDocument? GetById(Guid id)
        {
            return _repository.GetById(id) ?? throw new Exception("This object is not existed!");
        }
        public IEnumerable<TaskDocument?> GetByTaskReportId(Guid id)
        {
            return _repository.GetByTaskReportId(id) ?? throw new Exception("This object is not existed!");
        }
        public TaskDocument? CreateTaskDocument(TaskDocumentRequest request)
        {
            var td = new TaskDocument
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                Document = request.Document,
                TaskReportId = request.TaskReportId,
                IsDeleted = false,
            };
            var tdCreated = _repository.Save(td);
            return tdCreated;
        }
        public void DeleteTaskDocument(Guid id)
        {
            var td = _repository.GetById(id) ?? throw new Exception("This object is not existed!");

            td.IsDeleted = true;

            _repository.Update(td);
        }
    }
}
