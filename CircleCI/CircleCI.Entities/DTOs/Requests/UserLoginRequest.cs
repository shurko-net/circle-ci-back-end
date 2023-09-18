using System.ComponentModel.DataAnnotations;

namespace CircleCI.Entities.DTOs.Requests;

public class UserLoginRequest
{
    [Required] public string Email { get; set; }
    [Required] public string Password { get; set; }
}