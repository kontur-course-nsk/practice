namespace API.Todo
{
    using Microsoft.Extensions.DependencyInjection;
    using Model.Todo.Repositories;

    internal static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTodos(this IServiceCollection services)
        {
            services.AddSingleton<ITodoService, TodoService>();
            services.AddSingleton<ITodoRepository, TodoRepository>();
            return services;
        }
    }
}
