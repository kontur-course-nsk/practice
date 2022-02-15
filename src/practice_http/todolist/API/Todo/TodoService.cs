namespace API.Todo
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using Model.Todo.Repositories;
    using ViewTodos = View.Todo;
    using ModelTodos = Model.Todo;

    public sealed class TodoService : ITodoService
    {
        private readonly ITodoRepository todoRepository;
        private readonly IMapper mapper;

        public TodoService(ITodoRepository todoRepository, IMapper mapper)
        {
            this.todoRepository = todoRepository ?? throw new ArgumentNullException(nameof(todoRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public Task<ViewTodos.TodoList> SearchAsync(ViewTodos.TodoInfoSearchQuery query, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public async Task<ViewTodos.Todo> GetAsync(string id, CancellationToken token)
        {
            var modelTodoInfo = await todoRepository.GetAsync(id, token).ConfigureAwait(false);
            var viewTodoInfo = this.mapper.Map<ModelTodos.Todo, ViewTodos.Todo>(modelTodoInfo);
            return viewTodoInfo;
        }

        public async Task<ViewTodos.TodoInfo> CreateAsync(ViewTodos.TodoBuildInfo buildInfo, CancellationToken token)
        {
            if (buildInfo.Deadline <= DateTime.Now)
            {
                throw new ValidationException($"Invalid date of {buildInfo.Deadline}.");
            }

            var modelBuildInfo = this.mapper.Map<ViewTodos.TodoBuildInfo, ModelTodos.TodoBuildInfo>(buildInfo);

            var modelTodoInfo = await todoRepository.CreateAsync(modelBuildInfo, token).ConfigureAwait(false);

            var viewTodoInfo = this.mapper.Map<ModelTodos.TodoInfo, ViewTodos.TodoInfo>(modelTodoInfo);
            return viewTodoInfo;
        }

        public Task<ViewTodos.Todo> PatchAsync(ViewTodos.TodoPatchInfo patchInfo, CancellationToken token)
        {
            throw new System.NotImplementedException();
        }

        public Task RemoveAsync(string id, CancellationToken token)
        {
            throw new System.NotImplementedException();
        }
    }
}
