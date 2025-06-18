namespace Microservices.Comments.Domain.DTOs
{
    public class CommentDto
    {
        public Guid PostId { get; set; }

        public string? Author { get; set; }

        public string? Content { get; set; }

    }
}
