using Microservices.Comments.Core.Services;
using Microservices.Comments.Domain.DTOs;
using Microservices.Comments.Domain.Interfaces;
using Microservices.Comments.Domain.Models;
using Microservices.Comments.Infrastructure.Interfaces;
using Moq;
using Xunit;

namespace Microservices.Comments.UnitTests.CommentsServiceUnitTests
{
    public class CommentsServiceTests
    {
        private readonly Mock<ICommentsRepository> _repositoryMock;
        private readonly Mock<IPostResolver> _postResolverMock;
        private readonly CommentsService _service;

        public CommentsServiceTests()
        {
            _repositoryMock = new Mock<ICommentsRepository>();
            _postResolverMock = new Mock<IPostResolver>();
            _service = new CommentsService(_repositoryMock.Object, _postResolverMock.Object);
        }

        [Fact]
        public async Task CreateCommentAsync_ThrowsArgumentException_WhenPostDoesNotExist()
        {
            // Arrange
            var postId = Guid.NewGuid();
            var dto = new CommentDto { Author = "Author", Content = "Content", PostId = postId };

            _postResolverMock.Setup(p => p.PostExistsAsync(postId)).ReturnsAsync(false);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(() => _service.CreateCommentAsync(postId, dto));
            Assert.Contains("does not exist", ex.Message);
        }

        [Fact]
        public async Task CreateCommentAsync_ReturnsCommentDto_WhenPostExists()
        {
            // Arrange
            var postId = Guid.NewGuid();
            var dto = new CommentDto { Author = "Author", Content = "Content", PostId = postId };
            var model = new CommentModel { PostId = postId, Author = "Author", Content = "Content" };

            _postResolverMock.Setup(p => p.PostExistsAsync(postId)).ReturnsAsync(true);
            _repositoryMock.Setup(r => r.CreateCommentAsync(It.IsAny<CommentModel>())).ReturnsAsync(model);

            // Act
            var result = await _service.CreateCommentAsync(postId, dto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(dto.Author, result.Author);
            Assert.Equal(dto.Content, result.Content);
            Assert.Equal(dto.PostId, result.PostId);
        }

        [Fact]
        public async Task GetCommentByIdAsync_ReturnsCommentDto()
        {
            // Arrange
            var id = Guid.NewGuid();
            var model = new CommentModel { PostId = id, Author = "Author", Content = "Content" };

            _repositoryMock.Setup(r => r.GetCommentByIdAsync(id)).ReturnsAsync(model);

            // Act
            var result = await _service.GetCommentByIdAsync(id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(model.Author, result.Author);
        }

        [Fact]
        public async Task GetAllCommentsAsync_ReturnsListOfCommentDto()
        {
            // Arrange
            var models = new List<CommentModel>
        {
            new CommentModel { PostId = Guid.NewGuid(), Author = "Author1", Content = "Content1" },
            new CommentModel { PostId = Guid.NewGuid(), Author = "Author2", Content = "Content2" }
        };

            _repositoryMock.Setup(r => r.GetAllCommentsAsync()).ReturnsAsync(models);

            // Act
            var results = await _service.GetAllCommentsAsync();

            // Assert
            Assert.Equal(models.Count, results.Count());
        }

        [Fact]
        public async Task GetCommentsByPostIdAsync_ReturnsListOfCommentDto()
        {
            // Arrange
            var postId = Guid.NewGuid();
            var models = new List<CommentModel>
        {
            new CommentModel { PostId = postId, Author = "Author1", Content = "Content1" },
            new CommentModel { PostId = postId, Author = "Author2", Content = "Content2" }
        };

            _repositoryMock.Setup(r => r.GetCommentsByPostIdAsync(postId)).ReturnsAsync(models);

            // Act
            var results = await _service.GetCommentsByPostIdAsync(postId);

            // Assert
            Assert.All(results, r => Assert.Equal(postId, r.PostId));
        }

        [Fact]
        public async Task UpdateCommentAsync_ReturnsUpdatedCommentDto()
        {
            // Arrange
            var id = Guid.NewGuid();
            var dto = new CommentDto { Author = "Updated", Content = "Updated content" };
            var updatedModel = new CommentModel { PostId = id, Author = "Updated", Content = "Updated content" };

            _repositoryMock.Setup(r => r.UpdateCommentAsync(id, It.IsAny<CommentModel>())).ReturnsAsync(updatedModel);

            // Act
            var result = await _service.UpdateCommentAsync(id, dto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(dto.Author, result.Author);
            Assert.Equal(dto.Content, result.Content);
        }

        [Fact]
        public async Task DeleteCommentAsync_ReturnsDeletedCommentDto()
        {
            // Arrange
            var id = Guid.NewGuid();
            var deletedModel = new CommentModel { PostId = id, Author = "Author", Content = "Content" };

            _repositoryMock.Setup(r => r.DeleteCommentAsync(id)).ReturnsAsync(deletedModel);

            // Act
            var result = await _service.DeleteCommentAsync(id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(deletedModel.Author, result.Author);
        }
    }

}
