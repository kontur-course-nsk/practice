using System;
using System.Collections.Generic;

namespace Model.Posts
{
    public sealed class Post
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public IReadOnlyList<string> Tags { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
