using System;

namespace Model.Posts
{
    public sealed class PostSearchInfo
    {
        public string Tag { get; set; }

        public DateTime? FromCreatedAt { get; set; }

        public DateTime? ToCreatedAt { get; set; }

        public int? Limit { get; set; }

        public int? Offset { get; set; }
    }
}
