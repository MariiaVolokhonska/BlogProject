using Microservices.Comments.Domain.DTOs;
using Microservices.Comments.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Microservices.Comments.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentsService _commentsService;

        public CommentsController(ICommentsService commentsService)
        {
            _commentsService = commentsService;
        }

        [HttpPost("{postId:guid}")]
        public async Task<IActionResult> Create(Guid postId, [FromBody] CommentDto dto)
        {
            var created = await _commentsService.CreateCommentAsync(postId, dto);
            return CreatedAtAction(nameof(GetById), new { id = postId }, created);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var comments = await _commentsService.GetAllCommentsAsync();
            return Ok(comments);
        }

        [HttpGet("post/{postId:guid}")]
        public async Task<IActionResult> GetByPostId(Guid postId)
        {
            var comments = await _commentsService.GetCommentsByPostIdAsync(postId);
            return Ok(comments);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var comment = await _commentsService.GetCommentByIdAsync(id);
            return Ok(comment);
        }

        [HttpPatch("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CommentDto dto)
        {
            var updated = await _commentsService.UpdateCommentAsync(id, dto);
            return Ok(updated);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _commentsService.DeleteCommentAsync(id);
            return Ok(deleted);
        }
    }

}
