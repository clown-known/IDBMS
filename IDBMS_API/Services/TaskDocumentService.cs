using IDBMS_API.DTOs.Request;
using BusinessObject.Models;
using Repository.Interfaces;
using BLL.Services;

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
            return _repository.GetById(id) ?? throw new Exception("This task document id is not existed!");
        }
        public IEnumerable<TaskDocument> GetByTaskReportId(Guid id)
        {
            return _repository.GetByTaskReportId(id) ?? throw new Exception("This task document id is not existed!");
        }
        public async Task<TaskDocument?> CreateTaskDocument(Guid projectId, Guid taskReportId, TaskDocumentRequest request)
        {
            string link = "";
            if(request.Document != null)
            {
                FirebaseService s = new FirebaseService();
                link = await s.UploadDocument(request.Document,projectId);
            }
            var td = new TaskDocument
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Document = link,
                TaskReportId = taskReportId,
                IsDeleted = false,
            };
            var tdCreated = _repository.Save(td);
            return tdCreated;
        }
        public void DeleteTaskDocument(Guid id)
        {
            var td = _repository.GetById(id) ?? throw new Exception("This task document id is not existed!");

            td.IsDeleted = true;

            _repository.Update(td);
        }
    }
}
