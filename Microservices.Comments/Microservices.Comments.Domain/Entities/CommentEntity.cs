namespace Microservices.Comments.Domain.Entities
{
    public class CommentEntity
    {
        public Guid Id { get; set; }

        public Guid PostId { get; set; }

        public string Author { get; set; }

        public string Content { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
