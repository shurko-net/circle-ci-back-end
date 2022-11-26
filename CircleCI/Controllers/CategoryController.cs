using CircleCI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CircleCI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private DataContext context;
        public CategoryController(DataContext context)
        {
            this.context = context; 
        }

        [HttpGet]
        public IAsyncEnumerable<Category> GetCategories()
        {
            return context.Categories.AsAsyncEnumerable();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCategory(int id)
        {
            Category? c = await context.Categories.FindAsync(id);
            if (c == null)
            {
                return NotFound();
            }
            return Ok(c);
        }

        [HttpPost]
        public async Task<IActionResult> SaveCategory(Category category)
        {
            await context.Categories.AddAsync(category);
            await context.SaveChangesAsync();
            return Ok(category);
        }

        [HttpPut]
        public async Task UpdateCategory(Category category)
        {
            context.Update(category);
            await context.SaveChangesAsync();
        }

        [HttpDelete("{id}")]
        public async Task DeleteCategory(int id)
        {
            context.Categories.Remove(new Category() { IdCategory = id });
            await context.SaveChangesAsync();
        }
    }
}
