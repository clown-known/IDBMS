using Azure.Core;
using IDBMS_API.DTOs.Request;
using BusinessObject.Enums;
using BusinessObject.Models;
using Repository.Interfaces;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Mvc;
using DocumentFormat.OpenXml.Office2016.Excel;
using System.Text;
using System.Globalization;
using UnidecodeSharpFork;
using System.Linq;
using Repository.Implements;
using IDBMS_API.Supporters.TimeHelper;

namespace IDBMS_API.Services
{
    public class ProjectTaskService
    {
        private readonly IProjectTaskRepository _taskRepo;
        private readonly IProjectRepository _projectRepo;
        private readonly IPaymentStageRepository _stageRepo;
        private readonly IProjectDesignRepository _projectDesignRepo;
        private readonly IPaymentStageDesignRepository _stageDesignRepo;
        private readonly IFloorRepository _floorRepo;
        private readonly IRoomRepository _roomRepo;
        private readonly IRoomTypeRepository _roomTypeRepo;
        private readonly ITransactionRepository _transactionRepo;
        private readonly ITaskCategoryRepository _taskCategoryRepo;
        private readonly ITaskDesignRepository _taskDesignRepo;

        public ProjectTaskService(
                IProjectTaskRepository taskRepo,
                IProjectRepository projectRepo,
                IPaymentStageRepository stageRepo,
                IProjectDesignRepository projectDesignRepo,
                IPaymentStageDesignRepository stageDesignRepo,
                IFloorRepository floorRepo,
                IRoomRepository roomRepo,
                IRoomTypeRepository roomTypeRepo,
                ITransactionRepository transactionRepo,
                ITaskCategoryRepository taskCategoryRepo,
                ITaskDesignRepository taskDesignRepo)
        {
            _taskRepo = taskRepo;
            _projectRepo = projectRepo;
            _stageRepo = stageRepo;
            _projectDesignRepo = projectDesignRepo;
            _stageDesignRepo = stageDesignRepo;
            _floorRepo = floorRepo;
            _roomRepo = roomRepo;
            _roomTypeRepo = roomTypeRepo;
            _transactionRepo = transactionRepo;
            _taskCategoryRepo = taskCategoryRepo;
            _taskDesignRepo = taskDesignRepo;
        }

        public IEnumerable<ProjectTask> Filter(IEnumerable<ProjectTask> list, 
            string? codeOrName, Guid? stageId, ProjectTaskStatus? taskStatus, int? taskCategoryId, Guid? roomId, 
            bool includeRoomIdFilter, bool includeStageIdFilter, Guid? participationId)
        {
            IEnumerable<ProjectTask> filteredList = list;

            if (codeOrName != null)
            {
                filteredList = filteredList.Where(item =>
                            (item.Code != null && item.Code.Unidecode().IndexOf(codeOrName.Unidecode(), StringComparison.OrdinalIgnoreCase) >= 0) ||
                            (item.Name != null && item.Name.Unidecode().IndexOf(codeOrName.Unidecode(), StringComparison.OrdinalIgnoreCase) >= 0));
            }

            if (includeStageIdFilter)
            {
                filteredList = filteredList.Where(item => item.PaymentStageId == stageId);
            }

            if (taskStatus != null)
            {
                filteredList = filteredList.Where(item => item.Status == taskStatus);
            }

            if (taskCategoryId != null)
            {
                filteredList = filteredList.Where(item => item.TaskCategoryId == taskCategoryId);
            }

            if (participationId != null)
            {
                filteredList = filteredList.Where(item => item.TaskAssignments.Any(a => a.ProjectParticipationId == participationId)).ToList();
            }

            if (includeRoomIdFilter)
            {
                filteredList = filteredList.Where(item => item.RoomId == roomId);
            }

            return filteredList;
        }
        public bool CheckCodeExisted(string code)
        {
            return _taskRepo.CheckCodeExisted(code);
        }

        public string GenerateCode(int? categoryId)
        {
            string code = String.Empty;
            Random random = new ();

            for (int attempt = 0; attempt < 10; attempt++)
            {
                // Generate the code
                code = GenerateSingleCode(categoryId, random);

                
                bool codeExistsInTask = CheckCodeExisted(code);

                TaskDesignService taskDesignService = new (_taskDesignRepo, _taskCategoryRepo);
                bool codeExistsInTaskDesign = taskDesignService.CheckCodeExisted(code);

                if (codeExistsInTask == false && codeExistsInTaskDesign == false)
                {
                    return code;
                }
            }

            throw new Exception("Failed to generate a unique code after 10 attempts.");
        }

