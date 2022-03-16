using System.Collections.Generic;

namespace View.Posts
{
    public sealed class PostsList
    {
        public IReadOnlyList<PostShortInfo> Posts { get; set; }

        public int Total { get; set; }
    }
}
