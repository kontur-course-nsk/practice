namespace API.Todo
{
    using System.Threading;
    using System.Threading.Tasks;
    using View.Todo;

    public interface ITodoService
    {
        Task<TodoList> SearchAsync(TodoInfoSearchQuery query, CancellationToken token);

        Task<Todo> GetAsync(string id, CancellationToken token);

        Task<TodoInfo> CreateAsync(TodoBuildInfo buildInfo, CancellationToken token);

        Task<Todo> PatchAsync(TodoPatchInfo patchInfo, CancellationToken token);

        Task RemoveAsync(string id, CancellationToken token);
    }
}
