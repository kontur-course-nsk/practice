using System.Collections.Generic;

namespace Model.Posts
{
    public sealed class PostsList
    {
        public IReadOnlyList<Post> Posts { get; set; }

        public int Total { get; set; }
    }
}
