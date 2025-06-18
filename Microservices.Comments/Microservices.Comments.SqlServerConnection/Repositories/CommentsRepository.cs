using Microservices.Comments.Domain.Entities;
using Microservices.Comments.Domain.Interfaces;
using Microservices.Comments.Domain.Models;
using Microsoft.EntityFrameworkCore;


namespace Microservices.Comments.SqlServerConnection.Repositories
{
    public class CommentsRepository : ICommentsRepository
    {
        private readonly ApplicationDbContext _context;

        public CommentsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CommentModel> CreateCommentAsync(CommentModel model)
        {
            var entity = Mapper.MapToEntity(model);

            await _context.Comments.AddAsync(entity);
            await _context.SaveChangesAsync();

            return model;
        }

        public async Task<CommentModel> GetCommentByIdAsync(Guid id)
        {
            var entity = await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);
            if (entity == null)
                throw new KeyNotFoundException($"Comment with ID {id} not found.");

            return Mapper.MapToModel(entity);
        }

        public async Task<IEnumerable<CommentModel>> GetAllCommentsAsync()
        {
            var entities = await _context.Comments.ToListAsync();
            return entities.Select(Mapper.MapToModel);
        }

        public async Task<IEnumerable<CommentModel>> GetCommentsByPostIdAsync(Guid postId)
        {
            var entities = await _context.Comments
                .Where(c => c.PostId == postId)
                .ToListAsync();

            return entities.Select(Mapper.MapToModel);
        }

        public async Task<CommentModel> UpdateCommentAsync(Guid id, CommentModel model)
        {
            var entity = await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);
            if (entity == null)
                throw new KeyNotFoundException($"Comment with ID {id} not found.");

            entity.Content = model.Content ?? entity.Content;
            entity.Author = model.Author ?? entity.Author;

            _context.Comments.Update(entity);
            await _context.SaveChangesAsync();

            return Mapper.MapToModel(entity);
        }

        public async Task<CommentModel> DeleteCommentAsync(Guid id)
        {
            var entity = await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);
            if (entity == null)
                throw new KeyNotFoundException($"Comment with ID {id} not found.");

            _context.Comments.Remove(entity);
            await _context.SaveChangesAsync();

            return Mapper.MapToModel(entity);
        }

       
    }

}
