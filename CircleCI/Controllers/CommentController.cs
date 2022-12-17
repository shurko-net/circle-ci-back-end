using CircleCI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            User? user = await context.Users.FindAsync(comment.IdUser);
            Post? post = await context.Posts.FindAsync(comment.IdPost);
            if (post == null && user == null)
            {
                return NotFound();
            }
            else
            {
                post!.User = await context.Users.FindAsync(post.IdUser);
                post!.Category = await context.Categories.FindAsync(post.IdCategory);
                Comment com = new()
                {
                    CommentContent = comment.CommentContent,
                    IdUser = user!.IdUser,
                    IdPost = post!.IdPost,
                    User = user,
                    Post = post
                };
                await context.Comments.AddAsync(com);
                await context.SaveChangesAsync();
                return Ok(com);
            }
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
