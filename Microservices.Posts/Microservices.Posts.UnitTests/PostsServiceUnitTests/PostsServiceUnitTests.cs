using Microservices.Posts.Core.Services;
using Microservices.Posts.Domain.DTOs;
using Microservices.Posts.Domain.Interfaces;
using Microservices.Posts.Domain.Models;
using Moq;
using Xunit;

namespace Microservices.Posts.UnitTests.PostsServiceUnitTests
{

    public class PostsServiceTests
    {
        private readonly Mock<IPostsRepository> _repositoryMock;
        private readonly PostsService _service;

        public PostsServiceTests()
        {
            _repositoryMock = new Mock<IPostsRepository>();
            _service = new PostsService(_repositoryMock.Object);
        }

        [Fact]
        public async Task CreateAsync_ValidData_ReturnsCreatedPost()
        {
            var dto = new PostDto { Title = "Title", Content = "Content", Author = "Author" };
            var model = new PostModel { Title = dto.Title, Content = dto.Content, Author = dto.Author };

            _repositoryMock
                .Setup(r => r.CreatePostAsync(It.IsAny<PostModel>()))
                .ReturnsAsync(model);

            var result = await _service.CreateAsync(dto);

            Assert.Equal(dto.Title, result.Title);
            Assert.Equal(dto.Content, result.Content);
            Assert.Equal(dto.Author, result.Author);
        }

        [Theory]
        [InlineData(null, "Content")]
        [InlineData(" ", "Content")]
        [InlineData("Title", null)]
        [InlineData("Title", " ")]
        public async Task CreateAsync_InvalidData_ThrowsArgumentException(string title, string content)
        {
            var dto = new PostDto { Title = title, Content = content };

            await Assert.ThrowsAsync<ArgumentException>(() => _service.CreateAsync(dto));
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsPost()
        {
            var id = Guid.NewGuid();
            var model = new PostModel { Title = "T", Content = "C", Author = "A" };

            _repositoryMock
                .Setup(r => r.GetPostByIdAsync(id))
                .ReturnsAsync(model);

            var result = await _service.GetByIdAsync(id);

            Assert.Equal("T", result.Title);
            Assert.Equal("C", result.Content);
            Assert.Equal("A", result.Author);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllPosts()
        {
            var models = new List<PostModel>
        {
            new PostModel { Title = "T1", Content = "C1", Author = "A1" },
            new PostModel { Title = "T2", Content = "C2", Author = "A2" }
        };

            _repositoryMock
                .Setup(r => r.GetAllPostsAsync())
                .ReturnsAsync(models);

            var results = (await _service.GetAllAsync()).ToList();

            Assert.Equal(2, results.Count);
            Assert.Equal("T1", results[0].Title);
            Assert.Equal("T2", results[1].Title);
        }

        [Fact]
        public async Task UpdateAsync_ValidData_ReturnsUpdatedPost()
        {
            var id = Guid.NewGuid();
            var dto = new PostDto { Title = "Updated", Content = "UpdatedContent", Author = "Author" };
            var updatedModel = new PostModel { Title = dto.Title, Content = dto.Content, Author = dto.Author };

            _repositoryMock
                .Setup(r => r.UpdatePostAsync(id, It.IsAny<PostModel>()))
                .ReturnsAsync(updatedModel);

            var result = await _service.UpdateAsync(id, dto);

            Assert.Equal("Updated", result.Title);
            Assert.Equal("UpdatedContent", result.Content);
        }

        [Fact]
        public async Task DeleteAsync_ReturnsDeletedPost()
        {
            var id = Guid.NewGuid();
            var deleted = new PostModel { Title = "T", Content = "C", Author = "A" };

            _repositoryMock
                .Setup(r => r.DeletePostAsync(id))
                .ReturnsAsync(deleted);

            var result = await _service.DeleteAsync(id);

            Assert.Equal("T", result.Title);
            Assert.Equal("C", result.Content);
            Assert.Equal("A", result.Author);
        }
    }

}
