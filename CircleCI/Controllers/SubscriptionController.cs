using CircleCI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace CircleCI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        private DataContext context;
        public SubscriptionController(DataContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IAsyncEnumerable<Subscription> GetSubscriptions()
        {
            return context.Subscriptions.AsAsyncEnumerable();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSubscription(int id)
        {
            Subscription? s = await context.Subscriptions.FindAsync(id);
            if (s == null)
            {
                return NotFound();
            }
            return Ok(s);
        }

        [HttpPost]
        public async Task<IActionResult> SaveSubscription(Subscription subscription)
        {
            User? user = await context.Users.FindAsync(subscription.IdUser);
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                Subscription sub = new()
                {
                    IdUser = user.IdUser,
                    IdUserSub = subscription.IdUserSub,
                    User = user
                };
                await context.Subscriptions.AddAsync(sub);
                await context.SaveChangesAsync();
                return Ok(sub);
            }
        }
        [HttpPut]
        public async Task UpdateSubscription(Subscription subscription)
        {
            context.Update(subscription);
            await context.SaveChangesAsync();
        }

        [HttpDelete("{id}")]
        public async Task DeleteSubscription(int id)
        {
            context.Subscriptions.Remove(new Subscription() { IdSubscription = id });
            await context.SaveChangesAsync();
        }
    }
}
