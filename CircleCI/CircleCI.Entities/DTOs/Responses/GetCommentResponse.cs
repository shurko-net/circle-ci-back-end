namespace CircleCI.Entities.DTOs.Responses;

public class GetCommentResponse
{
    public int Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public int UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string ProfileImageUrl { get; set; } = string.Empty;
}