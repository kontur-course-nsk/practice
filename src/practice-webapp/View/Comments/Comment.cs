using System;

namespace View.Comments
{
    public sealed class Comment
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Text { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
