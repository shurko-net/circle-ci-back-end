using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace CircleCI.Entities.DTOs.Requests;

public class UploadPostImageRequest
{
    [Required] public int PostId { get; set; }
    [Required] public IFormFile File { get; set; }
}