using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Model.Exceptions;
using Model.Posts;
using MongoDB.Driver;

namespace Model
{
    public sealed class PostsRepository : IPostsRepository
    {
        private readonly IMongoCollection<Post> postsCollection;

        public PostsRepository(IMongoCollection<Post> postsCollection)
        {
            this.postsCollection = postsCollection ?? throw new ArgumentNullException(nameof(postsCollection));
        }

        public async Task<Post> GetPostAsync(string id, CancellationToken token)
        {
            var job = await this.postsCollection
                .Find(it => it.Id == id)
                .FirstOrDefaultAsync(token)
                .ConfigureAwait(false);

            if (job is null)
            {
                throw new PostNotFoundException(id);
            }

            return job;
        }

        public async Task<PostsList> SearchPostsAsync(PostSearchInfo searchInfo, CancellationToken token)
        {
            var builder = Builders<Post>.Filter;
            var filter = builder.Empty;

            if (!string.IsNullOrEmpty(searchInfo.Tag))
            {
                filter &= builder.AnyEq(post => post.Tags, searchInfo.Tag);
            }

            if (searchInfo.FromCreatedAt != null)
            {
                filter &= builder.Gte(post => post.CreatedAt, searchInfo.FromCreatedAt);
            }

            if (searchInfo.ToCreatedAt != null)
            {
                filter &= builder.Lt(post => post.CreatedAt, searchInfo.ToCreatedAt);
            }

            var cursor = this.postsCollection.Find(filter);
            var posts = await cursor
                .Limit(searchInfo.Limit)
                .Skip(searchInfo.Offset)
                .ToListAsync(token);
            var total = (int)await cursor.CountDocumentsAsync(token);

            return new PostsList { Posts = posts, Total = total };
        }

        public async Task<Post> CreatePostAsync(PostCreateInfo createInfo, CancellationToken token)
        {
            var utcNow = DateTime.UtcNow;
            var post = new Post
            {
                Id = Guid.NewGuid().ToString(),
                Title = createInfo.Title,
                Text = createInfo.Text,
                Tags = createInfo.Tags,
                CreatedAt = createInfo.CreatedAt?.ToUniversalTime() ?? utcNow,
            };

            await this.postsCollection.InsertOneAsync(post, cancellationToken: token);

            return post;
        }

        public async Task UpdatePostAsync(string id, PostUpdateInfo updateInfo, CancellationToken token)
        {
            var updates = new List<UpdateDefinition<Post>>();

            if (updateInfo.Title != null)
            {
                updates.Add(Builders<Post>.Update.Set(p => p.Title, updateInfo.Title));
            }

            if (updateInfo.Text != null)
            {
                updates.Add(Builders<Post>.Update.Set(p => p.Text, updateInfo.Text));
            }

            if (updateInfo.Tags != null)
            {
                updates.Add(Builders<Post>.Update.Set(p => p.Tags, updateInfo.Tags));
            }

            var update = Builders<Post>.Update.Combine(updates);
            var updateResult = await this.postsCollection
                .UpdateOneAsync(it => it.Id == id, update, cancellationToken: token)
                .ConfigureAwait(false);

            if (updateResult.ModifiedCount == 0)
            {
                throw new PostNotFoundException(id);
            }
        }

        public async Task DeletePostAsync(string id, CancellationToken token)
        {
            var deleteResult = await this.postsCollection.DeleteOneAsync(it => it.Id == id, token);

            if (deleteResult.DeletedCount == 0)
            {
                throw new PostNotFoundException(id);
            }
        }
    }
}
