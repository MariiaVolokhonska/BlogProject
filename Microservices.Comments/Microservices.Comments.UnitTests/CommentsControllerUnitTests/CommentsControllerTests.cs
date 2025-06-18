using Microservices.Comments.API.Controllers;
using Microservices.Comments.Domain.DTOs;
using Microservices.Comments.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

public class CommentsControllerTests
{
    private readonly Mock<ICommentsService> _mockService;
    private readonly CommentsController _controller;

    public CommentsControllerTests()
    {
        _mockService = new Mock<ICommentsService>();
        _controller = new CommentsController(_mockService.Object);
    }

    [Fact]
    public async Task Create_ReturnsCreated_WhenValid()
    {
        // Arrange
        var postId = Guid.NewGuid();
        var dto = new CommentDto { Author = "Test", Content = "Valid content" };
        _mockService.Setup(s => s.CreateCommentAsync(postId, dto)).ReturnsAsync(dto);

        // Act
        var result = await _controller.Create(postId, dto);

        // Assert
        var created = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(201, created.StatusCode);
    }

    [Fact]
    public async Task Create_ReturnsBadRequest_WhenAuthorMissing()
    {
        // Arrange
        var postId = Guid.NewGuid();
        var dto = new CommentDto { Author = null, Content = "Test" };
        _mockService.Setup(s => s.CreateCommentAsync(postId, dto))
            .ThrowsAsync(new ArgumentException());

        // Act
        var result = await Record.ExceptionAsync(() => _controller.Create(postId, dto));

        // Assert
        Assert.IsType<ArgumentException>(result);
    }

    [Fact]
    public async Task Create_ReturnsNotFound_WhenPostNotFound()
    {
        // Arrange
        var postId = Guid.NewGuid();
        var dto = new CommentDto { Author = "Test", Content = "Test" };
        _mockService.Setup(s => s.CreateCommentAsync(postId, dto))
            .ThrowsAsync(new KeyNotFoundException());

        // Act
        var result = await Record.ExceptionAsync(() => _controller.Create(postId, dto));

        // Assert
        Assert.IsType<KeyNotFoundException>(result);
    }

    [Fact]
    public async Task GetAll_ReturnsOk()
    {
        // Arrange
        _mockService.Setup(s => s.GetAllCommentsAsync()).ReturnsAsync(new List<CommentDto>());

        // Act
        var result = await _controller.GetAll();

        // Assert
        var ok = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, ok.StatusCode);
    }

    [Fact]
    public async Task GetByPostId_ReturnsOk()
    {
        // Arrange
        var postId = Guid.NewGuid();
        _mockService.Setup(s => s.GetCommentsByPostIdAsync(postId)).ReturnsAsync(new List<CommentDto>());

        // Act
        var result = await _controller.GetByPostId(postId);

        // Assert
        var ok = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, ok.StatusCode);
    }

    [Fact]
    public async Task GetById_ReturnsOk()
    {
        // Arrange
        var id = Guid.NewGuid();
        _mockService.Setup(s => s.GetCommentByIdAsync(id)).ReturnsAsync(new CommentDto());

        // Act
        var result = await _controller.GetById(id);

        // Assert
        var ok = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, ok.StatusCode);
    }

    [Fact]
    public async Task GetById_ReturnsNotFound()
    {
        // Arrange
        var id = Guid.NewGuid();
        _mockService.Setup(s => s.GetCommentByIdAsync(id))
            .ThrowsAsync(new KeyNotFoundException());

        // Act
        var result = await Record.ExceptionAsync(() => _controller.GetById(id));

        // Assert
        Assert.IsType<KeyNotFoundException>(result);
    }

    [Fact]
    public async Task Update_ReturnsOk()
    {
        // Arrange
        var id = Guid.NewGuid();
        var dto = new CommentDto { Author = "Updated", Content = "Updated content" };
        _mockService.Setup(s => s.UpdateCommentAsync(id, dto)).ReturnsAsync(dto);

        // Act
        var result = await _controller.Update(id, dto);

        // Assert
        var ok = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, ok.StatusCode);
    }

    [Fact]
    public async Task Update_ReturnsNotFound()
    {
        // Arrange
        var id = Guid.NewGuid();
        var dto = new CommentDto();
        _mockService.Setup(s => s.UpdateCommentAsync(id, dto)).ThrowsAsync(new KeyNotFoundException());

        // Act
        var result = await Record.ExceptionAsync(() => _controller.Update(id, dto));

        // Assert
        Assert.IsType<KeyNotFoundException>(result);
    }

    [Fact]
    public async Task Delete_ReturnsOk()
    {
        // Arrange
        var id = Guid.NewGuid();
        _mockService.Setup(s => s.DeleteCommentAsync(id)).ReturnsAsync(new CommentDto());

        // Act
        var result = await _controller.Delete(id);

        // Assert
        var ok = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, ok.StatusCode);
    }

    [Fact]
    public async Task Delete_ReturnsNotFound()
    {
        // Arrange
        var id = Guid.NewGuid();
        _mockService.Setup(s => s.DeleteCommentAsync(id)).ThrowsAsync(new KeyNotFoundException());

        // Act
        var result = await Record.ExceptionAsync(() => _controller.Delete(id));

        // Assert
        Assert.IsType<KeyNotFoundException>(result);
    }
}

