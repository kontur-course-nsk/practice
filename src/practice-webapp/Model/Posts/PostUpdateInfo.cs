using System.Collections.Generic;

namespace Model.Posts
{
    public sealed class PostUpdateInfo
    {
        public string Title { get; set; }

        public string Text { get; set; }

        public IReadOnlyList<string> Tags { get; set; }
    }
}
