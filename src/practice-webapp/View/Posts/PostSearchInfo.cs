using System;

namespace View.Posts
{
    public sealed class PostSearchInfo
    {
        public string Tag { get; set; }

        public DateTime? FromCreatedAt { get; set; }

        public DateTime? ToCreatedAt { get; set; }

        public int? Limit { get; set; } = 100;

        public int? Offset { get; set; } = 0;
    }
}
