using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microservices.Posts.Domain.Models;

namespace Microservices.Posts.Domain.Interfaces
{
    public interface IPostsRepository
    {
        public Task<IEnumerable<PostModel>> GetAllPostsAsync();
        public Task<PostModel> GetPostByIdAsync(Guid id);
        public Task<PostModel> CreatePostAsync(PostModel post);
        public Task<PostModel> UpdatePostAsync(Guid id, PostModel post);
        public Task<PostModel> DeletePostAsync(Guid id);
    }
}
