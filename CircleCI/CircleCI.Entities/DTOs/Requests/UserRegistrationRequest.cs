using System.ComponentModel.DataAnnotations;

namespace CircleCI.Entities.DTOs.Requests;

public class UserRegistrationRequest
{
    [Required] public string Name { get; set; }
    [Required] public string Surname { get; set;  }
    [Required] public string Email { get; set; }
    [Required] public string Password { get; set; }
}