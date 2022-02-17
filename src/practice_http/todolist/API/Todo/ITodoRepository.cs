namespace API.Todo
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using View.Todo;

    public interface ITodoRepository
    {
        Task<List<TodoInfo>> SearchAsync(TodoInfoSearchQuery query, CancellationToken token);

        Task<Todo> GetAsync(string id, CancellationToken token);

        Task<TodoInfo> CreateAsync(TodoBuildInfo buildInfo, CancellationToken token);

        Task<Todo> PatchAsync(string todoId, TodoPatchInfo patchInfo, CancellationToken token);

        Task RemoveAsync(string id, CancellationToken token);
    }
}