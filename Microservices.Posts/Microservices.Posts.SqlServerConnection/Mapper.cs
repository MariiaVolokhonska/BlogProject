using Microservices.Posts.Domain.Entities;
using Microservices.Posts.Domain.Models;

namespace Microservices.Posts.SqlServerConnection
{
    public static class Mapper
    {
        public static PostModel MapToModel(PostEntity entity) => new PostModel
        {
            Title = entity.Title,
            Content = entity.Content,
            Author = entity.Author,
        };
        public static PostEntity MapToEntity(PostModel model) => new PostEntity
        {
            Title = model.Title,
            Content = model.Content,
            Author = model.Author,
        };
    }
}
