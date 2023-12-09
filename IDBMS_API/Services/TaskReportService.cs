using IDBMS_API.DTOs.Request;
using BusinessObject.Models;
using Repository.Interfaces;
using Azure.Core;
using BusinessObject.Enums;
using UnidecodeSharpFork;

namespace IDBMS_API.Services
{
    public class TaskReportService
    {
        private readonly ITaskReportRepository _taskReportRepo;
        private readonly IProjectTaskRepository _taskRepo;
        private readonly IProjectRepository _projectRepo;
        private readonly IPaymentStageRepository _stageRepo;
        private readonly IProjectDesignRepository _projectDesignRepo;
        private readonly IPaymentStageDesignRepository _stageDesignRepo;
        public TaskReportService(
            ITaskReportRepository taskReportRepo,
            IProjectTaskRepository taskRepo,
            IProjectRepository projectRepo,
            IPaymentStageRepository stageRepo,
            IProjectDesignRepository projectDesignRepo,
            IPaymentStageDesignRepository stageDesignRepo)
        {
            _taskReportRepo = taskReportRepo;
            _taskRepo = taskRepo;
            _projectRepo = projectRepo;
            _stageRepo = stageRepo;
            _projectDesignRepo = projectDesignRepo;
            _stageDesignRepo = stageDesignRepo;
        }

        public IEnumerable<TaskReport> Filter(IEnumerable<TaskReport> list,
           string? name)
        {
            IEnumerable<TaskReport> filteredList = list;

            if (name != null)
            {
                filteredList = filteredList.Where(item => (item.Name != null && item.Name.Unidecode().IndexOf(name.Unidecode(), StringComparison.OrdinalIgnoreCase) >= 0));
            }

            return filteredList;
        }

        public IEnumerable<TaskReport> GetAll(string? name)
        {
            var list = _taskReportRepo.GetAll();

            return Filter(list, name);
        }
        public TaskReport? GetById(Guid id)
        {
            return _taskReportRepo.GetById(id) ?? throw new Exception("This object is not existed!");
        }
        public IEnumerable<TaskReport?> GetByTaskId(Guid id, string? name)
        {
            var list = _taskReportRepo.GetByTaskId(id) ?? throw new Exception("This object is not existed!");

            return Filter(list, name);
        }

        public void UpdateTaskPercentage(Guid taskId)
        {
            var reportsInTask = _taskReportRepo.GetByTaskId(taskId);

            if (reportsInTask != null && reportsInTask.Any())
            {
                var latestReport = reportsInTask.OrderByDescending(r => r.UpdatedTime ?? r.CreatedTime).FirstOrDefault();
                if (latestReport != null)
                {
                    ProjectTaskService taskService = new(_taskRepo, _projectRepo, _stageRepo, _projectDesignRepo, _stageDesignRepo);
                    taskService.UpdateTaskProgress(taskId, latestReport.UnitUsed);
                }
            }
        }

        public TaskReport? CreateTaskReport(TaskReportRequest request)
        {
            var ctr = new TaskReport
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                UnitUsed = request.UnitUsed,
                Description = request.Description,
                CreatedTime = DateTime.Now,
                ProjectTaskId= request.ProjectTaskId,
                IsDeleted = false,
            };
            var ctrCreated = _taskReportRepo.Save(ctr);

            UpdateTaskPercentage(request.ProjectTaskId);

            return ctrCreated;
        }
        public void UpdateTaskReport(Guid id, TaskReportRequest request)
        {
            var ctr = _taskReportRepo.GetById(id) ?? throw new Exception("This object is not existed!");

            ctr.Name = request.Name;
            ctr.UnitUsed = request.UnitUsed;
            ctr.Description = request.Description;
            ctr.UpdatedTime = DateTime.Now;

            _taskReportRepo.Update(ctr);

            UpdateTaskPercentage(request.ProjectTaskId);
        }
        public void DeleteTaskReport(Guid id)
        {
            var ctr = _taskReportRepo.GetById(id) ?? throw new Exception("This object is not existed!");

            ctr.IsDeleted = true;

            _taskReportRepo.Update(ctr);

            UpdateTaskPercentage(ctr.ProjectTaskId);
        }
    }

}
