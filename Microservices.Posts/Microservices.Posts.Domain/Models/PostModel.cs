namespace Microservices.Posts.Domain.Models
{
    public class PostModel
    {
        private string? _title;
        private string? _content;
        private string? _author;

        public string? Title
        {
            get => _title;
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentException("Title length can't be more than 100 characters.");
                _title = value;
            }
        }

        public string? Content
        {
            get => _content;
            set
            {
                _content = value;
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
    }



}
