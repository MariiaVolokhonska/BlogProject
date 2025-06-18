namespace Microservices.Comments.Domain.Models
{
    public class CommentModel
    {
        public Guid PostId { get; set; }

        public string? Author { get; set; }

        public string? Content { get; set; }
    }
}
