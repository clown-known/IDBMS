using BusinessObject.Enums;
using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implements
{
    public class ProjectTaskRepository : IProjectTaskRepository
    {
        public IEnumerable<ProjectTask> GetAll()
        {
            try
            {
                using var context = new IdtDbContext();
                return context.ProjectTasks
                    .Include(c => c.TaskCategory)
                    .Include(pt => pt.Comments.Where(cmt => cmt.IsDeleted == false))
                    .Include(pt => pt.TaskReports.Where(tr => tr.IsDeleted == false))
                    .Include(pt => pt.TaskAssignments)
                    .OrderByDescending(c => c.CreatedDate)
                    .ToList();
            }
            catch
            {
                throw;
            }
        }

        public ProjectTask? GetById(Guid id)
        {
            try
            {
                using var context = new IdtDbContext();
                return context.ProjectTasks
                    .Include(c => c.TaskCategory)
                    .Include(p => p.PaymentStage)
                    .Include(p => p.ParentTask)
                    .Include(r => r.Room)
                    .Include(pt => pt.TaskAssignments)
                        .ThenInclude(ta => ta.ProjectParticipation)
                            .ThenInclude(pp => pp.User)
                    .Include(pt => pt.Comments.Where(cmt => cmt.IsDeleted == false))
                    .Include(pt => pt.TaskReports.Where(tr => tr.IsDeleted == false))
                    .FirstOrDefault(task => task.Id == id);
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<ProjectTask> GetByProjectId(Guid id)
        {
            try
            {
                using var context = new IdtDbContext();
                return context.ProjectTasks
                    .AsNoTracking()
                    .Include(c => c.TaskCategory)
                    .Include(p => p.PaymentStage)
                    .Include(pt => pt.TaskAssignments)
                        .ThenInclude(ta => ta.ProjectParticipation)
                            .ThenInclude(pp => pp.User)
                    .Include(r => r.Room)
                        .ThenInclude(f => f.Floor)
                    .Where(task => task.ProjectId == id)
                    .OrderByDescending(c => c.CreatedDate)
                    .ToList();
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<ProjectTask> GetByRoomId(Guid id)
        {
            try
            {
                using var context = new IdtDbContext();
                return context.ProjectTasks
                    .Include(c => c.TaskCategory)
                    .Include(p => p.PaymentStage)
                    .Include(r => r.Room)
                        .ThenInclude(f => f.Floor)
                    .Include(pt => pt.Comments.Where(cmt => cmt.IsDeleted == false))
                    .Include(pt => pt.TaskReports.Where(tr => tr.IsDeleted == false))
                    .Where(task => task.RoomId == id)
                    .OrderByDescending(c => c.CreatedDate)
                    .ToList();
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<ProjectTask> GetByPaymentStageId(Guid id)
        {
            try
            {
                using var context = new IdtDbContext();
                return context.ProjectTasks
                    .Where(task => task.PaymentStageId != null && task.PaymentStageId == id)
                    .Include(pt => pt.Comments.Where(cmt => cmt.IsDeleted == false))
                    .Include(pt => pt.TaskReports.Where(tr => tr.IsDeleted == false))
                    .OrderByDescending(c => c.CreatedDate)
                    .ToList();
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<ProjectTask> GetOngoingTasks()
        {
            try
            {
                using var context = new IdtDbContext();
                return context.ProjectTasks
                    .Where(task => task.Status == ProjectTaskStatus.Ongoing)
                    .Include(pt => pt.Comments.Where(cmt => cmt.IsDeleted == false))
                    .Include(pt => pt.TaskReports.Where(tr => tr.IsDeleted == false))
                    .OrderByDescending(c => c.UpdatedDate ?? c.CreatedDate)
                    .ToList();
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<ProjectTask> GetOngoingTasksByUserId(Guid id)
        {
            try
            {
                using var context = new IdtDbContext();
                return context.ProjectTasks
                    .Include(pt => pt.TaskAssignments)
                        .ThenInclude(ta => ta.ProjectParticipation)
                    .Where(task => task.TaskAssignments.Any(ta => ta.ProjectParticipation.UserId == id) && 
                                    task.Status == ProjectTaskStatus.Ongoing)
                    .Include(pt => pt.Comments.Where(cmt => cmt.IsDeleted == false))
                    .Include(pt => pt.TaskReports.Where(tr => tr.IsDeleted == false))
                    .OrderByDescending(c => c.UpdatedDate ?? c.CreatedDate)
                    .ToList();
            }
            catch
            {
                throw;
            }
        }

        public bool CheckCodeExisted(string code)
        {
            try
            {
                using var context = new IdtDbContext();

                bool exists = context.ProjectTasks.Any(task => task.Code.ToLower() == code.ToLower());

                return exists;
            }
            catch
            {
                throw;
            }
        }

        public ProjectTask Save(ProjectTask entity)
        {
            try
            {
                using var context = new IdtDbContext();
                var task = context.ProjectTasks.Add(entity);
                context.SaveChanges();
                return task.Entity;
            }
            catch
            {
                throw;
            }
        }

        public void Update(ProjectTask entity)
        {
            try
            {
                using var context = new IdtDbContext();
                context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public void DeleteById(Guid id)
        {
            try
            {
                using var context = new IdtDbContext();
                var task = context.ProjectTasks.FirstOrDefault(task => task.Id == id);
                if (task != null)
                {
                    context.ProjectTasks.Remove(task);
                    context.SaveChanges();
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
