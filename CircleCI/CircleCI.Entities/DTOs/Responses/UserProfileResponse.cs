namespace CircleCI.Entities.DTOs.Responses;

public class UserProfileResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string ProfileImageUrl { get; set; } = string.Empty;
    public string BackgroundImageUrl { get; set; } = string.Empty;
    public string Biography { get; set; } = string.Empty;
    public int FollowersAmount { get; set; }
    public int CommentsAmount { get; set; }
    public int PostsAmount { get; set; }
    public bool IsMyself { get; set; }
    public bool IsFollowed { get; set; }
}