using Microservices.Posts.API.Controllers;
using Microservices.Posts.Domain.DTOs;
using Microservices.Posts.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
namespace Microservices.Posts.UnitTests.PostsControllerUnitTests
{
    public class PostsControllerTests
    {
        private readonly Mock<IPostsService> _mockService;
        private readonly PostsController _controller;

        public PostsControllerTests()
        {
            _mockService = new Mock<IPostsService>();
            _controller = new PostsController(_mockService.Object);
        }

        [Fact]
        public async Task Create_ValidPost_ReturnsCreated()
        {
            var postDto = new PostDto { Title = "Valid Title", Content = "Valid content" };
            _mockService.Setup(s => s.CreateAsync(postDto)).ReturnsAsync(postDto);

            var result = await _controller.Create(postDto);

            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal(postDto, createdResult.Value);
        }

        [Fact]
        public async Task Create_WithoutTitle_ReturnsBadRequest()
        {
            var postDto = new PostDto { Title = "", Content = "Content" };
            _mockService.Setup(s => s.CreateAsync(postDto))
                        .ThrowsAsync(new ArgumentException("Title is required"));

            var result = await _controller.Create(postDto);

            var badRequest = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("Title is required", badRequest.Value);
        }

        [Fact]
        public async Task Create_TitleTooLong_ReturnsBadRequest()
        {
            var postDto = new PostDto { Title = new string('A', 101), Content = "Content" };
            _mockService.Setup(s => s.CreateAsync(postDto))
                        .ThrowsAsync(new ArgumentException("Title cannot exceed 100 characters"));

            var result = await _controller.Create(postDto);

            var badRequest = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("Title cannot exceed 100 characters", badRequest.Value);
        }

        [Fact]
        public async Task GetAll_ReturnsOkWithPosts()
        {
            var posts = new List<PostDto> { new PostDto { Title = "T1", Content = "C1" } };
            _mockService.Setup(s => s.GetAllAsync()).ReturnsAsync(posts);

            var result = await _controller.GetAll();

            var ok = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(posts, ok.Value);
        }

        [Fact]
        public async Task GetById_ExistingId_ReturnsPost()
        {
            var postId = Guid.NewGuid();
            var post = new PostDto { Title = "T1", Content = "C1" };
            _mockService.Setup(s => s.GetByIdAsync(postId)).ReturnsAsync(post);

            var result = await _controller.GetById(postId);

            var ok = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(post, ok.Value);
        }

        [Fact]
        public async Task GetById_NotFound_ReturnsNotFound()
        {
            var postId = Guid.NewGuid();
            _mockService.Setup(s => s.GetByIdAsync(postId)).ThrowsAsync(new KeyNotFoundException());

            var result = await _controller.GetById(postId);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task Update_ValidId_ReturnsOk()
        {
            var postId = Guid.NewGuid();
            var dto = new PostDto { Title = "Updated", Content = "Updated content" };
            _mockService.Setup(s => s.UpdateAsync(postId, dto)).ReturnsAsync(dto);

            var result = await _controller.Update(postId, dto);

            var ok = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(dto, ok.Value);
        }

        [Fact]
        public async Task Update_NotFound_ReturnsNotFound()
        {
            var postId = Guid.NewGuid();
            var dto = new PostDto { Title = "", Content = "" };
            _mockService.Setup(s => s.UpdateAsync(postId, dto))
                        .ThrowsAsync(new ArgumentException("Post not found"));

            var result = await _controller.Update(postId, dto);

            var notFound = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.Equal("Post not found", notFound.Value);
        }

        [Fact]
        public async Task Delete_ExistingId_ReturnsDeletedPost()
        {
            var postId = Guid.NewGuid();
            var deleted = new PostDto { Title = "T1", Content = "C1" };
            _mockService.Setup(s => s.DeleteAsync(postId)).ReturnsAsync(deleted);

            var result = await _controller.Delete(postId);

            var ok = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(deleted, ok.Value);
        }

        [Fact]
        public async Task Delete_NotFound_ReturnsNotFound()
        {
            var postId = Guid.NewGuid();
            _mockService.Setup(s => s.DeleteAsync(postId)).ThrowsAsync(new ArgumentException());

            var result = await _controller.Delete(postId);

            Assert.IsType<NotFoundResult>(result.Result);
        }
    }
}
