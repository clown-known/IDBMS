using BusinessObject.DTOs.Request;
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
        public Comment? CreateComment(CommentRequest comment)
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
                Status = comment.Status,
            };
            var cmtCreated = repository.Save(cmt);
            return cmtCreated;
        }
        public void UpdateComment(CommentRequest comment)
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
                Status = comment.Status,
            };
            repository.Update(cmt);
        }
    }
}
