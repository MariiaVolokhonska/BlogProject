using Microservices.Posts.Domain.DTOs;
using Microservices.Posts.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Microservices.Posts.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly IPostsService _service;

        public PostsController(IPostsService service)
        {
            _service = service;
        }

        // GET: api/posts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostDto>>> GetAll()
        {
            var posts = await _service.GetAllAsync();
            return Ok(posts);
        }

        // GET: api/posts/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<PostDto>> GetById(Guid id)
        {
            try
            {
                var post = await _service.GetByIdAsync(id);
                return Ok(post);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // POST: api/posts
        [HttpPost]
        public async Task<ActionResult<PostDto>> Create([FromBody] PostDto dto)
        {
            try
            {
                var created = await _service.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = Guid.NewGuid() }, created);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }

        // PATCH: api/posts/{id}
        [HttpPatch("{id}")]
        public async Task<ActionResult<PostDto>> Update(Guid id, [FromBody] PostDto dto)
        {
            try
            {
                var updated = await _service.UpdateAsync(id, dto);
                return Ok(updated);
            }
            catch (ArgumentException e)
            {
                return NotFound(e.Message);
            }
        }

        // DELETE: api/posts/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<PostDto>> Delete(Guid id)
        {
            try
            {
                var deleted = await _service.DeleteAsync(id);
                return Ok(deleted);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
        }
    }
}
