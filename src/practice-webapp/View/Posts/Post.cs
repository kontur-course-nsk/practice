using System.Collections.Generic;
using View.Comments;

namespace View.Posts
{
    public sealed class Post : PostShortInfo
    {
        public string Text { get; set; }

        public IReadOnlyList<Comment> Comments { get; set; }
    }
}
