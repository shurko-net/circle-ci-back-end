namespace CircleCI.Entities.DbSet;

public class Post
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public int LikesAmount { get; set; }
    public int ViewsAmount { get; set; }
    public string ImageUrl { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public Saved Saves { get; set; } = null!;
    public ICollection<Category> Category { get; set; }
    public ICollection<Comment> Comment { get; set; }
    public ICollection<Like> Like { get; set; }
}