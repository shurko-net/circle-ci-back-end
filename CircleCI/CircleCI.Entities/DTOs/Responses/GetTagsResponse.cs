namespace CircleCI.Entities.DTOs.Responses;

public class GetTagsResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
}