        public string GenerateSingleCode(int? categoryId, Random random)
        {
            string code = String.Empty;

            code += "CVDT_"; // cong viec dac thu

            code += random.Next(100000, 999999);

            return code;
        }

        public IEnumerable<ProjectTask> GetAll()
        {
            return _taskRepo.GetAll();
        }

        public ProjectTask? GetById(Guid id)
        {
            return _taskRepo.GetById(id) ?? throw new Exception("This project task id is not existed!");
        }

        public IEnumerable<ProjectTask?> GetByProjectId(Guid id, 
            string? codeOrName, Guid? stageId, ProjectTaskStatus? taskStatus, int? taskCategoryId, Guid? roomId, 
            bool includeRoomIdFilter, bool includeStageIdFilter, Guid? participationId)
        {
            var list = _taskRepo.GetByProjectId(id);
            
            return Filter(list, codeOrName, stageId, taskStatus, taskCategoryId, roomId, includeRoomIdFilter, includeStageIdFilter, participationId);
        }

        public bool CheckFinishedTaskInStage(Guid stageId)
        {
            var list = _taskRepo.GetByPaymentStageId(stageId);

            return !list.Any(task => task.Status == ProjectTaskStatus.Pending || task.Status == ProjectTaskStatus.Confirmed || task.Status == ProjectTaskStatus.Ongoing);
        }

        public IEnumerable<Guid> GetAllProjectTaskIdByFilter(Guid projectId,
            string? codeOrName, Guid? stageId, ProjectTaskStatus? taskStatus, int? taskCategoryId, Guid? roomId, 
            bool includeRoomIdFilter, bool includeStageIdFilter, Guid? participationId)
        {
            var list = _taskRepo.GetByProjectId(projectId);

            var filteredList = Filter(list, codeOrName, stageId, taskStatus, taskCategoryId, roomId, includeRoomIdFilter, includeStageIdFilter, participationId);

            var filteredIds = filteredList.Select(item => item.Id).ToList();

            return filteredIds;
        }

        public IEnumerable<ProjectTask> GetByRoomId(Guid id)
        {
            return _taskRepo.GetByRoomId(id);
        }

        public IEnumerable<ProjectTask> GetByPaymentStageId(Guid id)
        {
            return _taskRepo.GetByPaymentStageId(id);
        }

        public IEnumerable<ProjectTask> GetOngoingTasks()
        {
            return _taskRepo.GetOngoingTasks();
        }

        public IEnumerable<ProjectTask> GetOngoingTasksByUserId(Guid id)
        {
            return _taskRepo.GetOngoingTasksByUserId(id);
        }

