using Microservices.Comments.Domain.DTOs;
using Microservices.Comments.Domain.Interfaces;
using Microservices.Comments.Domain.Models;
using Microservices.Comments.Infrastructure.Interfaces;

namespace Microservices.Comments.Core.Services
{
    public class CommentsService : ICommentsService
    {
        private readonly ICommentsRepository _repository;
        private readonly IPostResolver _postResolver;

        public CommentsService(ICommentsRepository repository, IPostResolver postResolver)
        {
            _repository = repository;
            _postResolver = postResolver;
        }

        public async Task<CommentDto> CreateCommentAsync(Guid postId, CommentDto dto)
        {
            if (!await _postResolver.PostExistsAsync(postId))
                throw new ArgumentException($"Post with ID {postId} does not exist.");

            var model = new CommentModel
            {
                PostId = postId,
                Author = dto.Author,
                Content = dto.Content
            };

            var created = await _repository.CreateCommentAsync(model);
            return MapToDto(created);
        }

        public async Task<CommentDto> GetCommentByIdAsync(Guid id)
        {
            var model = await _repository.GetCommentByIdAsync(id);
            return MapToDto(model);
        }

        public async Task<IEnumerable<CommentDto>> GetAllCommentsAsync()
        {
            var models = await _repository.GetAllCommentsAsync();
            return models.Select(MapToDto);
        }

        public async Task<IEnumerable<CommentDto>> GetCommentsByPostIdAsync(Guid postId)
        {
            var models = await _repository.GetCommentsByPostIdAsync(postId);
            return models.Select(MapToDto);
        }

        public async Task<CommentDto> UpdateCommentAsync(Guid id, CommentDto dto)
        {
            var updated = await _repository.UpdateCommentAsync(id, new CommentModel
            {
                Author = dto.Author,
                Content = dto.Content
            });
            return MapToDto(updated);
        }

        public async Task<CommentDto> DeleteCommentAsync(Guid id)
        {
            var deleted = await _repository.DeleteCommentAsync(id);
            return MapToDto(deleted);
        }

        private CommentDto MapToDto(CommentModel model) => new CommentDto
        {
            PostId = model.PostId,
            Author = model.Author,
            Content = model.Content
        };
    }

}
