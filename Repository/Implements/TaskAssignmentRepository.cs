﻿using BusinessObject.Models;
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
                return context.TaskAssignments.ToList();
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
            throw new NotImplementedException();
        }

    }
}