        public void UpdateProjectData(Guid projectId)
        {
            var tasksInProject = _taskRepo.GetByProjectId(projectId);

            decimal estimatePrice = 0;
            decimal finalPrice = 0;
            int estimateBusinessDay = 0;

            if (tasksInProject != null && tasksInProject.Any())
            {
                estimatePrice = tasksInProject.Sum(task =>
                {
                    if (task != null && task.Status != ProjectTaskStatus.Cancelled)
                    {
                        decimal pricePerUnit = task.PricePerUnit;
                        double unitInContract = task.UnitInContract;
                        return pricePerUnit * (decimal)unitInContract;
                    }
                    return 0; 
                });

                finalPrice = tasksInProject.Sum(task =>
                {
                    if (task != null && task.Status != ProjectTaskStatus.Cancelled)
                    {
                        decimal pricePerUnit = task.PricePerUnit;
                        double unit = 0;

                        if (task.Status == ProjectTaskStatus.Done)
                        {
                            unit = task.UnitUsed;
                        }
                        else
                        {
                            unit = task.UnitUsed > task.UnitInContract ? task.UnitUsed : task.UnitInContract;
                        }

                        return pricePerUnit * (decimal)unit;
                    }
                    return 0;
                });

                var soonestStartDate = tasksInProject
                    .Where(task => task != null && task.StartedDate != null && task.Status != ProjectTaskStatus.Cancelled)
                    .Min(task => task.StartedDate);

                var latestEndDate = tasksInProject
                    .Where(task => task != null && task.EndDate != null && task.Status != ProjectTaskStatus.Cancelled)
                    .Max(task => task.EndDate);

                if (soonestStartDate.HasValue && latestEndDate.HasValue)
                {
                    TimeSpan duration = latestEndDate.Value - soonestStartDate.Value;

                    for (DateTime date = soonestStartDate.Value; date <= latestEndDate.Value; date = date.AddDays(1))
                    {
                        if (date.DayOfWeek != DayOfWeek.Sunday)
                        {
                            estimateBusinessDay++;
                        }
                    }
                }
            }

            ProjectService projectService = new (_projectRepo, _roomRepo, _roomTypeRepo, _taskRepo, _stageRepo, _projectDesignRepo, _stageDesignRepo, _floorRepo, _transactionRepo, _taskDesignRepo, _taskCategoryRepo);
            projectService.UpdateProjectDataByTask(projectId, estimatePrice, finalPrice, estimateBusinessDay);

            PaymentStageService stageService = new(_stageRepo, _projectRepo, _projectDesignRepo, _stageDesignRepo, _taskRepo, _floorRepo, _roomRepo, _roomTypeRepo, _transactionRepo, _taskDesignRepo, _taskCategoryRepo);
            stageService.UpdateStagesTotalContractPaid(projectId, estimatePrice);
        }

        public void UpdatePaymentStageData(Guid projectId)
        {
            PaymentStageService stageService = new (_stageRepo, _projectRepo, _projectDesignRepo, _stageDesignRepo, _taskRepo, _floorRepo, _roomRepo, _roomTypeRepo, _transactionRepo, _taskDesignRepo, _taskCategoryRepo);
            var stagesByProjectId = stageService.GetByProjectId(projectId, null, null);

            ProjectService projectService = new(_projectRepo, _roomRepo, _roomTypeRepo, _taskRepo, _stageRepo, _projectDesignRepo, _stageDesignRepo, _floorRepo, _transactionRepo, _taskDesignRepo, _taskCategoryRepo);
            var project = projectService.GetById(projectId);

            foreach (var stage in stagesByProjectId)
            {
                var tasksInStage = _taskRepo.GetByPaymentStageId(stage.Id);
                int estimateBusinessDay = 0;
                decimal totalIncurredPaid = 0;

                if (tasksInStage != null && tasksInStage.Any())
                {
                    var soonestStartDate = tasksInStage
                    .Where(task => task != null && task.StartedDate != null && task.Status != ProjectTaskStatus.Cancelled)
                    .Min(task => task.StartedDate);

                    var latestEndDate = tasksInStage
                        .Where(task => task != null && task.EndDate != null && task.Status != ProjectTaskStatus.Cancelled)
                        .Max(task => task.EndDate);

                    if (soonestStartDate.HasValue && latestEndDate.HasValue)
                    {
                        TimeSpan duration = latestEndDate.Value - soonestStartDate.Value;

                        for (DateTime date = soonestStartDate.Value; date <= latestEndDate.Value; date = date.AddDays(1))
                        {
                            if (date.DayOfWeek != DayOfWeek.Sunday)
                            {
                                estimateBusinessDay++;
                            }
                        }

                        if (project != null && 
                            (project.Status == ProjectStatus.Negotiating))
                        {
                            stageService.UpdateStageTimeSpan(stage.Id, soonestStartDate, latestEndDate);
                        }
                    }

                    totalIncurredPaid = tasksInStage.Sum(task =>
                    {
                        if (task != null && task.Status != ProjectTaskStatus.Cancelled && task.IsIncurred == true)
                        {
                            decimal pricePerUnit = task.PricePerUnit;
                            double unitIncurred = task.UnitUsed - task.UnitInContract;
                            return pricePerUnit * (decimal)unitIncurred;
                        }
                        return 0;
                    });
                }

                stageService.UpdateStageEstimateBusinessDay(stage.Id, estimateBusinessDay);
                stageService.UpdateStageTotalIncurredPaid(stage.Id, totalIncurredPaid);
            }
        }

