using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CircleCI.Entities.DTOs.Requests;

public class CreatePostRequest
{
    [Required] public string Title { get; set; } = string.Empty;
    [Required] public string Content { get; set; } = string.Empty;
    public IFormFile? File { get; set; } = null;
    [Required] public string Categories { get; set; } = string.Empty;
}