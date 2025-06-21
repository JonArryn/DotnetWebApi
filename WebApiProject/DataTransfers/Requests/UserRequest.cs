using System.ComponentModel.DataAnnotations;

namespace WebApiProject.Models.Requests;

public class UserRequest
{

    [Required] [EmailAddress] public string Email { get; set; } = string.Empty;
    [Required] public string Password { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}