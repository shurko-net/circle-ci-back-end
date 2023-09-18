using System.ComponentModel.DataAnnotations;

namespace CircleCI.Entities.DbSet;

public class User
{
    public int Id { get; set; }
    [StringLength(45)]
    public string Name { get; set; } = string.Empty;
    [StringLength(45)]
    public string Surname { get; set; } = string.Empty;
    [StringLength(45)]
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    [StringLength(12)]
    public string TNumber { get; set; } = string.Empty;
    public string Biography { get; set; } = string.Empty;
    public string ProfileImageUrl { get; set; } = string.Empty;
    public string BackgroundImageUrl { get; set; } = string.Empty;
    public int FollowersAmount { get; set; }
    public Token Token { get; set; } = null!;
    public ICollection<Follow> Followers { get; } = new List<Follow>();
    public ICollection<Saved> Saves { get; } = new List<Saved>();
    public ICollection<Post> Posts { get; } = new List<Post>();
    public ICollection<Follow> Followings { get; } = new List<Follow>();
    public ICollection<Comment> Comment { get; } = new List<Comment>();
    public ICollection<Like> Like { get; } = new List<Like>();
}