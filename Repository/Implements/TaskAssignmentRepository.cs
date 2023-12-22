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
    public class TaskAssignmentRepository : ITaskAssignmentRepository
    {
       
        public IEnumerable<TaskAssignment> GetAll()
        {
            try
            {
                using var context = new IdtDbContext();
                return context
                    .TaskAssignments
                    .OrderByDescending(a => a.CreatedDate)
                    .ToList();
            }
            catch
            {
                throw;
            }
        }

        public TaskAssignment? GetById(Guid id)
        {
            try
            {
                using var context = new IdtDbContext();
                return context.TaskAssignments.Where(ta => ta.Id == id).FirstOrDefault();
            }
            catch
            {
                throw;
            }
        }
        public IEnumerable<TaskAssignment?> GetByProjectId(Guid id)
        {
            try
            {
                using var context = new IdtDbContext();
                return context.TaskAssignments
                    .Include(ta => ta.ProjectParticipation)
                    .Where(ta => ta.ProjectParticipation.ProjectId == id)
                    .OrderByDescending(a => a.CreatedDate)
                    .ToList();
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<TaskAssignment?> GetByUserId(Guid id)
        {
            try
            {
                using var context = new IdtDbContext();
                return context.TaskAssignments
                    .Include(ta => ta.ProjectParticipation)
                    .Where(ta => ta.ProjectParticipation.UserId == id)
                    .OrderByDescending(a => a.CreatedDate)
                    .ToList();
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<TaskAssignment?> GetByTaskId(Guid id)
        {
            try
            {
                using var context = new IdtDbContext();
                return context.TaskAssignments
                    .Include(ta => ta.ProjectParticipation)
                    .Where(ta => ta.ProjectTaskId == id)
                    .OrderByDescending(a => a.CreatedDate)
                    .ToList();
            }
            catch
            {
                throw;
            }
        }

        public TaskAssignment? Save(TaskAssignment entity)
        {
            try
            {
                using var context = new IdtDbContext();
                var ta = context.TaskAssignments.Add(entity);
                context.SaveChanges();
                return ta.Entity;
            }
            catch
            {
                throw;
            }
        }

        public void Update(TaskAssignment entity)
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
                var ta = context.TaskAssignments.FirstOrDefault(a => a.Id == id);
                if (ta != null)
                {
                    context.TaskAssignments.Remove(ta);
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
