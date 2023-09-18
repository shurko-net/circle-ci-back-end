namespace CircleCI.Entities.DTOs.Requests;

public class CreateCategoryRequest
{
    public required int PostId { get; set; }
    public required List<string> CategoryNames { get; set; }
}