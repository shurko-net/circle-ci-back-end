using CircleCI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CircleCI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private DataContext context;
        public CommentController(DataContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IAsyncEnumerable<Comment> GetComments()
        {
            return context.Comments.AsAsyncEnumerable();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetComment(int id)
        {
            Comment? c = await context.Comments.FindAsync(id);
            if (c == null)
            {
                return NotFound();
            }
            return Ok(c);
        }

        [HttpPost]
        public async Task<IActionResult> SaveComment(Comment comment)
        {
            await context.Comments.AddAsync(comment);
            await context.SaveChangesAsync();
            return Ok(comment);
        }

        [HttpPut]
        public async Task UpdateComment(Comment comment)
        {
            context.Update(comment);
            await context.SaveChangesAsync();
        }

        [HttpDelete("{id}")]
        public async Task DeleteComment(int id)
        {
            context.Comments.Remove(new Comment() { IdComment = id });
            await context.SaveChangesAsync();
        }
    }
}
