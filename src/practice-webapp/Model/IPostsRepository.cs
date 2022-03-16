using System.Threading;
using System.Threading.Tasks;
using Model.Posts;

namespace Model
{
    public interface IPostsRepository
    {
        public Task<Post> GetPostAsync(string id, CancellationToken token);

        public Task<PostsList> SearchPostsAsync(PostSearchInfo searchInfo, CancellationToken token);

        public Task<Post> CreatePostAsync(PostCreateInfo createInfo, CancellationToken token);

        public Task UpdatePostAsync(string id, PostUpdateInfo updateInfo, CancellationToken token);

        public Task DeletePostAsync(string id, CancellationToken token);
    }
}
