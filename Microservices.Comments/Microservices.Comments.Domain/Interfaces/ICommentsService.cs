using Microservices.Comments.Domain.DTOs;

namespace Microservices.Comments.Domain.Interfaces
{
    public interface ICommentsService
    {
        Task<CommentDto> CreateCommentAsync(Guid postId, CommentDto dto);
        Task<CommentDto> GetCommentByIdAsync(Guid id);
        Task<IEnumerable<CommentDto>> GetAllCommentsAsync();
        Task<IEnumerable<CommentDto>> GetCommentsByPostIdAsync(Guid postId);
        Task<CommentDto> UpdateCommentAsync(Guid id, CommentDto dto);
        Task<CommentDto> DeleteCommentAsync(Guid id);
    }

}
