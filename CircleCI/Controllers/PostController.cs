using CircleCI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CircleCI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private DataContext context;
        public PostController(DataContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IAsyncEnumerable<Post> GetPosts()
        {
            return context.Posts.AsAsyncEnumerable();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPost(int id)
        {
            Post? p = await context.Posts.FindAsync(id);
            if (p == null)
            {
                return NotFound();
            }
            return Ok(p);
        }

        [HttpPost]
        public async Task<IActionResult> SavePost(Post post)
        {
            User? user = await context.Users.FindAsync(post.IdUser);
            Category? category = await context.Categories.FindAsync(post.IdCategory);
            if (user != null && category != null)
            {
                Post pt = new()
                {
                    IdUser = user!.IdUser,
                    IdCategory = category!.IdCategory,
                    Date = post.Date,
                    PostContent = post.PostContent,
                    Likes = post.Likes,
                    User = user,
                    Category = category
                };
                await context.Posts.AddAsync(pt);
                await context.SaveChangesAsync();
                return Ok(pt);
            }
            return NotFound();
        }
        [HttpPut]
        public async Task UpdatePost(Post post)
        {
            context.Update(post);
            await context.SaveChangesAsync();
        }

        [HttpDelete("{id}")]
        public async Task DeletePost(int id)
        {
            context.Posts.Remove(new Post() { IdPost = id });
            await context.SaveChangesAsync();
        }
    }
}
