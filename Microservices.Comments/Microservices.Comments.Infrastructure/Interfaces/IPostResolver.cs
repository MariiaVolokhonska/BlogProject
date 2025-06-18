using Microservices.Comments.Domain.DTOs;

namespace Microservices.Comments.Infrastructure.Interfaces
{
    public interface IPostResolver
    {
        Task<PostDto> GetPostByIdAsync(Guid postId);

        Task<bool> PostExistsAsync(Guid postId);
    }

}
