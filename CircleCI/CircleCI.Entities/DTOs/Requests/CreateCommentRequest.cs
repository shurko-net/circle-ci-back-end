using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CircleCI.Entities.DTOs.Requests;

public class CreateCommentRequest
{
    [Required]public int PostId { get; set; }
    [Required] public string Content { get; set; } = string.Empty;
    [JsonIgnore] public int UserId { get; set; }
}