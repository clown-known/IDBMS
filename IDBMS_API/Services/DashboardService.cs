using BusinessObject.Models;
using IDBMS_API.DTOs.Response;
using Repository.Interfaces;

namespace IDBMS_API.Services
{
    public class DashboardService
    {
        private readonly IProjectRepository _projectRepo;
        private readonly ITaskReportRepository _taskReportRepo;
        private readonly IProjectTaskRepository _taskRepo;
        private readonly IPaymentStageRepository _stageRepo;
        private readonly IProjectDesignRepository _projectDesignRepo;
        private readonly IPaymentStageDesignRepository _stageDesignRepo;
        private readonly ITaskDocumentRepository _taskDocumentRepo;

        public DashboardService(
                IProjectRepository projectRepo,
                ITaskReportRepository taskReportRepo,
                IProjectTaskRepository taskRepo,
                IPaymentStageRepository stageRepo,
                IProjectDesignRepository projectDesignRepo,
                IPaymentStageDesignRepository stageDesignRepo,
                ITaskDocumentRepository taskDocumentRepo)
        {
            _projectRepo = projectRepo;
            _taskReportRepo = taskReportRepo;
            _taskRepo = taskRepo;
            _stageRepo = stageRepo;
            _projectDesignRepo = projectDesignRepo;
            _stageDesignRepo = stageDesignRepo;
            _taskDocumentRepo = taskDocumentRepo;
        }

        public DashboardReponse? GetDashboardData()
        {
            ProjectService projectService = new (_projectRepo);
            var recentProjects = projectService.GetRecentProjects().Take(5).ToList();

            TaskReportService taskReportService = new (_taskReportRepo, _taskRepo, _projectRepo, _stageRepo, _projectDesignRepo, _stageDesignRepo, _taskDocumentRepo);
            var recentReports = taskReportService.GetRecentReports().Take(5).ToList();

            var dashboardData = new DashboardReponse
            {
                recentProjects = (List<Project>)recentProjects,
                recentReports = (List<TaskReport>)recentReports,
            };
            return dashboardData;
        }
    }
}
