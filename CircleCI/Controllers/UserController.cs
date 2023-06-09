﻿using CircleCI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Net.Mime;
using System.Text;

namespace CircleCI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
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
            List<Subscription> sub = await context.Subscriptions.ToListAsync();
            List<Post> posts = post.FindAll(p => p.IdUser == id);
            List<Comment> comments = comment.FindAll(p => p.IdUser == id);
            List<Subscription> subs = sub.FindAll(p => p.IdUserSub == id);
            comments ??= new List<Comment>();
            posts ??= new List<Post>();
            subs ??= new List<Subscription>();
            context.Posts.RemoveRange((IEnumerable<Post>)posts);
            context.Comments.RemoveRange((IEnumerable<Comment>)comments);
            context.Subscriptions.RemoveRange((IEnumerable<Subscription>)subs);
            context.Users.Remove(new User { IdUser = id });
            await context.SaveChangesAsync();
        }
    }
}
