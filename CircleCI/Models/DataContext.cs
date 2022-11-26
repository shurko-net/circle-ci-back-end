using Microsoft.EntityFrameworkCore;

namespace CircleCI.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> opts)
        :base(opts) { }

        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Comment> Comments => Set<Comment>();
        public DbSet<Post> Posts => Set<Post>();
        public DbSet<Subscription> Subscriptions => Set<Subscription>();
        public DbSet<User> Users => Set<User>();
    }
}
