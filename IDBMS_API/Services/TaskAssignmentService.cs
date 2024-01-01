using IDBMS_API.DTOs.Request;
using BusinessObject.Models;
using Repository.Interfaces;
using BusinessObject.Enums;
using UnidecodeSharpFork;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace IDBMS_API.Services
{
    public class TaskAssignmentService
    {
        private readonly ITaskAssignmentRepository _repository;

        public TaskAssignmentService(ITaskAssignmentRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<TaskAssignment> Filter(IEnumerable<TaskAssignment> list,
           string? name)
        {
            IEnumerable<TaskAssignment> filteredList = list;

            if (name != null)
            {
                filteredList = filteredList.Where(item => (item.ProjectParticipation.User.Name != null && item.ProjectParticipation.User.Name.Unidecode().IndexOf(name.Unidecode(), StringComparison.OrdinalIgnoreCase) >= 0));
            }

            return filteredList;
        }

        public IEnumerable<TaskAssignment> GetAll(string? name)
        {
            var list =_repository.GetAll();

            return Filter(list, name);
        }

        public TaskAssignment? GetById(Guid id)
        {
            return _repository.GetById(id) ?? throw new Exception("This task assignment id is not existed!");
        }

        public IEnumerable<TaskAssignment> GetByProjectId(Guid id, string? name)
        {
            var list = _repository.GetByProjectId(id) ?? throw new Exception("This task assignment id is not existed!");

            return Filter(list, name);
        }

        public IEnumerable<TaskAssignment> GetByUserId(Guid id, string? name)
        {
            var list = _repository.GetByUserId(id) ?? throw new Exception("This task assignment id is not existed!");

            return Filter(list, name);
        }

        public IEnumerable<TaskAssignment> GetByTaskId(Guid id, string? name)
        {
            var list = _repository.GetByTaskId(id) ?? throw new Exception("This task assignment id is not existed!");

            return Filter(list, name);
        }

        public void CreateTaskAssignment(TaskAssignmentRequest request)
        {
            foreach (var taskId in request.ProjectTaskId)
            {
                var listAssignment = GetByTaskId(taskId, null);

                foreach (var participationId in request.ProjectParticipationId)
                {
                    //check participation exist
                    if (!listAssignment.Any(a => a.ProjectParticipationId == participationId))
                    {
                        var ta = new TaskAssignment
                        {
                            Id = Guid.NewGuid(),
                            ProjectParticipationId = participationId,
                            ProjectTaskId = taskId,
                            CreatedDate = DateTime.Now,
                        };

                        _repository.Save(ta);
                    }
                }
            }
        }

        public TaskAssignment? UpdateTaskAssignmentByTaskId(Guid taskId, List<Guid> request)
        {
            var listAssignment = GetByTaskId(taskId, null);

            foreach (var assignment in listAssignment)
            {
                //check deleted
                if (!request.Any(a=> a == assignment.ProjectParticipationId))
                {
                    DeleteTaskAssignment(assignment.Id);
                }
            }

            foreach (var participationId in request)
            {
                //check participation exist
                if (!listAssignment.Any(a => a.ProjectParticipationId == participationId))
                {
                    var ta = new TaskAssignment
                    {
                        Id = Guid.NewGuid(),
                        ProjectParticipationId = participationId,
                        ProjectTaskId = taskId,
                        CreatedDate = DateTime.Now,
                    };

                    _repository.Save(ta);
                }
            }

            return null;
        }

        public void DeleteTaskAssignment(Guid id)
        {
            var ta = _repository.GetById(id) ?? throw new Exception("This task assignment id is not existed!");

            _repository.DeleteById(id);
        }

        public void DeleteTaskAssignmentByParticipationId(Guid participationId)
        {
            var list = _repository.GetByParticipationId(participationId);

            foreach (var assignment in list)
            {
                _repository.DeleteById(assignment.Id);
            }
        }
    }

}
