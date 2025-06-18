using Microservices.Posts.Domain.DTOs;
using Microservices.Posts.Domain.Interfaces;
using Microservices.Posts.Domain.Models;

namespace Microservices.Posts.Core.Services
{
    public class PostsService : IPostsService
    {
        private readonly IPostsRepository _repository;

        public PostsService(IPostsRepository repository)
        {
            _repository = repository;
        }

        public async Task<PostDto> CreateAsync(PostDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Title) || string.IsNullOrWhiteSpace(dto.Content))
                throw new ArgumentException("Title and Content are required.");

            var model = new PostModel
            {
                Title = dto.Title!,
                Content = dto.Content!,
                Author = dto.Author ?? "Anonymous"
            };

            var created = await _repository.CreatePostAsync(model);
            return MapToDto(created);
        }

        public async Task<PostDto> GetByIdAsync(Guid id)
        {
            var post = await _repository.GetPostByIdAsync(id);

            return MapToDto(post);
        }

        public async Task<IEnumerable<PostDto>> GetAllAsync()
        {
            var posts = await _repository.GetAllPostsAsync();
            return posts.Select(MapToDto);
        }

        public async Task<PostDto> UpdateAsync(Guid id, PostDto dto)
        {
            var model = new PostModel
            {
                Title = dto.Title,
                Content = dto.Content,
                Author = dto.Author
            };

            var updated = await _repository.UpdatePostAsync(id, model);
            return MapToDto(updated);
        }

        public async Task<PostDto> DeleteAsync(Guid id)
        {
            var deleted = await _repository.DeletePostAsync(id);
            return MapToDto(deleted);
        }

        private PostDto MapToDto(PostModel model) => new PostDto
        {
            Title = model.Title,
            Content = model.Content,
            Author = model.Author
        };
    }
}
