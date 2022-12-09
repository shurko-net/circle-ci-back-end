using Microsoft.EntityFrameworkCore;

namespace CircleCI.Models
{
    public static class SeedData
    {
        public static void SeedDatabase(DataContext context)
        {
            context.Database.Migrate();
            if (context.Categories.Count() == 0)
            {
                
                Category c = new Category() { Name = "IT" };
                context.Categories.Add(c);
                context.SaveChanges();
            }
        }
    }
}
