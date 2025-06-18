using Microservices.Comments.Domain.Models;

namespace Microservices.Comments.Domain.Interfaces
{
    public interface ICommentsRepository
    {
        Task<CommentModel> CreateCommentAsync(CommentModel model);

        Task<CommentModel> GetCommentByIdAsync(Guid id);

        Task<IEnumerable<CommentModel>> GetAllCommentsAsync();

        Task<IEnumerable<CommentModel>> GetCommentsByPostIdAsync(Guid postId);

        Task<CommentModel> UpdateCommentAsync(Guid id, CommentModel model);

        Task<CommentModel> DeleteCommentAsync(Guid id);
    }

}
