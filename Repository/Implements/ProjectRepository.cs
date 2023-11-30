using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository.Implements
{
    public class ProjectRepository : IProjectRepository
    {
        public IEnumerable<Project> GetAll()
        {
            try
            {
                using var context = new IdtDbContext();
                return context.Projects.ToList();
            }
            catch
            {
                throw;
            }
        }

        public Project? GetById(Guid id)
        {
            try
            {
                using var context = new IdtDbContext();
                return context.Projects
                    .Include(pc => pc.ProjectCategory)
                    .Include(par => par.ProjectParticipations)
                        .ThenInclude(u => u.User)
                    .FirstOrDefault(project => project.Id == id);
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<Project> GetBySiteId(Guid id)
        {
            try
            {
                using var context = new IdtDbContext();
                return context.Projects.Where(p => p.SiteId == id).ToList();
            }
            catch
            {
                throw;
            }
        }

        public Project Save(Project entity)
        {
            try
            {
                using var context = new IdtDbContext();
                var project = context.Projects.Add(entity);
                context.SaveChanges();
                return project.Entity;
            }
            catch
            {
                throw;
            }
        }

        public void Update(Project entity)
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
                var project = context.Projects.FirstOrDefault(project => project.Id == id);
                if (project != null)
                {
                    context.Projects.Remove(project);
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