        public void UpdateTaskProgress(Guid taskId, double unitUsed)
        {
            var ct = _taskRepo.GetById(taskId) ?? throw new Exception("This project task id is not existed!");

            ct.UnitUsed = unitUsed;
            ct.Percentage = (int)((unitUsed / ct.UnitInContract) * 100);
            ct.UpdatedDate = TimeHelper.GetTime(DateTime.Now);

            if (unitUsed > ct.UnitInContract)
                ct.IsIncurred = true;

            _taskRepo.Update(ct);

            UpdateProjectData(ct.ProjectId);
            UpdatePaymentStageData(ct.ProjectId);
        }        
        
        public void UpdateTasksInDeletedStage(Guid stageId, Guid projectId)
        {
            var listTask = _taskRepo.GetByPaymentStageId(stageId);

            foreach (var task in listTask)
            {
                task.PaymentStageId = null;
                task.UpdatedDate = TimeHelper.GetTime(DateTime.Now);

                _taskRepo.Update(task);
            }

            UpdatePaymentStageData(projectId);
        }

        public DateTime? CalculateEndDate(DateTime? startDate, int businessDaysToAdd)
        {
            if (startDate == null)
            {
                return null;
            }
            else
            {
                DateTime endDate = startDate.GetValueOrDefault();

                for (int i = 0; i < businessDaysToAdd;)
                {
                    endDate = endDate.AddDays(1);

                    // Skip Sunday
                    if (endDate.DayOfWeek != DayOfWeek.Sunday)
                    {
                        i++;
                    }
                }

                return endDate;
            }
        }

        public ProjectTask? CreateProjectTask(ProjectTaskRequest request)
        {
            var ct = new ProjectTask
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                CalculationUnit = request.CalculationUnit,
                PricePerUnit = request.PricePerUnit,
                UnitInContract = request.UnitInContract,
                UnitUsed = 0,
                IsIncurred = request.IsIncurred,
                StartedDate = request.StartedDate,
                EndDate = CalculateEndDate(request.StartedDate, request.EstimateBusinessDay),
                CreatedDate = DateTime.Now,
                ProjectId = request.ProjectId,
                PaymentStageId = request.PaymentStageId,
                RoomId = request.RoomId,
                ParentTaskId= request.ParentTaskId,
                Status = ProjectTaskStatus.Pending,
                EstimateBusinessDay = request.EstimateBusinessDay,
                TaskCategoryId = request.TaskCategoryId
            };

            if (request.TaskDesignId != null)
            {
                ct.TaskDesignId = request.TaskDesignId;

                TaskDesignService taskDesignService = new (_taskDesignRepo, _taskCategoryRepo);
                var taskDesign = taskDesignService.GetById(ct.TaskDesignId.Value);

                ct.Code = taskDesign.Code;
            }
            else
            {
                var generateCode = GenerateCode(request.TaskCategoryId);

                ct.Code = generateCode;
            }

            var ctCreated = _taskRepo.Save(ct);

            UpdateProjectData(request.ProjectId);
            UpdatePaymentStageData(request.ProjectId);

            return ctCreated;
        }

        public void CreateTasksDecor(ProjectTaskRequest request)
        {
            var task = new ProjectTask
            {
                Id = Guid.NewGuid(),
                Code = "DECOR",
                Name = request.Name,
                Description = request.Description,
                CalculationUnit = request.CalculationUnit,
                PricePerUnit = request.PricePerUnit,
                UnitInContract = request.UnitInContract,
                UnitUsed = 0,
                IsIncurred = request.IsIncurred,
                StartedDate = request.StartedDate,
                EndDate = CalculateEndDate(request.StartedDate, request.EstimateBusinessDay),
                CreatedDate = DateTime.Now,
                ProjectId = request.ProjectId,
                PaymentStageId = request.PaymentStageId,
                RoomId = request.RoomId,
                Status = ProjectTaskStatus.Pending,
                EstimateBusinessDay = request.EstimateBusinessDay,
                TaskCategoryId = request.TaskCategoryId
            };

            _taskRepo.Save(task);

            UpdateProjectData(request.ProjectId);
            UpdatePaymentStageData(request.ProjectId);
        }

