namespace CircleCI.Entities.DTOs.Responses;

public class UserAuthResponse
{
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string ProfileImageUrl { get; set; } = string.Empty;
    public string BackgroundImageUrl { get; set; } = string.Empty;
    public string TNumber { get; set; } = string.Empty;
    public string Biography { get; set; } = string.Empty;
    public int FollowersAmount { get; set; }
}