using Microservices.Posts.Domain.Entities;
using Microservices.Posts.Domain.Models;
using Microservices.Posts.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Microservices.Posts.SqlServerConnection.Repositories
{
    public class PostsRepository : IPostsRepository
    {
        private readonly ApplicationDbContext _context;

        public PostsRepository(ApplicationDbContext context)
        {
            _context = context;
            
        }

        public async Task<PostModel> CreatePostAsync(PostModel post)
        {
            PostEntity entity = new PostEntity
            {
                Title = post.Title,
                Content = post.Content,
                Author = post.Author,
            };

            await _context.Posts.AddAsync(entity);
            await _context.SaveChangesAsync();
            return post;
        }

        public async Task<PostModel> DeletePostAsync(Guid id)
        {
            var postEntity = await _context.Posts.FirstOrDefaultAsync(p => p.Id == id);
            if (postEntity == null)
            {
                throw new ArgumentException("Post not found.");
            }
            PostModel post = new PostModel
            {
                Title = postEntity.Title,
                Content = postEntity.Content,
                Author = postEntity.Author,
            };

            _context.Posts.Remove(postEntity);
            await _context.SaveChangesAsync();

            return post;
        }

        public async Task<IEnumerable<PostModel>> GetAllPostsAsync()
        {
            var posts = await _context.Posts
                .Select(p => new PostModel
                {
                    Title = p.Title,
                    Content = p.Content,
                    Author = p.Author,
                })
                .ToListAsync();

            return posts;
        }

        public async Task<PostModel> GetPostByIdAsync(Guid id)
        {
            var post = await _context.Posts.FirstOrDefaultAsync(p => p.Id == id);
            if (post == null)
            {
                throw new ArgumentException();
            }

            PostModel postModel = new PostModel
            {
                Title = post.Title,
                Content = post.Content,
                Author = post.Author,
            };
            return postModel;
        }

        public async Task<PostModel> UpdatePostAsync(Guid id, PostModel post)
        {
            var postEntity = await _context.Posts.FirstOrDefaultAsync(p => p.Id == id);
            if (postEntity == null)
            {
                throw new ArgumentException("Post not found.");
            }

            var verifiedEntity = UpdateVerifier(postEntity, post);

            _context.Posts.Update(verifiedEntity);
            await _context.SaveChangesAsync();
            return post;
        }

        private PostEntity UpdateVerifier(PostEntity entity, PostModel model)
        {
            if (model.Title != null && model.Title!=entity.Title)
                entity.Title = model.Title;
            if(model.Content != null && model.Content != entity.Content)
                entity.Content = model.Content;
            if (model.Author != null && model.Author != entity.Author)
                entity.Author = model.Author;
            return entity;
        }
    }
}
