﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Posts.Domain.DTOs
{
    public class PostDto
    {
        public string? Title { get; set; }

        public string? Content { get; set; }

        public string? Author { get; set; }
    }
}
