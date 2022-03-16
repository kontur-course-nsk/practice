using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Model;
using Model.Posts;
using MongoDB.Driver;

namespace Blog.Posts
{
    internal static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPosts(this IServiceCollection services)
        {
            services.AddSingleton(provider =>
            {
                var configuration = provider.GetRequiredService<IConfiguration>();
                var connectionString = configuration["ConnectionString"];
                var client = new MongoClient(connectionString);
                var database = client.GetDatabase("blog");
                var collection = database.GetCollection<Post>("posts");
                return collection;
            });
            services.AddSingleton<IPostsRepository, PostsRepository>();
            services.AddSingleton<PostsService>();

            return services;
        }
    }
}
