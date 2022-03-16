using System;
using System.Collections.Generic;

namespace View.Posts
{
    public class PostShortInfo
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public IReadOnlyList<string> Tags { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
