using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using View.Comments;

namespace Blog.Controllers
{
    [ApiController]
    [Route("posts/{postId}/comments")]
    public sealed class CommentsController
    {
        [HttpPost("")]
        public async Task<ActionResult> CreateAsync(
            string postId,
            [FromBody] CommentCreateInfo createInfo,
            CancellationToken token)
        {
            throw new NotImplementedException();
        }

        [HttpPatch("{commentId}")]
        public async Task<ActionResult> UpdateAsync(
            string postId,
            string commentId,
            [FromBody] CommentUpdateInfo updateInfo,
            CancellationToken token)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{commentId}")]
        public async Task<ActionResult> DeleteAsync(
            string postId,
            string commentId,
            CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}