        public void UpdateDecorTask(Guid roomId, decimal pricePerArea, int estimateBusinessDay)
        {
            var decorTask = _taskRepo.GetByRoomId(roomId).FirstOrDefault(t => t.Code != null && t.Code.Equals("DECOR"));

            if (decorTask != null)
            {
                decorTask.PricePerUnit = pricePerArea;
                decorTask.EstimateBusinessDay = estimateBusinessDay;
                decorTask.EndDate = CalculateEndDate(decorTask.StartedDate, decorTask.EstimateBusinessDay);
                decorTask.UpdatedDate = TimeHelper.GetTime(DateTime.Now);

                _taskRepo.Update(decorTask);

                UpdateProjectData(decorTask.ProjectId);
                UpdatePaymentStageData(decorTask.ProjectId);
            }
        }

        public void CancelTasksInRoom(Guid roomId, Guid projectId)
        {
            var tasks = _taskRepo.GetByRoomId(roomId);

            foreach (var task in tasks)
            {
                task.Status = ProjectTaskStatus.Cancelled;
                task.UpdatedDate = TimeHelper.GetTime(DateTime.Now);
                task.RoomId = null;

                _taskRepo.Update(task);
            }

            UpdateProjectData(projectId);
            UpdatePaymentStageData(projectId);
        }

        public void AssignTasksToStage(Guid paymentStageId, List<Guid> listTaskId, Guid projectId)
        {
            foreach (var taskId in listTaskId)
            {
                var task = _taskRepo.GetById(taskId) ?? throw new Exception("This project task id is not existed!");

                task.PaymentStageId = paymentStageId;
                task.UpdatedDate = TimeHelper.GetTime(DateTime.Now);

                _taskRepo.Update(task);
            }
            UpdatePaymentStageData(projectId);
        }

        /*        public void StartTasksOfStage(Guid paymentStageId, Guid projectId)
                {
                    var listTask = _taskRepo.GetByPaymentStageId(paymentStageId);
                    if (listTask.Any())
                    {
                        foreach (var task in listTask)
                        {
                            if (task!= null && task.PaymentStageId == paymentStageId && task.Status == ProjectTaskStatus.Confirmed)
                            {
                                task.StartedDate = TimeHelper.GetTime(DateTime.Now);
                                _taskRepo.Update(task);
                            }
                        }
                    }

                    UpdatePaymentStageData(projectId);
                }*/

        public void UpdateProjectTask(Guid id, ProjectTaskRequest request)
        {
            var ct = _taskRepo.GetById(id) ?? throw new Exception("This project task id is not existed!");

            ct.Name = request.Name;
            ct.Description = request.Description;
            ct.CalculationUnit = request.CalculationUnit;
            ct.PricePerUnit = request.PricePerUnit;
            ct.UnitInContract = request.UnitInContract;
            ct.TaskCategoryId = request.TaskCategoryId;
            ct.IsIncurred = request.IsIncurred;
            ct.UpdatedDate= TimeHelper.GetTime(DateTime.Now);
            ct.StartedDate= request.StartedDate;
            ct.EndDate = CalculateEndDate(request.StartedDate, request.EstimateBusinessDay);
            ct.ProjectId = request.ProjectId;
            ct.PaymentStageId = request.PaymentStageId;
            ct.ParentTaskId= request.ParentTaskId;
            ct.RoomId = request.RoomId;
            ct.EstimateBusinessDay= request.EstimateBusinessDay;

            _taskRepo.Update(ct);

            UpdateProjectData(request.ProjectId);
            UpdatePaymentStageData(request.ProjectId);
        }
        public void UpdateProjectTaskStatus(Guid id, ProjectTaskStatus status)
        {
            var ct = _taskRepo.GetById(id) ?? throw new Exception("This project task id is not existed!");

            ct.Status = status;
            ct.UpdatedDate = TimeHelper.GetTime(DateTime.Now);

            if (status == ProjectTaskStatus.Ongoing)
            {
                ct.StartedDate= TimeHelper.GetTime(DateTime.Now);
            }

            if (status == ProjectTaskStatus.Done)
            {
                ct.EndDate = TimeHelper.GetTime(DateTime.Now);

                if (ct.UnitUsed != ct.UnitInContract)
                {
                    ct.IsIncurred = true;
                }
            }

            _taskRepo.Update(ct);

            UpdateProjectData(ct.ProjectId);
            UpdatePaymentStageData(ct.ProjectId);
        }

    }
}
