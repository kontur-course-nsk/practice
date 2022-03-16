using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using Blog.Posts;
using Microsoft.AspNetCore.Mvc;
using Model.Exceptions;
using View.Posts;

namespace Blog.Controllers
{
    [ApiController]
    [Route("posts")]
    public class PostsController : ControllerBase
    {
        private readonly PostsService postsService;

        public PostsController(PostsService postsService)
        {
            this.postsService = postsService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetAsync(string id, CancellationToken token)
        {
            try
            {
                var post = await this.postsService.GetPostAsync(id, token).ConfigureAwait(false);
                return this.Ok(post);
            }
            catch (PostNotFoundException)
            {
                return this.NotFound();
            }
        }

        [HttpGet("")]
        public async Task<ActionResult> SearchAsync([FromQuery] PostSearchInfo searchInfo, CancellationToken token)
        {
            var postsList = await this.postsService.SearchPostsAsync(searchInfo, token).ConfigureAwait(false);
            return this.Ok(postsList);
        }

        [HttpPost("")]
        public async Task<ActionResult> CreateAsync([FromBody] PostCreateInfo createInfo, CancellationToken token)
        {
            try
            {
                var post = await this.postsService.CreatePostAsync(createInfo, token);
                return this.Ok(post);
            }
            catch (ValidationException ex)
            {
                return this.BadRequest(ex.ValidationResult);
            }
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdateAsync(
            string id,
            [FromBody] PostUpdateInfo updateInfo,
            CancellationToken token)
        {
            try
            {
                await this.postsService.UpdatePostAsync(id, updateInfo, token).ConfigureAwait(false);
                return this.NoContent();
            }
            catch (ValidationException ex)
            {
                return this.BadRequest(ex.ValidationResult);
            }
            catch (PostNotFoundException)
            {
                return this.NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(string id, CancellationToken token)
        {
            try
            {
                await this.postsService.DeletePostAsync(id, token).ConfigureAwait(false);
                return this.NoContent();
            }
            catch (PostNotFoundException)
            {
                return this.NotFound();
            }
        }
    }
}
