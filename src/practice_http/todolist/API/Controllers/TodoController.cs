namespace API.Controllers
{
    using Todo;
    using Model.Todo;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using ViewTodos = View.Todo;

    [Route("[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService todoService;

        public TodoController(ITodoService todoService)
        {
            this.todoService = todoService ?? throw new ArgumentNullException(nameof(todoService));
        }

        [HttpGet]
        public async Task<ActionResult> SearchTodoItems(
            [FromQuery] ViewTodos.TodoInfoSearchQuery query,
            CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            throw new NotImplementedException();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetAsync(string id, CancellationToken token)
        {
            try
            {
                var todoItem = await todoService.GetAsync(id, token).ConfigureAwait(false);
                return this.Ok(todoItem);
            }
            catch (TodoNotFoundException)
            {
                return this.NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateTodoAsync(
            [FromBody] ViewTodos.TodoBuildInfo buildInfo,
            CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            try
            {
                var todoInfo = await this.todoService.CreateAsync(buildInfo, token).ConfigureAwait(false);
                return this.Ok(todoInfo);
            }
            catch (ValidationException ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        [HttpPatch]
        [Route("{id}")]
        public async Task<IActionResult> PatchTodoAsync(
            string id,
            [FromBody] ViewTodos.TodoPatchInfo patchInfo,
            CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            throw new NotImplementedException();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id, CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}
