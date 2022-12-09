using CircleCI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace CircleCI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private DataContext context;
        public UserController(DataContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IAsyncEnumerable<User> GetUsers()
        {
            return context.Users.AsAsyncEnumerable();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUser(int id)
        {
            User? u = await context.Users.FindAsync(id);
            if (u == null)
            {
                return NotFound();
            }
            return Ok(u);
        }

        [HttpPost]
        public async Task<IActionResult> SaveUser(User user)
        {
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
            return Ok(user);
        }

        [HttpPut]
        public async Task UpdateUser(User user)
        {
            context.Update(user);
            await context.SaveChangesAsync();
        }

        [HttpDelete("{id}")]
        public async Task DeleteUser(int id)
        {
            List<Post> post = await context.Posts.ToListAsync();
            List<Comment> comment = await context.Comments.ToListAsync();
            List<Post> posts = post.FindAll(p => p.IdUser == id);
            List<Comment> comments = comment.FindAll(p => p.IdUser == id);
            comments ??= new List<Comment>();
            posts ??= new List<Post>();
            context.Posts.RemoveRange((IEnumerable<Post>)posts);
            context.Comments.RemoveRange((IEnumerable<Comment>)comments);
            context.Users.Remove(new User { IdUser = id });
            await context.SaveChangesAsync();
        }
    }
}
