using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Model;
using ViewPosts = View.Posts;
using ModelPosts = Model.Posts;

namespace Blog.Posts
{
    public sealed class PostsService
    {
        private readonly IPostsRepository postsRepository;
        private readonly IMapper mapper;

        public PostsService(IPostsRepository postsRepository, IMapper mapper)
        {
            this.postsRepository = postsRepository ?? throw new ArgumentNullException(nameof(postsRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ViewPosts.Post> GetPostAsync(string id, CancellationToken token)
        {
            var modelPost = await this.postsRepository.GetPostAsync(id, token);

            var viewPost = this.mapper.Map<ModelPosts.Post, ViewPosts.Post>(modelPost);
            return viewPost;
        }

        public async Task<ViewPosts.PostsList> SearchPostsAsync(
            ViewPosts.PostSearchInfo viewSearchInfo,
            CancellationToken token)
        {
            var modelSearchInfo = this.mapper.Map<ViewPosts.PostSearchInfo, ModelPosts.PostSearchInfo>(viewSearchInfo);
            var modelPostsList = await this.postsRepository.SearchPostsAsync(modelSearchInfo, token);

            var viewPostsList = this.mapper.Map<ModelPosts.PostsList, ViewPosts.PostsList>(modelPostsList);
            return viewPostsList;
        }

        public async Task<ViewPosts.Post> CreatePostAsync(
            ViewPosts.PostCreateInfo viewCreateInfo,
            CancellationToken token)
        {
            var modelCreateInfo = this.mapper.Map<ViewPosts.PostCreateInfo, ModelPosts.PostCreateInfo>(viewCreateInfo);
            ValidateOnCreate(modelCreateInfo);
            var modelPost = await this.postsRepository.CreatePostAsync(modelCreateInfo, token);

            var viewPost = this.mapper.Map<ModelPosts.Post, ViewPosts.Post>(modelPost);
            return viewPost;
        }

        public async Task UpdatePostAsync(string id, ViewPosts.PostUpdateInfo viewUpdateInfo, CancellationToken token)
        {
            var modelUpdateInfo = this.mapper.Map<ViewPosts.PostUpdateInfo, ModelPosts.PostUpdateInfo>(viewUpdateInfo);
            ValidateOnUpdate(modelUpdateInfo);
            await this.postsRepository.UpdatePostAsync(id, modelUpdateInfo, token);
        }

        public async Task DeletePostAsync(string id, CancellationToken token)
        {
            await this.postsRepository.DeletePostAsync(id, token);
        }

        private static void ValidateOnCreate(ModelPosts.PostCreateInfo createInfo)
        {
            if (createInfo.Tags?.Any(string.IsNullOrWhiteSpace) == true)
            {
                throw new ValidationException("Tag cannot be null or whitespace.");
            }

            if (createInfo.Tags?.Count > createInfo.Tags?.Distinct().Count())
            {
                throw new ValidationException("All tags must be different");
            }
        }

        private static void ValidateOnUpdate(ModelPosts.PostUpdateInfo updateInfo)
        {
            if (updateInfo.Title == null && updateInfo.Text == null && updateInfo.Tags == null)
            {
                throw new ValidationException("Update info is empty.");
            }

            if (updateInfo.Tags?.Any(string.IsNullOrWhiteSpace) == true)
            {
                throw new ValidationException("Tag cannot be null or whitespace.");
            }

            if (updateInfo.Tags?.Count > updateInfo.Tags?.Distinct().Count())
            {
                throw new ValidationException("All tags must be different");
            }
        }
    }
}
