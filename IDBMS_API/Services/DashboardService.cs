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

        private readonly IFloorRepository _floorRepo;
        private readonly IRoomRepository _roomRepo;
        private readonly IRoomTypeRepository _roomTypeRepo;
        private readonly IProjectTaskRepository _projectTaskRepo;

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

        public DashboardReponse? GetDashboardDataByAdmin()
        {
            ProjectService projectService = new (_projectRepo, _roomRepo, _roomTypeRepo, _projectTaskRepo, _stageRepo, _projectDesignRepo, _stageDesignRepo, _floorRepo);
            var recentProjects = projectService.GetRecentProjects().Take(5).ToList();
            var numOngoingProjects = projectService.GetOngoingProjects().Count();

            TaskReportService taskReportService = new (_taskReportRepo, _taskRepo, _projectRepo, _stageRepo, _projectDesignRepo, _stageDesignRepo, _taskDocumentRepo, _floorRepo, _roomRepo, _roomTypeRepo);
            var recentReports = taskReportService.GetRecentReports().Take(5).ToList();

            ProjectTaskService taskService = new ProjectTaskService(_taskRepo, _projectRepo, _stageRepo, _projectDesignRepo, _stageDesignRepo, _floorRepo, _roomRepo, _roomTypeRepo);
            var numOngoingTasks = taskService.GetOngoingTasks().Count();

            var dashboardData = new DashboardReponse
            {
                numOngoingProjects= numOngoingProjects,
                numOngoingTasks = numOngoingTasks,
                recentProjects = (List<Project>)recentProjects,
                recentReports = (List<TaskReport>)recentReports,
            };
            return dashboardData;
        }

        public DashboardReponse? GetDashboardDataByUserId(Guid id)
        {
            ProjectService projectService = new(_projectRepo, _roomRepo, _roomTypeRepo, _projectTaskRepo, _stageRepo, _projectDesignRepo, _stageDesignRepo, _floorRepo);
            var recentProjects = projectService.GetRecentProjectsByUserId(id).Take(5).ToList();
            var numOngoingProjects = projectService.GetOngoingProjectsByUserId(id).Count();

            TaskReportService taskReportService = new(_taskReportRepo, _taskRepo, _projectRepo, _stageRepo, _projectDesignRepo, _stageDesignRepo, _taskDocumentRepo, _floorRepo, _roomRepo, _roomTypeRepo);
            var recentReports = taskReportService.GetRecentReportsByUserId(id).Take(5).ToList();

            ProjectTaskService taskService = new ProjectTaskService(_taskRepo, _projectRepo, _stageRepo, _projectDesignRepo, _stageDesignRepo, _floorRepo, _roomRepo, _roomTypeRepo);
            var numOngoingTasks = taskService.GetOngoingTasksByUserId(id).Count();

            var dashboardData = new DashboardReponse
            {
                numOngoingProjects = numOngoingProjects,
                numOngoingTasks = numOngoingTasks,
                recentProjects = (List<Project>)recentProjects,
                recentReports = (List<TaskReport>)recentReports,
            };
            return dashboardData;
        }
    }
}
