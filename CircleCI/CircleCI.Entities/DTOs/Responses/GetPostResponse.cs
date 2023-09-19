using CircleCI.Entities.DbSet;

namespace CircleCI.Entities.DTOs.Responses;

public class GetPostResponse
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public int LikesAmount { get; set; }
    public int ViewsAmount { get; set; }
    public int CommentsAmount { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public int UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string ProfileImageUrl { get; set; } = string.Empty;
    public bool IsPostOwner { get; set; }
    public bool IsMyself { get; set; }
    public bool IsLiked { get; set; }
    public bool IsSaved { get; set; }
    public ICollection<int> Category { get; set; }
}