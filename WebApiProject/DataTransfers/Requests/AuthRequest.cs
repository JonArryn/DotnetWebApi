using System.ComponentModel.DataAnnotations;

namespace WebApiProject.Models.Requests;

public class RegisterRequest
{
    [Required] [EmailAddress] public string Email { get; set; } = string.Empty;
    [Required] public string Password { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}

public class LoginRequest
{
    [Required] [EmailAddress] public string Email { get; set; } = string.Empty;
    [Required] public string Password { get; set; } = string.Empty;
}

public class RefreshTokenRequest
{
    public int UserId { get; set; }

    [Required] public string RefreshToken { get; set; } = string.Empty;
}