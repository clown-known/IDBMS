using BusinessObject.Enums;
using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;

namespace Repository.Implements;

public class ProjectParticipationRepository : IProjectParticipationRepository
{

    public IEnumerable<ProjectParticipation> GetAll()
    {
        try
        {
            using var context = new IdtDbContext();
            return context.ProjectParticipations
                .ToList()
                .Reverse<ProjectParticipation>();
        }
        catch
        {
            throw;
        }
    }

    public ProjectParticipation? GetById(Guid id)
    {
        try
        {
            using var context = new IdtDbContext();
            return context.ProjectParticipations.FirstOrDefault(p => p.Id == id && p.IsDeleted == false);
        }
        catch
        {
            throw;
        }
    }

    public ProjectParticipation? GetProjectOwnerByProjectId(Guid id)
    {
        try
        {
            using var context = new IdtDbContext();
            return context.ProjectParticipations
                    .Include(u => u.User)
                    .FirstOrDefault(item => item.ProjectId == id && item.Role == ParticipationRole.ProductOwner && item.IsDeleted == false);
        }
        catch
        {
            throw;
        }
    }

    public ProjectParticipation? GetProjectManagerByProjectId(Guid id)
    {
        try
        {
            using var context = new IdtDbContext();
            return context.ProjectParticipations
                    .Include(u => u.User)
                    .FirstOrDefault(item => item.ProjectId == id && item.Role == ParticipationRole.ProjectManager && item.IsDeleted == false);
        }
        catch
        {
            throw;
        }
    }

    public ProjectParticipation Save(ProjectParticipation entity)
    {
        try
        {
            using var context = new IdtDbContext();
            var partiAdded = context.ProjectParticipations.Add(entity);
            context.SaveChanges();
            return partiAdded.Entity;
        }
        catch
        {
            throw;
        }
    }

    public void Update(ProjectParticipation entity)
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

    public IEnumerable<ProjectParticipation> GetByProjectId(Guid id)
    {
        try
        {
            using var context = new IdtDbContext();
            return context.ProjectParticipations
                .OrderBy(p => p.Role)
                .Include(u => u.User)
                .Where(u => u.ProjectId.Equals(id) && u.IsDeleted == false)
                .ToList();
        }
        catch
        {
            throw;
        }
    }

    public IEnumerable<ProjectParticipation> GetProjectMemberByProjectId(Guid id)
    {
        try
        {
            using var context = new IdtDbContext();
            return context.ProjectParticipations
                .OrderBy(p => p.Role)
                .Include(u => u.User)
                .Where(u => u.ProjectId.Equals(id) && u.IsDeleted == false 
                            && u.Role != ParticipationRole.ProjectManager && u.Role != ParticipationRole.ProductOwner)
                .ToList();
        }
        catch
        {
            throw;
        }
    }

    public IEnumerable<ProjectParticipation> GetUsersByParticipationInProject(Guid id)
    {
        try
        {
            using var context = new IdtDbContext();
            return context.ProjectParticipations
                .OrderBy(p => p.Role == ParticipationRole.ProductOwner || p.Role == ParticipationRole.ProjectManager ? 0 : p.Role)
                    .ThenBy(p => p.Role)
                .Include(u => u.User)
                .Where(u => u.ProjectId.Equals(id) && u.IsDeleted == false)
                .ToList();
        }
        catch
        {
            throw;
        }
    }
    
    public IEnumerable<ProjectParticipation> GetByUserId(Guid id)
    {
        try
        {
            using var context = new IdtDbContext();
            return context.ProjectParticipations
                .Where(u => u.UserId.Equals(id) && u.IsDeleted == false)
                .Include(p => p.Project)
                    .ThenInclude(pc => pc.ProjectCategory)
                .OrderByDescending(p => p.Project.UpdatedDate)
                .ToList();
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
            var entity = context.ProjectParticipations.Where(p => p.Id == id && p.IsDeleted == false).FirstOrDefault();
            if (entity != null)
            {
                entity.IsDeleted = true;
                context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
            }
        }
        catch
        {
            throw;
        }
    }
}
