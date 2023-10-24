using BusinessObject.DTOs.Request.CreateRequests;
using BusinessObject.Models;
using Repository.Interfaces;

namespace IDBMS_API.Services
{
    public class CommentService
    {
        private readonly ICommentRepository repository;
        public CommentService(ICommentRepository repository)
        {
            this.repository = repository;
        }   
        public IEnumerable<Comment> GetAll()
        {
            return repository.GetAll();
        }
        public Comment? GetById(Guid id)
        {
            return repository.GetById(id);
        }
        public async Task<Comment?> CreateComment(CommentRequest comment)
        {
            var cmt = new Comment
            {
                Content = comment.Content,
                ConstructionTaskId = comment.ConstructionTaskId,
                DecorProgressReportId = comment.ConstructionTaskId,
                CreatedDate = comment.CreatedDate,
                UserId = comment.UserId,
                FileUrl = comment.FileUrl,
                CreatedTime = comment.CreatedTime,
                LastModifiedTime = comment.LastModifiedTime,
                IsDeleted = comment.IsDeleted,
            };
            var cmtCreated = repository.Save(cmt);
            return cmtCreated;
        }
        public async Task UpdateComment(CommentRequest comment)
        {
            var cmt = new Comment
            {
                Content = comment.Content,
                ConstructionTaskId = comment.ConstructionTaskId,
                DecorProgressReportId = comment.ConstructionTaskId,
                CreatedDate = comment.CreatedDate,
                UserId = comment.UserId,
                FileUrl = comment.FileUrl,
                CreatedTime = comment.CreatedTime,
                LastModifiedTime = comment.LastModifiedTime,
                IsDeleted = comment.IsDeleted,
            };
            repository.Update(cmt);
        }
    }
}
