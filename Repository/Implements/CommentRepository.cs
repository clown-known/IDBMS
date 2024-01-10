using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository.Implements
{
    public class CommentRepository : ICommentRepository
    {
        public IEnumerable<Comment> GetAll()
        {
            try
            {
                using var context = new IdtDbContext();
                return context.Comments
                    .Where(comment => comment.IsDeleted == false)
                    .OrderByDescending(comment => comment.LastModifiedTime ?? comment.CreatedTime)
                    .ToList();
            }
            catch
            {
                throw;
            }
        }

        public Comment? GetById(Guid id)
        {
            try
            {
                using var context = new IdtDbContext();
                return context.Comments.FirstOrDefault(comment => comment.Id == id && comment.IsDeleted == false);
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<Comment> GetByTaskId(Guid id)
        {
            try
            {
                using var context = new IdtDbContext();
                return context.Comments
                    .Include(t => t.CommentReplies.Where(rp => rp.IsDeleted == false))
                    .Where(comment => comment.ProjectTaskId == id && comment.IsDeleted == false && comment.ReplyCommentId == null)
                    .OrderByDescending(comment => comment.LastModifiedTime ?? comment.CreatedTime)
                    .ToList();
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<Comment> GetByProjectId(Guid id)
        {
            try
            {
                using var context = new IdtDbContext();
                return context.Comments
                    .Include(t => t.ProjectTask)
                    .Include(u => u.User)
                    .Include(t => t.CommentReplies.Where(rp => rp.IsDeleted == false))
                    .Where(comment => comment.ProjectId == id && comment.IsDeleted == false && comment.ReplyCommentId == null)
                    .OrderByDescending(comment => comment.LastModifiedTime ?? comment.CreatedTime)
                    .ToList();
            }
            catch
            {
                throw;
            }
        }

        public Comment Save(Comment entity)
        {
            try
            {
                using var context = new IdtDbContext();
                var comment = context.Comments.Add(entity);
                context.SaveChanges();
                return comment.Entity;
            }
            catch
            {
                throw;
            }
        }

        public void Update(Comment entity)
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
                var comment = context.Comments.FirstOrDefault(comment => comment.Id == id);
                if (comment != null)
                {
                    comment.IsDeleted = true;
                    context.Entry(comment).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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
