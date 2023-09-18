using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CircleCI.Entities.DTOs.Requests;

public class CreatePostRequest
{
    [Required] public string Title { get; set; } = string.Empty;
    [Required] public string Content { get; set; } = string.Empty;
    //[Required] public IFormFile ImageUrl { get; set; }
    [Required] public IEnumerable<string> Categories { get; set; } = new List<string>();
}