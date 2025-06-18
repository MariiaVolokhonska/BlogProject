using Microservices.Posts.Domain.DTOs;

namespace Microservices.Posts.Domain.Interfaces
{
    public interface IPostsService
    {
        Task<PostDto> CreateAsync(PostDto dto);

        Task<PostDto> GetByIdAsync(Guid id);

        Task<IEnumerable<PostDto>> GetAllAsync();

        Task<PostDto> UpdateAsync(Guid id, PostDto dto);

        Task<PostDto> DeleteAsync(Guid id);
    }
}
