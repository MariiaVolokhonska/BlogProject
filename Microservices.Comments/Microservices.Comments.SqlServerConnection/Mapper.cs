
using Microservices.Comments.Domain.Entities;
using Microservices.Comments.Domain.Models;

namespace Microservices.Comments.SqlServerConnection
{
    public static class Mapper
    {
        public static CommentModel MapToModel(CommentEntity entity)
        {
            return new CommentModel
            {
                PostId = entity.PostId,
                Content = entity.Content,
                Author = entity.Author
            };
        }
        public static CommentEntity MapToEntity(CommentModel model)
        {
            return new CommentEntity
            {
                PostId = model.PostId,
                Author = model.Author,
                Content = model.Content,
            };
        }
    }
}
