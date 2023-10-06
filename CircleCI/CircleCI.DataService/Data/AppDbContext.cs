using CircleCI.Entities.DbSet;
using Microsoft.EntityFrameworkCore;

namespace CircleCI.DataService.Data;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Token> Tokens { get; set; }
    public DbSet<Follow> Follows { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Like> Likes { get; set; }
    public DbSet<Saved> Saves { get; set; }
    public DbSet<CategoryList> CategoriesList { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        :base(options){}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasOne(e => e.Token)
            .WithOne(e => e.User)
            .HasForeignKey<Token>(e => e.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<User>()
            .HasMany(e => e.Saves)
            .WithOne(e => e.User)
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<User>()
            .HasMany(e => e.Followings)
            .WithOne(e => e.FollowerUser)
            .HasForeignKey(e => e.FollowerUserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<User>()
            .HasMany(e => e.Posts)
            .WithOne(e => e.User)
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<User>()
            .HasMany(e => e.Followers)
            .WithOne(e => e.FollowedUser)
            .HasForeignKey(e => e.FollowedUserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<User>()
            .HasMany(e => e.Comment)
            .WithOne(e => e.User)
            .HasForeignKey(e => e.UserId);

        modelBuilder.Entity<User>()
            .Ignore(u => u.Comment);
        
        modelBuilder.Entity<User>()
            .HasMany(e => e.Like)
            .WithOne(e => e.User)
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Post>()
            .HasMany(e => e.Category)
            .WithOne(e => e.Post)
            .HasForeignKey(e => e.PostId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Post>()
            .HasMany(e => e.Comment)
            .WithOne(e => e.Post)
            .HasForeignKey(e => e.PostId)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<Post>()
            .HasMany(e => e.Like)
            .WithOne(e => e.Post)
            .HasForeignKey(e => e.PostId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}