namespace Microservices.Comments.Domain.Models
{
    public class CommentModel
    {
        private Guid _postId;
        private string? _author;
        private string? _content;

        public Guid PostId
        {
            get => _postId;
            set
            {
                if (value == Guid.Empty)
                    throw new ArgumentException("PostId cannot be empty GUID.");
                _postId = value;
            }
        }

        public string? Author
        {
            get => _author;
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentException("Author length can't be more than 50 characters.");
                _author = value;
            }
        }

        public string? Content
        {
            get => _content;
            set
            {
                if (value != null && value.Length > 1000)
                    throw new ArgumentException("Content length can't be more than 1000 characters.");
                _content = value;
            }
        }
    }

}
