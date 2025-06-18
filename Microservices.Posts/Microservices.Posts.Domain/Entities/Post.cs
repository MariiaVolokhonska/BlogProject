
namespace Microservices.Posts.Domain.Entities
{
    public class PostEntity
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string Author { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
