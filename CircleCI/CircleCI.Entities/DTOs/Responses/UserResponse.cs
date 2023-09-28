namespace CircleCI.Entities.DTOs.Responses;

public class UserResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string ProfileImageUrl { get; set; } = string.Empty;
    public string BackgroundImageUrl { get; set; } = string.Empty;
    public string Biography { get; set; } = string.Empty;
    public int FollowersAmount { get; set; }
}