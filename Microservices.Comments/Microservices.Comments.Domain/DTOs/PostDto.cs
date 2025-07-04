﻿namespace Microservices.Comments.Domain.DTOs
{
    public class PostDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = default!;

        public string Content { get; set; } = default!;

        public string Author { get; set; } = default!;
    }

}
